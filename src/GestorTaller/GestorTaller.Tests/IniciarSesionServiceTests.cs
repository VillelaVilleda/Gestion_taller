using GestorTaller.Negocio;

namespace GestorTaller.Tests;

public class IniciarSesionServiceTests
{
    [Fact]
    public void Autenticar_ConDatosDeAdmin_DevuelveUsuarioAdministrador()
    {
        var servicio = new IniciarSesionService();

        var usuario = servicio.Autenticar("admin", "admin123");

        Assert.NotNull(usuario);
        Assert.Equal(RolUsuario.Administrador, usuario!.Rol);
    }

    [Fact]
    public void Autenticar_ConDatosDeUsuarioComun_DevuelveUsuarioComun()
    {
        var servicio = new IniciarSesionService();

        var usuario = servicio.Autenticar("usuario", "usuario123");

        Assert.NotNull(usuario);
        Assert.Equal(RolUsuario.Usuario, usuario!.Rol);
    }

    [Fact]
    public void Autenticar_ConContrasenaIncorrecta_DevuelveNull()
    {
        var servicio = new IniciarSesionService();

        var usuario = servicio.Autenticar("admin", "contrasena-incorrecta");

        Assert.Null(usuario);
    }

    [Fact]
    public void Autenticar_ConUsuarioInexistente_DevuelveNull()
    {
        var servicio = new IniciarSesionService();

        var usuario = servicio.Autenticar("no-existe", "loquesea");

        Assert.Null(usuario);
    }
}
