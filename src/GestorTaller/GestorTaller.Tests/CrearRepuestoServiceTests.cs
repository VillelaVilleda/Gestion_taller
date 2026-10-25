using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "crear repuesto" (issue #113).
public class CrearRepuestoServiceTests
{
    private static (CrearRepuestoService Servicio, RepuestoRepositoryFalso Repositorio) Crear()
    {
        var repositorio = new RepuestoRepositoryFalso();
        return (new CrearRepuestoService(repositorio), repositorio);
    }

    [Fact]
    public void Ejecutar_ConDatosValidos_GuardaElRepuesto()
    {
        var (servicio, repositorio) = Crear();

        var repuesto = servicio.Ejecutar("  Bujia  ", 10, 25.50m);

        Assert.Same(repuesto, Assert.Single(repositorio.Guardados));
        Assert.Equal("Bujia", repuesto.Nombre);
        Assert.Equal(10, repuesto.Existencia);
        Assert.Equal(25.50m, repuesto.Precio);
        Assert.NotEqual(Guid.Empty, repuesto.Id);
    }

    [Fact]
    public void Ejecutar_ConExistenciaYPrecioEnCero_EsValido()
    {
        var (servicio, _) = Crear();

        var repuesto = servicio.Ejecutar("Bujia", 0, 0m);

        Assert.Equal(0, repuesto.Existencia);
        Assert.Equal(0m, repuesto.Precio);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinNombre_LanzaExcepcion(string? nombre)
    {
        var (servicio, repositorio) = Crear();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(nombre!, 10, 25m));
        Assert.Empty(repositorio.Guardados);
    }

    [Fact]
    public void Ejecutar_ConNombreDeMasDe150Caracteres_LanzaExcepcion()
    {
        var (servicio, _) = Crear();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(new string('a', 151), 10, 25m));
    }

    [Theory]
    [InlineData("bujia")]
    [InlineData("  BUJIA ")]
    public void Ejecutar_ConNombreRepetido_LanzaExcepcion(string nombre)
    {
        var (servicio, repositorio) = Crear();
        servicio.Ejecutar("Bujía", 10, 25m);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(nombre, 5, 30m));
        Assert.Single(repositorio.Guardados);
    }

    [Fact]
    public void Ejecutar_ConExistenciaNegativa_LanzaExcepcion()
    {
        var (servicio, _) = Crear();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar("Bujia", -1, 25m));
    }

    [Fact]
    public void Ejecutar_ConPrecioNegativo_LanzaExcepcion()
    {
        var (servicio, _) = Crear();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar("Bujia", 10, -0.01m));
    }
}
