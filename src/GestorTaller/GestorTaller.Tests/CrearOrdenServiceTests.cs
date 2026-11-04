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

        var orden = servicio.Ejecutar(cliente, "Vehiculo sedan color rojo", "El vehiculo no enciende", 15m);

        Assert.Equal(EstadoOrden.Recepcion, orden.Estado);
        Assert.Equal(cliente, orden.Cliente);
        Assert.Equal("Vehiculo sedan color rojo", orden.DescripcionObjeto);
        Assert.Equal("El vehiculo no enciende", orden.DescripcionProblema);
        Assert.Equal(15m, orden.CostoDiagnostico);
    }

    [Fact]
    public void Ejecutar_ConDatosValidos_GuardaLaOrdenEnElRepositorio()
    {
        var servicio = CrearServicio(out var repositorio);
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };

        var orden = servicio.Ejecutar(cliente, "Vehiculo sedan color rojo", "El vehiculo no enciende", 15m);

        var guardada = repositorio.ObtenerPorId(orden.Id);
        Assert.NotNull(guardada);
    }

    [Fact]
    public void Ejecutar_CostoDiagnosticoEnCero_EsValido()
    {
        var servicio = CrearServicio(out _);
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };

        var orden = servicio.Ejecutar(cliente, "Vehiculo sedan color rojo", "Revision general", 0m);

        Assert.Equal(0m, orden.CostoDiagnostico);
    }

    [Fact]
    public void Ejecutar_SinCliente_LanzaExcepcion()
    {
        var servicio = CrearServicio(out _);

        Assert.Throws<ArgumentException>(() =>
            servicio.Ejecutar(null!, "Vehiculo sedan color rojo", "El vehiculo no enciende", 15m));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinDescripcionObjeto_LanzaExcepcion(string? descripcionObjeto)
    {
        var servicio = CrearServicio(out _);
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };

        Assert.Throws<ArgumentException>(() =>
            servicio.Ejecutar(cliente, descripcionObjeto!, "El vehiculo no enciende", 15m));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinDescripcionProblema_LanzaExcepcion(string? descripcion)
    {
        var servicio = CrearServicio(out _);
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };

        Assert.Throws<ArgumentException>(() =>
            servicio.Ejecutar(cliente, "Vehiculo sedan color rojo", descripcion!, 15m));
    }

    [Fact]
    public void Ejecutar_CostoDiagnosticoNegativo_LanzaExcepcion()
    {
        var servicio = CrearServicio(out _);
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };

        Assert.Throws<ArgumentException>(() =>
            servicio.Ejecutar(cliente, "Vehiculo sedan color rojo", "El vehiculo no enciende", -1m));
    }

    // Issue #175: la base de datos solo acepta 300 caracteres en cada descripción.
    [Theory]
    [InlineData(301, 10)]
    [InlineData(10, 301)]
    public void Ejecutar_ConDescripcionDeMasDe300Caracteres_LanzaExcepcionYNoGuarda(int largoObjeto, int largoProblema)
    {
        var servicio = CrearServicio(out var repositorio);
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };

        Assert.Throws<ArgumentException>(() =>
            servicio.Ejecutar(cliente, new string('a', largoObjeto), new string('b', largoProblema), 15m));

        Assert.Empty(repositorio.ObtenerTodas());
    }

    [Fact]
    public void Ejecutar_ConDescripcionDe300CaracteresYEspacios_GuardaElTextoRecortado()
    {
        var servicio = CrearServicio(out _);
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };
        var objeto = new string('a', CrearOrdenService.LongitudMaximaDescripcion);

        var orden = servicio.Ejecutar(cliente, "  " + objeto + "  ", " No enciende ", 15m);

        Assert.Equal(objeto, orden.DescripcionObjeto);
        Assert.Equal("No enciende", orden.DescripcionProblema);
    }
}
