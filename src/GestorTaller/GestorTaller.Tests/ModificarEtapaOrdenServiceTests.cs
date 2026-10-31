using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "editar diagnóstico o reparación de una orden" (issue #152).
public class ModificarEtapaOrdenServiceTests
{
    private static readonly PermisosUsuario Administrador = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Admin", EsAdministrador = true });
    private static readonly PermisosUsuario UsuarioComun = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Luis", EsAdministrador = false });
    private static readonly Empleado Ana = new() { Id = Guid.NewGuid(), Nombre = "Ana", NombreUsuario = "ana" };
    private static readonly Empleado Pedro = new() { Id = Guid.NewGuid(), Nombre = "Pedro", NombreUsuario = "pedro" };

    // La orden pasó por todas las etapas hasta estadoActual (en orden), con diagnóstico y reparación de Ana.
    private static Orden CrearOrden(EstadoOrden estadoActual) => new()
    {
        Id = Guid.NewGuid(),
        Cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" },
        DescripcionObjeto = "Vehiculo sedan color rojo",
        DescripcionProblema = "El vehiculo no enciende",
        Estado = estadoActual,
        FechaRecepcion = DateTime.Now,
        HistorialEstados = Enum.GetValues<EstadoOrden>().Where(e => e <= estadoActual).ToList(),
        EmpleadoDiagnostico = Ana,
        DetalleDiagnostico = "Bateria descargada",
        EmpleadoReparacion = Ana,
        DetalleReparacion = "Cambio de bateria"
    };

    private static (ModificarEtapaOrdenService Servicio, OrdenRepositoryFalso Repositorio) Crear()
    {
        var repositorio = new OrdenRepositoryFalso();
        return (new ModificarEtapaOrdenService(repositorio), repositorio);
    }

    private static string Detalle(Orden orden, EstadoOrden etapa) =>
        etapa == EstadoOrden.Diagnostico ? orden.DetalleDiagnostico : orden.DetalleReparacion;

    private static Empleado? Encargado(Orden orden, EstadoOrden etapa) =>
        etapa == EstadoOrden.Diagnostico ? orden.EmpleadoDiagnostico : orden.EmpleadoReparacion;

    [Theory]
    [InlineData(EstadoOrden.Diagnostico)]
    [InlineData(EstadoOrden.EnReparacion)]
    public void Ejecutar_ConCambios_GuardaRegistraLaFechaYDevuelveTrue(EstadoOrden etapa)
    {
        var (servicio, repositorio) = Crear();
        var orden = CrearOrden(EstadoOrden.Terminado);

        var cambio = servicio.Ejecutar(Administrador, orden, etapa, Pedro, "  Detalle corregido ");

        Assert.True(cambio);
        Assert.Same(Pedro, Encargado(orden, etapa));
        Assert.Equal("Detalle corregido", Detalle(orden, etapa));
        Assert.NotNull(orden.FechaUltimaEdicion);
        Assert.Equal(EstadoOrden.Terminado, orden.Estado);
        Assert.Equal(1, repositorio.Actualizaciones);
    }

    [Theory]
    [InlineData(EstadoOrden.Diagnostico, "Bateria descargada")]
    [InlineData(EstadoOrden.EnReparacion, "Cambio de bateria")]
    public void Ejecutar_SinCambios_NoGuardaNiTocaLaFecha(EstadoOrden etapa, string detalleActual)
    {
        var (servicio, repositorio) = Crear();
        var orden = CrearOrden(EstadoOrden.Terminado);

        var cambio = servicio.Ejecutar(Administrador, orden, etapa, Ana, $"  {detalleActual} ");

        Assert.False(cambio);
        Assert.Null(orden.FechaUltimaEdicion);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Theory]
    [InlineData(EstadoOrden.Diagnostico)]
    [InlineData(EstadoOrden.EnReparacion)]
    public void Ejecutar_ComoUsuarioComun_LanzaYNoCambiaNada(EstadoOrden etapa)
    {
        var (servicio, repositorio) = Crear();
        var orden = CrearOrden(EstadoOrden.Terminado);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(UsuarioComun, orden, etapa, Pedro, "Otro detalle"));

        Assert.Null(orden.FechaUltimaEdicion);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Theory]
    [InlineData(EstadoOrden.Diagnostico, EstadoOrden.Diagnostico)]
    [InlineData(EstadoOrden.Cotizacion, EstadoOrden.EnReparacion)]
    [InlineData(EstadoOrden.EnReparacion, EstadoOrden.EnReparacion)]
    public void Ejecutar_ConLaEtapaAunNoCompletada_LanzaYNoCambiaNada(EstadoOrden estadoActual, EstadoOrden etapa)
    {
        var (servicio, repositorio) = Crear();
        var orden = CrearOrden(estadoActual);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(Administrador, orden, etapa, Pedro, "Otro detalle"));

        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_EnOrdenEntregada_LanzaYNoCambiaNada()
    {
        var (servicio, repositorio) = Crear();
        var orden = CrearOrden(EstadoOrden.Entregado);

        var error = Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(Administrador, orden, EstadoOrden.Diagnostico, Pedro, "Otro detalle"));

        Assert.Equal("Una orden entregada no se puede editar.", error.Message);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinDetalle_LanzaYNoCambiaNada(string? detalle)
    {
        var (servicio, repositorio) = Crear();
        var orden = CrearOrden(EstadoOrden.Terminado);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, orden, EstadoOrden.Diagnostico, Pedro, detalle!));

        Assert.Equal("Bateria descargada", orden.DetalleDiagnostico);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_SinEmpleadoPermisosOrdenOConEtapaNoEditable_Lanza()
    {
        var (servicio, _) = Crear();
        var orden = CrearOrden(EstadoOrden.Terminado);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, orden, EstadoOrden.Diagnostico, null!, "Detalle"));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, orden, EstadoOrden.Diagnostico, Pedro, "Detalle"));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, null!, EstadoOrden.Diagnostico, Pedro, "Detalle"));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, orden, EstadoOrden.Cotizacion, Pedro, "Detalle"));
    }
}
