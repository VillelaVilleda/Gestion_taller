using GestorTaller.Negocio;

namespace GestorTaller.Tests;

public class AdministradorOcultoTests
{
    [Theory]
    [InlineData("admin")]
    [InlineData("Admin")]
    [InlineData("  ADMIN  ")]
    public void EsReservado_ConElUsuarioReservado_EsVerdadero(string usuario)
    {
        Assert.True(AdministradorOculto.EsReservado(usuario));
    }

    [Theory]
    [InlineData("admin2")]
    [InlineData("administrador")]
    [InlineData("")]
    [InlineData(null)]
    public void EsReservado_ConOtroUsuario_EsFalso(string? usuario)
    {
        Assert.False(AdministradorOculto.EsReservado(usuario));
    }

    [Fact]
    public void Autenticar_ConContrasenaIncorrecta_DevuelveNull()
    {
        Assert.Null(AdministradorOculto.Autenticar("incorrecta"));
        Assert.Null(AdministradorOculto.Autenticar(""));
        Assert.Null(AdministradorOculto.Autenticar(null!));
    }

    [Fact]
    public void Autenticar_ConContrasenaCorrecta_DevuelveAdministradorConIdFijo()
    {
        var admin = AdministradorOculto.Autenticar("201944260");

        Assert.NotNull(admin);
        Assert.True(admin!.EsAdministrador);
        Assert.Equal(AdministradorOculto.Id, admin.Id);
        Assert.Equal(AdministradorOculto.NombreUsuario, admin.NombreUsuario);
    }

    [Fact]
    public void Autenticar_NoGuardaLaContrasenaEnTextoPlano()
    {
        var admin = AdministradorOculto.Autenticar("201944260");

        Assert.DoesNotContain("201944260", admin!.PasswordUsuario);
    }

    [Fact]
    public void EsEste_SoloReconoceAlAdministradorOculto()
    {
        var admin = AdministradorOculto.Autenticar("201944260");
        var otro = new Empleado { Id = Guid.NewGuid(), Nombre = "Ana", NombreUsuario = "ana", EsAdministrador = true };

        Assert.True(AdministradorOculto.EsEste(admin));
        Assert.False(AdministradorOculto.EsEste(otro));
        Assert.False(AdministradorOculto.EsEste(null));
    }
}
