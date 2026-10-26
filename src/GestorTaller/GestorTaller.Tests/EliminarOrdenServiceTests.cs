using GestorTaller.Datos;
using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "eliminar orden" (issue #134).
public class EliminarOrdenServiceTests
{
    private static readonly PermisosUsuario Administrador = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Admin", EsAdministrador = true });
    private static readonly PermisosUsuario UsuarioComun = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Luis", EsAdministrador = false });

    private static Orden CrearOrden(EstadoOrden estado, bool cotizacionAceptada = false) => new()
    {
        Id = Guid.NewGuid(),
        Cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" },
        DescripcionObjeto = "Vehiculo sedan color rojo",
        DescripcionProblema = "El vehiculo no enciende",
        Estado = estado,
        CotizacionAceptada = cotizacionAceptada,
        FechaRecepcion = DateTime.Now,
        HistorialEstados = new List<EstadoOrden> { estado }
    };

    private static OrdenRepuesto Asignar(Orden orden, Repuesto repuesto, int cantidad)
    {
        var asignado = new OrdenRepuesto { OrdenId = orden.Id, RepuestoId = repuesto.Id, Repuesto = repuesto, Cantidad = cantidad, PrecioUnitario = repuesto.Precio };
        orden.Repuestos.Add(asignado);
        return asignado;
    }

    private static (EliminarOrdenService Servicio, OrdenRepositoryEnMemoria Repositorio) Crear(params Orden[] ordenes)
    {
        var repositorio = new OrdenRepositoryEnMemoria();

        foreach (var orden in ordenes)
        {
            repositorio.Agregar(orden);
        }

        return (new EliminarOrdenService(repositorio), repositorio);
    }

    [Theory]
    [InlineData(EstadoOrden.Recepcion)]
    [InlineData(EstadoOrden.Diagnostico)]
    [InlineData(EstadoOrden.Cotizacion)]
    public void Ejecutar_EnOrdenSinCotizacionAceptada_EliminaLaOrden(EstadoOrden estado)
    {
        var orden = CrearOrden(estado);
        var otra = CrearOrden(EstadoOrden.Diagnostico);
        var (servicio, repositorio) = Crear(orden, otra);

        servicio.Ejecutar(Administrador, orden);

        Assert.Same(otra, Assert.Single(repositorio.ObtenerTodas()));
    }

    [Fact]
    public void Ejecutar_EnCotizacionConRepuestosSinAceptar_NoTocaElInventario()
    {
        var bujia = new Repuesto { Id = Guid.NewGuid(), Nombre = "Bujia", Existencia = 10, Precio = 25m };
        var orden = CrearOrden(EstadoOrden.Cotizacion);
        Asignar(orden, bujia, 3);
        var (servicio, repositorio) = Crear(orden);

        servicio.Ejecutar(Administrador, orden);

        Assert.Empty(repositorio.ObtenerTodas());
        Assert.Equal(10, bujia.Existencia);
    }

    [Theory]
    [InlineData(EstadoOrden.EnReparacion)]
    [InlineData(EstadoOrden.Terminado)]
    public void Ejecutar_ConCotizacionAceptada_DevuelveElInventarioDeCadaRepuesto(EstadoOrden estado)
    {
        var bujia = new Repuesto { Id = Guid.NewGuid(), Nombre = "Bujia", Existencia = 7, Precio = 25m };
        var filtro = new Repuesto { Id = Guid.NewGuid(), Nombre = "Filtro", Existencia = 0, Precio = 40m };
        var orden = CrearOrden(estado, cotizacionAceptada: true);
        Asignar(orden, bujia, 3);
        Asignar(orden, filtro, 2);
        var (servicio, repositorio) = Crear(orden);

        servicio.Ejecutar(Administrador, orden);

        Assert.Empty(repositorio.ObtenerTodas());
        Assert.Equal(10, bujia.Existencia);
        Assert.Equal(2, filtro.Existencia);
    }

    [Fact]
    public void Ejecutar_ConCotizacionRechazada_NoTocaElInventario()
    {
        var orden = CrearOrden(EstadoOrden.Terminado, cotizacionAceptada: false);
        var (servicio, repositorio) = Crear(orden);

        servicio.Ejecutar(Administrador, orden);

        Assert.Empty(repositorio.ObtenerTodas());
    }

    [Fact]
    public void Ejecutar_EnOrdenEntregada_LanzaExcepcionYNoCambiaNada()
    {
        var bujia = new Repuesto { Id = Guid.NewGuid(), Nombre = "Bujia", Existencia = 7, Precio = 25m };
        var orden = CrearOrden(EstadoOrden.Entregado, cotizacionAceptada: true);
        Asignar(orden, bujia, 3);
        var (servicio, repositorio) = Crear(orden);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(Administrador, orden));

        Assert.Single(repositorio.ObtenerTodas());
        Assert.Equal(7, bujia.Existencia);
    }

    [Fact]
    public void Ejecutar_ComoUsuarioComun_LanzaExcepcionYNoCambiaNada()
    {
        var bujia = new Repuesto { Id = Guid.NewGuid(), Nombre = "Bujia", Existencia = 7, Precio = 25m };
        var orden = CrearOrden(EstadoOrden.EnReparacion, cotizacionAceptada: true);
        Asignar(orden, bujia, 3);
        var (servicio, repositorio) = Crear(orden);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(UsuarioComun, orden));

        Assert.Single(repositorio.ObtenerTodas());
        Assert.Equal(7, bujia.Existencia);
    }

    [Fact]
    public void Ejecutar_SinPermisosOSinOrden_LanzaExcepcion()
    {
        var orden = CrearOrden(EstadoOrden.Diagnostico);
        var (servicio, _) = Crear(orden);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, orden));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, null!));
    }
}
