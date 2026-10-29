using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas de las reglas de longitud de una contraseña escrita por una persona.
public class ValidadorContrasenaTests
{
    [Theory]
    [InlineData("abcdef")]
    [InlineData("una clave con espacios")]
    public void Validar_ConLongitudCorrecta_NoLanza(string password)
    {
        ValidadorContrasena.Validar(password);
    }

    [Fact]
    public void Validar_ConElMaximoExacto_NoLanza()
    {
        ValidadorContrasena.Validar(new string('a', ValidadorContrasena.LongitudMaxima));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData("abcde")]
    public void Validar_ConMenosDelMinimo_Lanza(string? password)
    {
        var error = Assert.Throws<ArgumentException>(() => ValidadorContrasena.Validar(password));

        Assert.Contains("al menos 6", error.Message);
    }

    [Fact]
    public void Validar_ConMasDelMaximo_Lanza()
    {
        var error = Assert.Throws<ArgumentException>(() => ValidadorContrasena.Validar(new string('a', ValidadorContrasena.LongitudMaxima + 1)));

        Assert.Contains("más de 50", error.Message);
    }

    [Fact]
    public void Validar_ConPocosCaracteresPeroMasDe72Bytes_Lanza()
    {
        // 50 caracteres de 4 bytes cada uno (200 bytes): cumple el máximo de caracteres pero BCrypt los recortaría.
        var password = string.Concat(Enumerable.Repeat("😀", 25));

        Assert.True(password.Length <= ValidadorContrasena.LongitudMaxima);
        Assert.Throws<ArgumentException>(() => ValidadorContrasena.Validar(password));
    }

    [Fact]
    public void Validar_UsaLaDescripcionIndicadaEnElMensaje()
    {
        var error = Assert.Throws<ArgumentException>(() => ValidadorContrasena.Validar("abc", "La contraseña nueva"));

        Assert.StartsWith("La contraseña nueva", error.Message);
    }
}
