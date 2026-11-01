using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "retroceder una etapa" (issue #154).
public class RetrocederEtapaOrdenServiceTests
{
    private static readonly PermisosUsuario Administrador = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Admin", EsAdministrador = true });
    private static readonly PermisosUsuario UsuarioComun = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Luis", EsAdministrador = false });

    private static Orden CrearOrden(params EstadoOrden[] historial) => new()
    {
        Id = Guid.NewGuid(),
        Cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" },
        DescripcionObjeto = "Vehiculo sedan color rojo",
        DescripcionProblema = "El vehiculo no enciende",
        Estado = historial[^1],
        HistorialEstados = historial.ToList()
    };

    private static readonly EstadoOrden R = EstadoOrden.Recepcion;
    private static readonly EstadoOrden D = EstadoOrden.Diagnostico;
    private static readonly EstadoOrden C = EstadoOrden.Cotizacion;
    private static readonly EstadoOrden E = EstadoOrden.EnReparacion;
    private static readonly EstadoOrden T = EstadoOrden.Terminado;
    private static readonly EstadoOrden X = EstadoOrden.Entregado;

    [Fact]
    public void Ejecutar_Terminado_VuelveALaEtapaPreviaSegunElHistorial()
    {
        var repositorio = new OrdenRepositoryFalso();
        var servicio = new RetrocederEtapaOrdenService(repositorio);
        var aceptada = CrearOrden(R, D, C, E, T);
        var rechazada = CrearOrden(R, D, C, T);

        Assert.Equal(E, servicio.Ejecutar(Administrador, aceptada));
        Assert.Equal(C, servicio.Ejecutar(Administrador, rechazada));

        Assert.Equal(new[] { R, D, C, E }, aceptada.HistorialEstados);
        Assert.Equal(new[] { R, D, C }, rechazada.HistorialEstados);
        Assert.NotNull(aceptada.FechaUltimaEdicion);
        Assert.Equal(2, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_Cotizacion_VuelveADiagnostico()
    {
        var servicio = new RetrocederEtapaOrdenService(new OrdenRepositoryFalso());
        var orden = CrearOrden(R, D, C);

        Assert.Equal(D, servicio.Ejecutar(Administrador, orden));
        Assert.Equal(D, orden.Estado);
    }

    [Fact]
    public void Ejecutar_EnReparacion_DevuelveElInventarioYDescartaLaReparacion()
    {
        var repositorio = new OrdenRepositoryFalso();
        var servicio = new RetrocederEtapaOrdenService(repositorio);
        var bujia = new Repuesto { Id = Guid.NewGuid(), Nombre = "Bujia", Existencia = 7, Precio = 25m };
        var orden = CrearOrden(R, D, C, E);
        orden.CotizacionAceptada = true;
        orden.EmpleadoReparacion = new Empleado { Id = Guid.NewGuid(), Nombre = "Ana" };
        orden.DetalleReparacion = "Cambio de bujias";
        orden.Repuestos.Add(new OrdenRepuesto { OrdenId = orden.Id, RepuestoId = bujia.Id, Repuesto = bujia, Cantidad = 3, PrecioUnitario = 25m });

        servicio.Ejecutar(Administrador, orden);

        Assert.Equal(10, bujia.Existencia);
        Assert.Single(orden.Repuestos); // siguen cotizados, pero ya no descontados
        Assert.False(orden.CotizacionAceptada);
        Assert.Null(orden.EmpleadoReparacion);
        Assert.Equal(string.Empty, orden.DetalleReparacion);
    }

    [Fact]
    public void Ejecutar_ComoUsuarioComun_LanzaYNoCambiaNada()
    {
        var repositorio = new OrdenRepositoryFalso();
        var orden = CrearOrden(R, D, C, T);

        Assert.Throws<InvalidOperationException>(() => new RetrocederEtapaOrdenService(repositorio).Ejecutar(UsuarioComun, orden));

        Assert.Equal(T, orden.Estado);
        Assert.Equal(4, orden.HistorialEstados.Count);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Theory]
    [InlineData(EstadoOrden.Recepcion)]
    [InlineData(EstadoOrden.Diagnostico)]
    [InlineData(EstadoOrden.Entregado)]
    public void Ejecutar_SinEtapaAnteriorOEntregada_LanzaYNoCambiaNada(EstadoOrden estado)
    {
        var repositorio = new OrdenRepositoryFalso();
        var historial = Enum.GetValues<EstadoOrden>().Where(e => e <= estado).ToArray();
        var orden = CrearOrden(historial);

        Assert.False(RetrocederEtapaOrdenService.PuedeRetroceder(orden));
        Assert.Throws<InvalidOperationException>(() => new RetrocederEtapaOrdenService(repositorio).Ejecutar(Administrador, orden));

        Assert.Equal(estado, orden.Estado);
        Assert.Equal(historial.Length, orden.HistorialEstados.Count);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_ConDatosFaltantes_Lanza()
    {
        var servicio = new RetrocederEtapaOrdenService(new OrdenRepositoryFalso());

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, CrearOrden(R, D, C)));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, null!));
    }
}
