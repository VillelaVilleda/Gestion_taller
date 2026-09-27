using GestorTaller.Negocio;

namespace GestorTaller.Tests;

public class IniciarSesionServiceTests
{
    [Fact]
    public void Autenticar_ConDatosDeAdmin_DevuelveEmpleadoAdministrador()
    {
        var servicio = new IniciarSesionService();

        var empleado = servicio.Autenticar("admin", "admin123");

        Assert.NotNull(empleado);
        Assert.True(empleado!.EsAdministrador);
    }

    [Fact]
    public void Autenticar_ConDatosDeEmpleadoComun_DevuelveEmpleadoSinAdministrador()
    {
        var servicio = new IniciarSesionService();

        var empleado = servicio.Autenticar("usuario", "usuario123");

        Assert.NotNull(empleado);
        Assert.False(empleado!.EsAdministrador);
    }

    [Fact]
    public void Autenticar_ConContrasenaIncorrecta_DevuelveNull()
    {
        var servicio = new IniciarSesionService();

        var empleado = servicio.Autenticar("admin", "contrasena-incorrecta");

        Assert.Null(empleado);
    }

    [Fact]
    public void Autenticar_ConUsuarioInexistente_DevuelveNull()
    {
        var servicio = new IniciarSesionService();

        var empleado = servicio.Autenticar("no-existe", "loquesea");

        Assert.Null(empleado);
    }
}
