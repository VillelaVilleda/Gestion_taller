using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// El estilo visual debe ser legible: texto con contraste minimo 4.5:1 (WCAG AA) y
// bordes de campos con 3:1 (issue #139).
public class PaletaTemaTests
{
    [Theory]
    [InlineData(PaletaTema.Titanio, PaletaTema.Blanco)]
    [InlineData(PaletaTema.Titanio, PaletaTema.Fondo)]
    [InlineData(PaletaTema.Blanco, PaletaTema.Pizarra)]
    [InlineData(PaletaTema.Blanco, PaletaTema.PizarraHover)]
    [InlineData(PaletaTema.Titanio, PaletaTema.Cobre)]
    [InlineData(PaletaTema.CobreOscuro, PaletaTema.Blanco)]
    [InlineData(PaletaTema.TextoSecundario, PaletaTema.Blanco)]
    [InlineData(PaletaTema.TextoSecundario, PaletaTema.Fondo)]
    [InlineData(PaletaTema.Blanco, PaletaTema.Exito)]
    [InlineData(PaletaTema.Blanco, PaletaTema.Error)]
    [InlineData(PaletaTema.Blanco, PaletaTema.Advertencia)]
    [InlineData(PaletaTema.Blanco, PaletaTema.Informacion)]
    [InlineData(PaletaTema.Exito, PaletaTema.ExitoClaro)]
    [InlineData(PaletaTema.Error, PaletaTema.ErrorClaro)]
    [InlineData(PaletaTema.Advertencia, PaletaTema.AdvertenciaClaro)]
    [InlineData(PaletaTema.Informacion, PaletaTema.InformacionClaro)]
    public void TextoYFondo_TienenContrasteMinimoDe4Punto5(string texto, string fondo)
    {
        Assert.True(ContrasteColor.Calcular(texto, fondo) >= 4.5);
    }

    [Fact]
    public void BordeDeCampo_TieneContrasteMinimoDe3ConElFondoBlanco()
    {
        Assert.True(ContrasteColor.Calcular(PaletaTema.BordeCampo, PaletaTema.Blanco) >= 3.0);
    }

    [Fact]
    public void BlancoSobreCobre_NoCumple_PorEsoElCobreLlevaTextoOscuro()
    {
        Assert.True(ContrasteColor.Calcular(PaletaTema.Blanco, PaletaTema.Cobre) < 4.5);
    }

    [Theory]
    [MemberData(nameof(TodasLasEtapas))]
    public void CadaEtapa_TieneColoresConContrasteMinimo(EstadoOrden estado)
    {
        var (texto, fondo) = PaletaTema.ColoresEtapa(estado);

        Assert.True(ContrasteColor.Calcular(texto, fondo) >= 4.5);
    }

    public static IEnumerable<object[]> TodasLasEtapas() =>
        Enum.GetValues<EstadoOrden>().Select(estado => new object[] { estado });
}
