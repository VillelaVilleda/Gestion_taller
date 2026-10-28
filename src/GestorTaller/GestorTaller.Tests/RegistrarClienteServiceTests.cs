using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "registrar cliente" (issue #144): crea un cliente desde la pantalla de clientes y lo guarda.
public class RegistrarClienteServiceTests
{
    private static (RegistrarClienteService Servicio, ClienteRepositoryFalso Repositorio) Crear(params Cliente[] clientes)
    {
        var repositorio = new ClienteRepositoryFalso();
        repositorio.Guardados.AddRange(clientes);

        return (new RegistrarClienteService(repositorio), repositorio);
    }

    [Fact]
    public void Ejecutar_ConDatosValidos_GuardaElClienteConLosDatosRecortados()
    {
        var (servicio, repositorio) = Crear();

        var cliente = servicio.Ejecutar("  Juan Perez ", " 5555-1234 ", " juan@correo.com ", "  Zona 1 ");

        var guardado = Assert.Single(repositorio.Guardados);
        Assert.Same(cliente, guardado);
        Assert.Equal("Juan Perez", guardado.Nombre);
        Assert.Equal("5555-1234", guardado.Telefono);
        Assert.Equal("juan@correo.com", guardado.Correo);
        Assert.Equal("Zona 1", guardado.Direccion);
        Assert.NotEqual(Guid.Empty, guardado.Id);
    }

    [Fact]
    public void Ejecutar_ConNombreRepetido_LanzaExcepcionYNoGuarda()
    {
        var existente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez", Telefono = "5555-1234" };
        var (servicio, repositorio) = Crear(existente);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar("juan pérez", "5555-9999"));

        Assert.Single(repositorio.Guardados);
    }

    [Fact]
    public void Ejecutar_ConDatosInvalidos_LanzaExcepcionYNoGuarda()
    {
        var (servicio, repositorio) = Crear();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar("Juan Perez", null, null, null));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar("", "5555-1234"));

        Assert.Empty(repositorio.Guardados);
    }
}
