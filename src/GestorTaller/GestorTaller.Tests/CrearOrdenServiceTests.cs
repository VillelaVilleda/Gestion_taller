using GestorTaller.Datos;
using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del caso de uso "crear nueva orden" (issue #11).
// Estan en rojo a proposito: CrearOrdenService.Ejecutar todavia no esta
// implementado (issue #12). Implementar el codigo minimo en CrearOrdenService
// hasta que todas estas pruebas pasen (fase Green), luego refactorizar si hace falta.
public class CrearOrdenServiceTests
{
    private static CrearOrdenService CrearServicio(out IOrdenRepository repositorio)
    {
        repositorio = new OrdenRepositoryEnMemoria();
        return new CrearOrdenService(repositorio);
    }

    [Fact]
    public void Ejecutar_ConDatosValidos_CreaOrdenEnEstadoRecepcion()
    {
        var servicio = CrearServicio(out _);
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };

        var orden = servicio.Ejecutar(cliente, "El vehiculo no enciende", 15m);

        Assert.Equal(EstadoOrden.Recepcion, orden.Estado);
        Assert.Equal(cliente, orden.Cliente);
        Assert.Equal("El vehiculo no enciende", orden.DescripcionProblema);
        Assert.Equal(15m, orden.CostoDiagnostico);
    }

    [Fact]
    public void Ejecutar_ConDatosValidos_GuardaLaOrdenEnElRepositorio()
    {
        var servicio = CrearServicio(out var repositorio);
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };

        var orden = servicio.Ejecutar(cliente, "El vehiculo no enciende", 15m);

        var guardada = repositorio.ObtenerPorId(orden.Id);
        Assert.NotNull(guardada);
    }

    [Fact]
    public void Ejecutar_CostoDiagnosticoEnCero_EsValido()
    {
        var servicio = CrearServicio(out _);
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };

        var orden = servicio.Ejecutar(cliente, "Revision general", 0m);

        Assert.Equal(0m, orden.CostoDiagnostico);
    }

    [Fact]
    public void Ejecutar_SinCliente_LanzaExcepcion()
    {
        var servicio = CrearServicio(out _);

        Assert.Throws<ArgumentException>(() =>
            servicio.Ejecutar(null!, "El vehiculo no enciende", 15m));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinDescripcion_LanzaExcepcion(string? descripcion)
    {
        var servicio = CrearServicio(out _);
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };

        Assert.Throws<ArgumentException>(() =>
            servicio.Ejecutar(cliente, descripcion!, 15m));
    }

    [Fact]
    public void Ejecutar_CostoDiagnosticoNegativo_LanzaExcepcion()
    {
        var servicio = CrearServicio(out _);
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };

        Assert.Throws<ArgumentException>(() =>
            servicio.Ejecutar(cliente, "El vehiculo no enciende", -1m));
    }
}
