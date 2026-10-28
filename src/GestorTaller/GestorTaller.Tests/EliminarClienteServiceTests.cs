using GestorTaller.Datos;
using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "eliminar cliente" (issue #126).
public class EliminarClienteServiceTests
{
    private static readonly PermisosUsuario Administrador = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Admin", EsAdministrador = true });
    private static readonly PermisosUsuario UsuarioComun = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Luis", EsAdministrador = false });

    private static Cliente CrearCliente(string nombre) =>
        new() { Id = Guid.NewGuid(), Nombre = nombre, Telefono = "5555-1234" };

    private static Orden CrearOrdenDe(Cliente cliente) => new()
    {
        Id = Guid.NewGuid(),
        Cliente = cliente,
        DescripcionObjeto = "Vehiculo sedan color rojo",
        DescripcionProblema = "El vehiculo no enciende",
        Estado = EstadoOrden.Recepcion,
        FechaRecepcion = DateTime.Now,
        HistorialEstados = new List<EstadoOrden> { EstadoOrden.Recepcion }
    };

    private static (EliminarClienteService Servicio, ClienteRepositoryFalso Clientes, OrdenRepositoryEnMemoria Ordenes) Crear(params Cliente[] clientes)
    {
        var repositorio = new ClienteRepositoryFalso();
        repositorio.Guardados.AddRange(clientes);
        var ordenes = new OrdenRepositoryEnMemoria();
        return (new EliminarClienteService(repositorio, ordenes), repositorio, ordenes);
    }

    [Fact]
    public void Ejecutar_ComoAdministradorSinOrdenes_EliminaElCliente()
    {
        var juan = CrearCliente("Juan Perez");
        var maria = CrearCliente("Maria Lopez");
        var (servicio, clientes, _) = Crear(juan, maria);

        servicio.Ejecutar(Administrador, juan);

        Assert.Same(maria, Assert.Single(clientes.Guardados));
    }

    [Fact]
    public void Ejecutar_ConClienteConOrdenes_LanzaExcepcionYNoElimina()
    {
        var juan = CrearCliente("Juan Perez");
        var (servicio, clientes, ordenes) = Crear(juan);
        ordenes.Agregar(CrearOrdenDe(juan));

        var error = Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(Administrador, juan));

        Assert.Contains("1 orden", error.Message);
        Assert.Single(clientes.Guardados);
    }

    [Fact]
    public void Ejecutar_ConVariasOrdenes_IndicaCuantasTiene()
    {
        var juan = CrearCliente("Juan Perez");
        var (servicio, _, ordenes) = Crear(juan);
        ordenes.Agregar(CrearOrdenDe(juan));
        ordenes.Agregar(CrearOrdenDe(juan));

        var error = Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(Administrador, juan));

        Assert.Contains("2 órdenes", error.Message);
    }

    [Fact]
    public void Ejecutar_ConOrdenesDeOtroCliente_EliminaAlClienteLibre()
    {
        var juan = CrearCliente("Juan Perez");
        var maria = CrearCliente("Maria Lopez");
        var (servicio, clientes, ordenes) = Crear(juan, maria);
        ordenes.Agregar(CrearOrdenDe(maria));

        servicio.Ejecutar(Administrador, juan);

        Assert.Same(maria, Assert.Single(clientes.Guardados));
    }

    [Fact]
    public void Ejecutar_ComoUsuarioComun_LanzaExcepcionYNoElimina()
    {
        var juan = CrearCliente("Juan Perez");
        var (servicio, clientes, _) = Crear(juan);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(UsuarioComun, juan));

        Assert.Single(clientes.Guardados);
    }

    [Fact]
    public void Ejecutar_SinPermisosOSinCliente_LanzaExcepcion()
    {
        var juan = CrearCliente("Juan Perez");
        var (servicio, _, _) = Crear(juan);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, juan));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, null!));
    }
}
