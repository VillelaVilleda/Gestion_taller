using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "editar cotización con ajuste de inventario" (issue #153).
public class ModificarCotizacionOrdenServiceTests
{
    private static readonly PermisosUsuario Administrador = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Admin", EsAdministrador = true });
    private static readonly PermisosUsuario UsuarioComun = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Luis", EsAdministrador = false });
    private static readonly Empleado Ana = new() { Id = Guid.NewGuid(), Nombre = "Ana", NombreUsuario = "ana" };
    private static readonly Empleado Pedro = new() { Id = Guid.NewGuid(), Nombre = "Pedro", NombreUsuario = "pedro" };

    private static Repuesto CrearRepuesto(string nombre, int existencia, decimal precio = 25m) =>
        new() { Id = Guid.NewGuid(), Nombre = nombre, Existencia = existencia, Precio = precio };

    // Orden con cotización ya registrada por Ana (mano de obra Q100) y, si fue aceptada, con los repuestos indicados
    // (el inventario de esos repuestos ya refleja lo descontado).
    private static Orden CrearOrden(bool aceptada, EstadoOrden estado = EstadoOrden.EnReparacion, params (Repuesto Repuesto, int Cantidad)[] asignados)
    {
        var orden = new Orden
        {
            Id = Guid.NewGuid(),
            Cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" },
            DescripcionObjeto = "Vehiculo sedan color rojo",
            DescripcionProblema = "El vehiculo no enciende",
            Estado = estado,
            FechaRecepcion = DateTime.Now,
            HistorialEstados = Enum.GetValues<EstadoOrden>().Where(e => e <= estado).ToList(),
            EmpleadoCotizacion = Ana,
            MontoCotizado = 100m,
            CotizacionAceptada = aceptada
        };

        foreach (var (repuesto, cantidad) in asignados)
        {
            orden.Repuestos.Add(new OrdenRepuesto { OrdenId = orden.Id, RepuestoId = repuesto.Id, Repuesto = repuesto, Cantidad = cantidad, PrecioUnitario = repuesto.Precio });
        }

        return orden;
    }

    private static (ModificarCotizacionOrdenService Servicio, OrdenRepositoryFalso Repositorio) Crear()
    {
        var repositorio = new OrdenRepositoryFalso();
        return (new ModificarCotizacionOrdenService(repositorio), repositorio);
    }

    [Fact]
    public void Ejecutar_SubirLaCantidad_DescuentaSoloLaDiferencia()
    {
        var (servicio, repositorio) = Crear();
        var bujia = CrearRepuesto("Bujia", existencia: 10);
        var orden = CrearOrden(true, EstadoOrden.EnReparacion, (bujia, 2));

        var cambio = servicio.Ejecutar(Administrador, orden, Ana, 100m, new[] { new RepuestoCotizado(bujia, 5) });

        Assert.True(cambio);
        Assert.Equal(7, bujia.Existencia);
        Assert.Equal(5, Assert.Single(orden.Repuestos).Cantidad);
        Assert.NotNull(orden.FechaUltimaEdicion);
        Assert.Equal(1, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_BajarLaCantidad_DevuelveLaDiferencia()
    {
        var (servicio, _) = Crear();
        var bujia = CrearRepuesto("Bujia", existencia: 10);
        var orden = CrearOrden(true, EstadoOrden.EnReparacion, (bujia, 4));

        servicio.Ejecutar(Administrador, orden, Ana, 100m, new[] { new RepuestoCotizado(bujia, 1) });

        Assert.Equal(13, bujia.Existencia);
        Assert.Equal(1, Assert.Single(orden.Repuestos).Cantidad);
    }

    [Fact]
    public void Ejecutar_QuitarUnRepuesto_LoDevuelveCompletoYLoSacaDeLaOrden()
    {
        var (servicio, _) = Crear();
        var bujia = CrearRepuesto("Bujia", existencia: 10);
        var filtro = CrearRepuesto("Filtro", existencia: 5);
        var orden = CrearOrden(true, EstadoOrden.EnReparacion, (bujia, 3), (filtro, 1));

        servicio.Ejecutar(Administrador, orden, Ana, 100m, new[] { new RepuestoCotizado(filtro, 1) });

        Assert.Equal(13, bujia.Existencia);
        Assert.Equal(5, filtro.Existencia);
        Assert.Equal(filtro.Id, Assert.Single(orden.Repuestos).RepuestoId);
    }

    [Fact]
    public void Ejecutar_AgregarUnRepuestoNuevo_LoDescuentaCompletoConElPrecioVigente()
    {
        var (servicio, _) = Crear();
        var bujia = CrearRepuesto("Bujia", existencia: 10, precio: 25m);
        var filtro = CrearRepuesto("Filtro", existencia: 5, precio: 60m);
        var orden = CrearOrden(true, EstadoOrden.EnReparacion, (bujia, 2));
        bujia.Precio = 40m; // el precio cambió después de cotizar

        servicio.Ejecutar(Administrador, orden, Ana, 100m, new[] { new RepuestoCotizado(bujia, 2), new RepuestoCotizado(filtro, 2) });

        Assert.Equal(3, filtro.Existencia);
        Assert.Equal(10, bujia.Existencia);
        Assert.Equal(25m, orden.Repuestos.Single(r => r.RepuestoId == bujia.Id).PrecioUnitario); // conserva el original
        Assert.Equal(60m, orden.Repuestos.Single(r => r.RepuestoId == filtro.Id).PrecioUnitario);
    }

    [Fact]
    public void Ejecutar_SiFaltaExistenciaDeAlgunRepuesto_LanzaYNoCambiaNada()
    {
        var (servicio, repositorio) = Crear();
        var bujia = CrearRepuesto("Bujia", existencia: 10);
        var filtro = CrearRepuesto("Filtro", existencia: 1);
        var orden = CrearOrden(true, EstadoOrden.EnReparacion, (bujia, 2), (filtro, 1));

        var error = Assert.Throws<InvalidOperationException>(() =>
            servicio.Ejecutar(Administrador, orden, Pedro, 200m, new[] { new RepuestoCotizado(bujia, 1), new RepuestoCotizado(filtro, 4) }));

        Assert.Contains("Filtro", error.Message);
        Assert.Equal(10, bujia.Existencia);
        Assert.Equal(1, filtro.Existencia);
        Assert.Equal(2, orden.Repuestos.Single(r => r.RepuestoId == bujia.Id).Cantidad);
        Assert.Equal(Ana, orden.EmpleadoCotizacion);
        Assert.Equal(100m, orden.MontoCotizado);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_SoloCambiaElEmpleadoOLaManoDeObra_NoTocaElInventario()
    {
        var (servicio, repositorio) = Crear();
        var bujia = CrearRepuesto("Bujia", existencia: 10);
        var orden = CrearOrden(true, EstadoOrden.Terminado, (bujia, 2));

        var cambio = servicio.Ejecutar(Administrador, orden, Pedro, 150m, new[] { new RepuestoCotizado(bujia, 2) });

        Assert.True(cambio);
        Assert.Equal(10, bujia.Existencia);
        Assert.Same(Pedro, orden.EmpleadoCotizacion);
        Assert.Equal(150m, orden.MontoCotizado);
        Assert.Equal(1, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_SinCambios_NoGuardaNiTocaLaFechaNiElInventario()
    {
        var (servicio, repositorio) = Crear();
        var bujia = CrearRepuesto("Bujia", existencia: 10);
        var orden = CrearOrden(true, EstadoOrden.EnReparacion, (bujia, 2));

        var cambio = servicio.Ejecutar(Administrador, orden, Ana, 100m, new[] { new RepuestoCotizado(bujia, 2) });

        Assert.False(cambio);
        Assert.Null(orden.FechaUltimaEdicion);
        Assert.Equal(10, bujia.Existencia);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_EnCotizacionRechazada_EditaEmpleadoYMontoPeroNoAceptaRepuestos()
    {
        var (servicio, _) = Crear();
        var bujia = CrearRepuesto("Bujia", existencia: 10);
        var orden = CrearOrden(false, EstadoOrden.Terminado);

        Assert.True(servicio.Ejecutar(Administrador, orden, Pedro, 80m, Array.Empty<RepuestoCotizado>()));
        Assert.Equal(80m, orden.MontoCotizado);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(Administrador, orden, Pedro, 80m, new[] { new RepuestoCotizado(bujia, 1) }));
        Assert.Equal(10, bujia.Existencia);
    }

    [Fact]
    public void Ejecutar_ComoUsuarioComun_LanzaYNoCambiaNada()
    {
        var (servicio, repositorio) = Crear();
        var orden = CrearOrden(true);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(UsuarioComun, orden, Pedro, 150m, Array.Empty<RepuestoCotizado>()));

        Assert.Equal(100m, orden.MontoCotizado);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Theory]
    [InlineData(EstadoOrden.Cotizacion)]
    [InlineData(EstadoOrden.Entregado)]
    public void Ejecutar_ConLaCotizacionAunEnCursoOLaOrdenEntregada_LanzaYNoCambiaNada(EstadoOrden estado)
    {
        var (servicio, repositorio) = Crear();
        var orden = CrearOrden(true, estado);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(Administrador, orden, Pedro, 150m, Array.Empty<RepuestoCotizado>()));

        Assert.Equal(100m, orden.MontoCotizado);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_ConDatosInvalidos_Lanza()
    {
        var (servicio, repositorio) = Crear();
        var bujia = CrearRepuesto("Bujia", existencia: 10);
        var orden = CrearOrden(true, EstadoOrden.EnReparacion, (bujia, 2));
        var sinRepuestos = Array.Empty<RepuestoCotizado>();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, orden, Pedro, 0m, sinRepuestos));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, orden, null!, 100m, sinRepuestos));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, orden, Pedro, 100m, null!));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, orden, Pedro, 100m, new[] { new RepuestoCotizado(bujia, 0) }));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, orden, Pedro, 100m, new[] { new RepuestoCotizado(bujia, 1), new RepuestoCotizado(bujia, 2) }));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, orden, Pedro, 100m, sinRepuestos));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, null!, Pedro, 100m, sinRepuestos));

        Assert.Equal(10, bujia.Existencia);
        Assert.Equal(0, repositorio.Actualizaciones);
    }
}
