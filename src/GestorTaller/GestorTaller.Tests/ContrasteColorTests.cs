using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del calculo de contraste entre colores (issue #139).
public class ContrasteColorTests
{
    [Fact]
    public void NegroSobreBlanco_Es21()
    {
        Assert.Equal(21.0, ContrasteColor.Calcular("#000000", "#FFFFFF"), 2);
    }

    [Fact]
    public void ColoresIguales_Es1()
    {
        Assert.Equal(1.0, ContrasteColor.Calcular("#34495E", "#34495E"), 2);
    }

    [Fact]
    public void ElOrdenDeLosColores_NoCambiaElResultado()
    {
        Assert.Equal(
            ContrasteColor.Calcular("#1F2D3D", "#FFFFFF"),
            ContrasteColor.Calcular("#FFFFFF", "#1F2D3D"));
    }

    [Theory]
    [InlineData("rojo")]
    [InlineData("#12345")]
    [InlineData("1F2D3D")]
    [InlineData("#GGGGGG")]
    [InlineData("")]
    public void ColorConFormatoInvalido_LanzaArgumentException(string color)
    {
        Assert.Throws<ArgumentException>(() => ContrasteColor.Calcular(color, "#FFFFFF"));
    }
}
