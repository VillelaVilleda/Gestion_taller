using GestorTaller.Datos;
using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "eliminar repuesto" (issue #125).
public class EliminarRepuestoServiceTests
{
    private static readonly PermisosUsuario Administrador = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Admin", EsAdministrador = true });
    private static readonly PermisosUsuario UsuarioComun = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Luis", EsAdministrador = false });

    private static Repuesto CrearRepuesto(string nombre) =>
        new() { Id = Guid.NewGuid(), Nombre = nombre, Existencia = 10, Precio = 25m };

    private static Orden CrearOrdenCon(Repuesto repuesto) => new()
    {
        Id = Guid.NewGuid(),
        Cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" },
        DescripcionObjeto = "Vehiculo sedan color rojo",
        DescripcionProblema = "El vehiculo no enciende",
        Estado = EstadoOrden.Cotizacion,
        FechaRecepcion = DateTime.Now,
        HistorialEstados = new List<EstadoOrden> { EstadoOrden.Cotizacion },
        Repuestos = new List<OrdenRepuesto> { new() { RepuestoId = repuesto.Id, Repuesto = repuesto, Cantidad = 1, PrecioUnitario = 25m } }
    };

    private static (EliminarRepuestoService Servicio, RepuestoRepositoryFalso Repuestos, OrdenRepositoryEnMemoria Ordenes) Crear(params Repuesto[] repuestos)
    {
        var repositorio = new RepuestoRepositoryFalso();
        repositorio.Guardados.AddRange(repuestos);
        var ordenes = new OrdenRepositoryEnMemoria();
        return (new EliminarRepuestoService(repositorio, ordenes), repositorio, ordenes);
    }

    [Fact]
    public void Ejecutar_ComoAdministradorSinOrdenes_EliminaElRepuesto()
    {
        var bujia = CrearRepuesto("Bujia");
        var filtro = CrearRepuesto("Filtro");
        var (servicio, repuestos, _) = Crear(bujia, filtro);

        servicio.Ejecutar(Administrador, bujia);

        Assert.Same(filtro, Assert.Single(repuestos.Guardados));
    }

    [Fact]
    public void Ejecutar_ConRepuestoEnUnaOrden_LanzaExcepcionYNoElimina()
    {
        var bujia = CrearRepuesto("Bujia");
        var (servicio, repuestos, ordenes) = Crear(bujia);
        ordenes.Agregar(CrearOrdenCon(bujia));

        var error = Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(Administrador, bujia));

        Assert.Contains("ordenes", error.Message);
        Assert.Single(repuestos.Guardados);
    }

    [Fact]
    public void Ejecutar_ConOtroRepuestoEnLasOrdenes_EliminaElRepuestoLibre()
    {
        var bujia = CrearRepuesto("Bujia");
        var filtro = CrearRepuesto("Filtro");
        var (servicio, repuestos, ordenes) = Crear(bujia, filtro);
        ordenes.Agregar(CrearOrdenCon(filtro));

        servicio.Ejecutar(Administrador, bujia);

        Assert.Same(filtro, Assert.Single(repuestos.Guardados));
    }

    [Fact]
    public void Ejecutar_ComoUsuarioComun_LanzaExcepcionYNoElimina()
    {
        var bujia = CrearRepuesto("Bujia");
        var (servicio, repuestos, _) = Crear(bujia);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(UsuarioComun, bujia));

        Assert.Single(repuestos.Guardados);
    }

    [Fact]
    public void Ejecutar_SinPermisosOSinRepuesto_LanzaExcepcion()
    {
        var bujia = CrearRepuesto("Bujia");
        var (servicio, _, _) = Crear(bujia);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, bujia));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, null!));
    }
}
