using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "cambiar la propia contrasena" (issue #114).
public class CambiarContrasenaServiceTests
{
    private static Empleado CrearEmpleado() => new()
    {
        Id = Guid.NewGuid(),
        Nombre = "Ana Torres",
        NombreUsuario = "ana.torres",
        PasswordUsuario = PasswordHasher.Hash("vieja123")
    };

    [Fact]
    public void Ejecutar_ConDatosValidos_GuardaLaNuevaContrasenaConHash()
    {
        var repositorio = new EmpleadoRepositoryFalso();
        var empleado = CrearEmpleado();

        new CambiarContrasenaService(repositorio).Ejecutar(empleado, "vieja123", "nueva456");

        Assert.NotEqual("nueva456", empleado.PasswordUsuario);
        Assert.True(PasswordHasher.Verificar("nueva456", empleado.PasswordUsuario));
        Assert.Equal(1, repositorio.Actualizaciones);
    }

    [Theory]
    [InlineData("incorrecta")]
    [InlineData("")]
    [InlineData(null)]
    public void Ejecutar_ConContrasenaActualIncorrecta_LanzaExcepcionYNoCambiaNada(string? actual)
    {
        var repositorio = new EmpleadoRepositoryFalso();
        var empleado = CrearEmpleado();

        Assert.Throws<InvalidOperationException>(() => new CambiarContrasenaService(repositorio).Ejecutar(empleado, actual!, "nueva456"));

        Assert.True(PasswordHasher.Verificar("vieja123", empleado.PasswordUsuario));
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    [InlineData("corta")]
    [InlineData("vieja123")]
    public void Ejecutar_ConContrasenaNuevaInvalida_LanzaExcepcion(string? nueva)
    {
        var servicio = new CambiarContrasenaService(new EmpleadoRepositoryFalso());

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(CrearEmpleado(), "vieja123", nueva!));
    }

    [Fact]
    public void Ejecutar_ConContrasenaNuevaDemasiadoLarga_LanzaYNoCambiaNada()
    {
        var repositorio = new EmpleadoRepositoryFalso();
        var empleado = CrearEmpleado();

        Assert.Throws<ArgumentException>(() => new CambiarContrasenaService(repositorio).Ejecutar(empleado, "vieja123", new string('a', ValidadorContrasena.LongitudMaxima + 1)));

        Assert.True(PasswordHasher.Verificar("vieja123", empleado.PasswordUsuario));
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_SinEmpleado_LanzaExcepcion()
    {
        var servicio = new CambiarContrasenaService(new EmpleadoRepositoryFalso());

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, "vieja123", "nueva456"));
    }
}
