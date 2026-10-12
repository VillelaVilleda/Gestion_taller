using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas de la paginacion de ConsultaTabla, de 20 en 20 (issue #92).
public class ConsultaTablaPaginacionTests
{
    private record Fila(int Numero, string Nombre);

    private static readonly ColumnaTabla<Fila> ColNumero = new("Numero", f => f.Numero);
    private static readonly ColumnaTabla<Fila> ColNombre = new("Nombre", f => f.Nombre);

    private static ConsultaTabla<Fila> CrearConsulta(int cantidad)
    {
        var filas = Enumerable.Range(1, cantidad).Select(n => new Fila(n, $"Elemento {n}"));
        return new ConsultaTabla<Fila>(new[] { ColNumero, ColNombre }, filas);
    }

    [Fact]
    public void FilasPorPagina_Es20()
    {
        Assert.Equal(20, ConsultaTabla<Fila>.FilasPorPagina);
    }

    [Fact]
    public void SinFilas_NoHayPaginas()
    {
        var consulta = CrearConsulta(0);

        Assert.Equal(0, consulta.TotalFilas);
        Assert.Equal(0, consulta.TotalPaginas);
        Assert.Equal(0, consulta.PaginaActual);
        Assert.Empty(consulta.FilasDePagina);
        Assert.False(consulta.HayAnterior);
        Assert.False(consulta.HaySiguiente);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(20, 1)]
    [InlineData(21, 2)]
    [InlineData(40, 2)]
    [InlineData(41, 3)]
    public void TotalPaginas_SeCalculaDeAVeinte(int filas, int paginasEsperadas)
    {
        Assert.Equal(paginasEsperadas, CrearConsulta(filas).TotalPaginas);
    }

    [Fact]
    public void FilasDePagina_LaPrimeraPaginaTraeLas20Primeras()
    {
        var consulta = CrearConsulta(45);

        Assert.Equal(1, consulta.PaginaActual);
        Assert.Equal(20, consulta.FilasDePagina.Count);
        Assert.Equal(1, consulta.FilasDePagina[0].Numero);
        Assert.Equal(20, consulta.FilasDePagina[^1].Numero);
    }

    [Fact]
    public void PaginaSiguiente_MuestraLasSiguientesFilas()
    {
        var consulta = CrearConsulta(45);

        consulta.PaginaSiguiente();

        Assert.Equal(2, consulta.PaginaActual);
        Assert.Equal(21, consulta.FilasDePagina[0].Numero);
        Assert.Equal(40, consulta.FilasDePagina[^1].Numero);
    }

    [Fact]
    public void LaUltimaPagina_PuedeTenerMenosDe20Filas()
    {
        var consulta = CrearConsulta(45);

        consulta.PaginaSiguiente();
        consulta.PaginaSiguiente();

        Assert.Equal(3, consulta.PaginaActual);
        Assert.Equal(5, consulta.FilasDePagina.Count);
        Assert.Equal(45, consulta.FilasDePagina[^1].Numero);
    }

    [Fact]
    public void PaginaSiguiente_EnLaUltimaPagina_NoAvanza()
    {
        var consulta = CrearConsulta(25);

        consulta.PaginaSiguiente();
        consulta.PaginaSiguiente();

        Assert.Equal(2, consulta.PaginaActual);
        Assert.False(consulta.HaySiguiente);
    }

    [Fact]
    public void PaginaAnterior_EnLaPrimeraPagina_NoRetrocede()
    {
        var consulta = CrearConsulta(25);

        consulta.PaginaAnterior();

        Assert.Equal(1, consulta.PaginaActual);
        Assert.False(consulta.HayAnterior);
    }

    [Fact]
    public void PaginaAnterior_VuelveALaPaginaPrevia()
    {
        var consulta = CrearConsulta(45);
        consulta.PaginaSiguiente();
        consulta.PaginaSiguiente();

        consulta.PaginaAnterior();

        Assert.Equal(2, consulta.PaginaActual);
        Assert.True(consulta.HayAnterior);
        Assert.True(consulta.HaySiguiente);
    }

    [Fact]
    public void Buscar_VuelveALaPrimeraPagina()
    {
        var consulta = CrearConsulta(60);
        consulta.PaginaSiguiente();
        consulta.PaginaSiguiente();

        consulta.Buscar(ColNombre, "Elemento");

        Assert.Equal(1, consulta.PaginaActual);
    }

    [Fact]
    public void Ordenar_VuelveALaPrimeraPagina()
    {
        var consulta = CrearConsulta(60);
        consulta.PaginaSiguiente();

        consulta.Ordenar(ColNumero);

        Assert.Equal(1, consulta.PaginaActual);
    }

    [Fact]
    public void Paginacion_SeAplicaSobreTodasLasFilasFiltradas()
    {
        var consulta = CrearConsulta(100);

        consulta.Buscar(ColNombre, "Elemento 1");

        // Coinciden 1, 10-19 y 100: 12 filas, una sola pagina.
        Assert.Equal(12, consulta.TotalFilas);
        Assert.Equal(1, consulta.TotalPaginas);
        Assert.Equal(12, consulta.FilasDePagina.Count);
    }

    [Fact]
    public void Paginacion_RespetaElOrdenSobreTodasLasFilas()
    {
        var consulta = CrearConsulta(45);

        consulta.Ordenar(ColNumero, DireccionOrden.Descendente);

        Assert.Equal(45, consulta.FilasDePagina[0].Numero);
        Assert.Equal(26, consulta.FilasDePagina[^1].Numero);

        consulta.PaginaSiguiente();

        Assert.Equal(25, consulta.FilasDePagina[0].Numero);
    }
}
