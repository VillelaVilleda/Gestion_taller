using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "crear cliente nuevo" (issue #72, correccion de clientes duplicados).
public class CrearClienteServiceTests
{
    private const string Telefono = "5555-1234";

    private class ClienteRepositoryFalso : IClienteRepository
    {
        public List<Cliente> Guardados { get; } = new();

        public void Agregar(Cliente cliente) => Guardados.Add(cliente);

        public Cliente? ObtenerPorId(Guid id) => Guardados.FirstOrDefault(c => c.Id == id);

        public IReadOnlyList<Cliente> ObtenerTodos() => Guardados;
    }

    [Fact]
    public void Ejecutar_ConNombreValido_DevuelveUnClienteConEseNombre()
    {
        var servicio = new CrearClienteService(new ClienteRepositoryFalso());

        var cliente = servicio.Ejecutar("  Juan Perez  ", Telefono);

        Assert.Equal("Juan Perez", cliente.Nombre);
        Assert.NotEqual(Guid.Empty, cliente.Id);
    }

    [Fact]
    public void Ejecutar_ConDatosDeContacto_LosGuardaRecortados()
    {
        var servicio = new CrearClienteService(new ClienteRepositoryFalso());

        var cliente = servicio.Ejecutar("Juan Perez", " 5555-1234 ", " juan@correo.com ", "  Zona 1  ");

        Assert.Equal("5555-1234", cliente.Telefono);
        Assert.Equal("juan@correo.com", cliente.Correo);
        Assert.Equal("Zona 1", cliente.Direccion);
    }

    [Fact]
    public void Ejecutar_SinContacto_LanzaExcepcion()
    {
        var servicio = new CrearClienteService(new ClienteRepositoryFalso());

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar("Juan Perez", direccion: "Zona 1"));
    }

    [Fact]
    public void Ejecutar_NoGuardaElClienteEnElRepositorio()
    {
        var repositorio = new ClienteRepositoryFalso();
        var servicio = new CrearClienteService(repositorio);

        servicio.Ejecutar("Juan Perez", Telefono);

        Assert.Empty(repositorio.Guardados);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinNombre_LanzaExcepcion(string? nombre)
    {
        var servicio = new CrearClienteService(new ClienteRepositoryFalso());

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(nombre!));
    }

    [Theory]
    [InlineData("juan perez")]
    [InlineData("JUAN PEREZ")]
    [InlineData("  Juan Perez ")]
    public void Ejecutar_ConNombreDeUnClienteExistente_LanzaExcepcion(string nombre)
    {
        var repositorio = new ClienteRepositoryFalso();
        repositorio.Agregar(new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" });
        var servicio = new CrearClienteService(repositorio);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(nombre, Telefono));
    }

    [Fact]
    public void Ejecutar_ConNombreDeUnClienteExistenteConAcentos_LanzaExcepcion()
    {
        var repositorio = new ClienteRepositoryFalso();
        repositorio.Agregar(new Cliente { Id = Guid.NewGuid(), Nombre = "María López" });
        var servicio = new CrearClienteService(repositorio);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar("maria lopez", Telefono));
    }
}
