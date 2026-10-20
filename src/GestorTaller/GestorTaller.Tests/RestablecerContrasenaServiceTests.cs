using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "restablecer contrasena" (issue #114).
public class RestablecerContrasenaServiceTests
{
    private static Empleado CrearEmpleado(bool esAdministrador = false) => new()
    {
        Id = Guid.NewGuid(),
        Nombre = "Ana Torres",
        NombreUsuario = "ana.torres",
        PasswordUsuario = PasswordHasher.Hash("vieja123"),
        EsAdministrador = esAdministrador
    };

    [Fact]
    public void Ejecutar_ComoAdministrador_GeneraUnaContrasenaTemporalYLaGuardaConHash()
    {
        var repositorio = new EmpleadoRepositoryFalso();
        var servicio = new RestablecerContrasenaService(repositorio);
        var empleado = CrearEmpleado();

        var password = servicio.Ejecutar(new PermisosUsuario(CrearEmpleado(esAdministrador: true)), empleado);

        Assert.Equal(GeneradorContrasena.Longitud, password.Length);
        Assert.NotEqual(password, empleado.PasswordUsuario);
        Assert.True(PasswordHasher.Verificar(password, empleado.PasswordUsuario));
        Assert.False(PasswordHasher.Verificar("vieja123", empleado.PasswordUsuario));
        Assert.Equal(1, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_ComoUsuarioComun_LanzaExcepcionYNoCambiaNada()
    {
        var repositorio = new EmpleadoRepositoryFalso();
        var servicio = new RestablecerContrasenaService(repositorio);
        var empleado = CrearEmpleado();

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(new PermisosUsuario(CrearEmpleado()), empleado));

        Assert.True(PasswordHasher.Verificar("vieja123", empleado.PasswordUsuario));
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_SinEmpleadoOSinPermisos_LanzaExcepcion()
    {
        var servicio = new RestablecerContrasenaService(new EmpleadoRepositoryFalso());

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(new PermisosUsuario(CrearEmpleado(true)), null!));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, CrearEmpleado()));
    }
}
