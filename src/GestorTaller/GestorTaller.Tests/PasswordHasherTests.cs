using GestorTaller.Negocio;

namespace GestorTaller.Tests;

public class PasswordHasherTests
{
    [Fact]
    public void Hash_NoDevuelveLaContrasenaEnTextoPlano()
    {
        var hash = PasswordHasher.Hash("miClave123");

        Assert.NotEqual("miClave123", hash);
    }

    [Fact]
    public void Hash_DeLaMismaContrasena_GeneraHashesDistintos()
    {
        var hash1 = PasswordHasher.Hash("miClave123");
        var hash2 = PasswordHasher.Hash("miClave123");

        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void Verificar_ConLaContrasenaCorrecta_DevuelveTrue()
    {
        var hash = PasswordHasher.Hash("miClave123");

        Assert.True(PasswordHasher.Verificar("miClave123", hash));
    }

    [Fact]
    public void Verificar_ConLaContrasenaIncorrecta_DevuelveFalse()
    {
        var hash = PasswordHasher.Hash("miClave123");

        Assert.False(PasswordHasher.Verificar("otraClave", hash));
    }
}