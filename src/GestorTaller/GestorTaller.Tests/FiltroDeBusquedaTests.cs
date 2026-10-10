using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del filtro de busqueda de los combobox (issue #68).
public class FiltroDeBusquedaTests
{
    private static readonly string[] Nombres =
    {
        "Filtro de aceite",
        "Bujia",
        "Pastillas de freno",
        "Correa de distribución"
    };

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Filtrar_ConTextoVacio_DevuelveTodosLosElementos(string? texto)
    {
        var resultado = FiltroDeBusqueda.Filtrar(Nombres, texto, n => n);

        Assert.Equal(4, resultado.Count);
    }

    [Fact]
    public void Filtrar_ConCoincidenciaParcial_DevuelveLosQueContienenElTexto()
    {
        var resultado = FiltroDeBusqueda.Filtrar(Nombres, "tro", n => n);

        Assert.Single(resultado);
        Assert.Equal("Filtro de aceite", resultado[0]);
    }

    [Fact]
    public void Filtrar_IgnoraMayusculasYMinusculas()
    {
        var resultado = FiltroDeBusqueda.Filtrar(Nombres, "BUJIA", n => n);

        Assert.Single(resultado);
    }

    [Fact]
    public void Filtrar_IgnoraAcentos()
    {
        var resultado = FiltroDeBusqueda.Filtrar(Nombres, "distribucion", n => n);

        Assert.Single(resultado);
        Assert.Equal("Correa de distribución", resultado[0]);
    }

    [Fact]
    public void Filtrar_SinCoincidencias_DevuelveListaVacia()
    {
        var resultado = FiltroDeBusqueda.Filtrar(Nombres, "xyz", n => n);

        Assert.Empty(resultado);
    }

    [Fact]
    public void Filtrar_ConObjetos_UsaElSelectorDeTexto()
    {
        var clientes = new[]
        {
            new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" },
            new Cliente { Id = Guid.NewGuid(), Nombre = "Maria Lopez" }
        };

        var resultado = FiltroDeBusqueda.Filtrar(clientes, "lop", c => c.Nombre);

        Assert.Single(resultado);
        Assert.Equal("Maria Lopez", resultado[0].Nombre);
    }

    [Fact]
    public void Filtrar_SinLista_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => FiltroDeBusqueda.Filtrar<string>(null!, "a", n => n));
    }
}
