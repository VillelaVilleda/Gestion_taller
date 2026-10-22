using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "modificar empleado y rol de administrador" (issue #115).
public class ModificarEmpleadoServiceTests
{
    private static Empleado CrearEmpleado(string nombre, bool esAdministrador) => new()
    {
        Id = Guid.NewGuid(),
        Nombre = nombre,
        NombreUsuario = nombre.ToLowerInvariant().Replace(' ', '.'),
        PasswordUsuario = "hash",
        EsAdministrador = esAdministrador
    };

    private static (ModificarEmpleadoService Servicio, EmpleadoRepositoryFalso Repositorio) Crear(params Empleado[] empleados)
    {
        var repositorio = new EmpleadoRepositoryFalso();
        repositorio.Guardados.AddRange(empleados);
        return (new ModificarEmpleadoService(repositorio), repositorio);
    }

    [Fact]
    public void Ejecutar_ComoAdministrador_CambiaElNombreSinTocarElUsuario()
    {
        var admin = CrearEmpleado("Admin Uno", true);
        var ana = CrearEmpleado("Ana Torres", false);
        var (servicio, repositorio) = Crear(admin, ana);

        servicio.Ejecutar(admin, ana, "  Ana Maria Torres ", false);

        Assert.Equal("Ana Maria Torres", ana.Nombre);
        Assert.Equal("ana.torres", ana.NombreUsuario);
        Assert.Equal(1, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_ComoAdministrador_OtorgaElRolDeAdministrador()
    {
        var admin = CrearEmpleado("Admin Uno", true);
        var ana = CrearEmpleado("Ana Torres", false);
        var (servicio, _) = Crear(admin, ana);

        servicio.Ejecutar(admin, ana, "Ana Torres", true);

        Assert.True(ana.EsAdministrador);
    }

    [Fact]
    public void Ejecutar_ComoAdministrador_QuitaElRolSiQuedaOtroAdministrador()
    {
        var admin = CrearEmpleado("Admin Uno", true);
        var otro = CrearEmpleado("Admin Dos", true);
        var (servicio, _) = Crear(admin, otro);

        servicio.Ejecutar(admin, otro, "Admin Dos", false);

        Assert.False(otro.EsAdministrador);
    }

    [Fact]
    public void Ejecutar_ComoUsuarioComun_LanzaExcepcionYNoCambiaNada()
    {
        var comun = CrearEmpleado("Luis Perez", false);
        var ana = CrearEmpleado("Ana Torres", false);
        var (servicio, repositorio) = Crear(comun, ana);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(comun, ana, "Ana Maria", true));

        Assert.Equal("Ana Torres", ana.Nombre);
        Assert.False(ana.EsAdministrador);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_QuitandoseElRolASiMismo_LanzaExcepcion()
    {
        var admin = CrearEmpleado("Admin Uno", true);
        var otro = CrearEmpleado("Admin Dos", true);
        var (servicio, _) = Crear(admin, otro);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(admin, admin, "Admin Uno", false));

        Assert.True(admin.EsAdministrador);
    }

    [Fact]
    public void Ejecutar_QuitandoElRolAlUnicoAdministrador_LanzaExcepcion()
    {
        var unico = CrearEmpleado("Admin Uno", true);
        var actual = CrearEmpleado("Admin Dos", true); // administrador de la sesion que ya no figura en la lista guardada
        var (servicio, _) = Crear(unico);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(actual, unico, "Admin Uno", false));

        Assert.True(unico.EsAdministrador);
    }

    [Fact]
    public void Ejecutar_ConservandoElRol_PuedeEditarseASiMismo()
    {
        var admin = CrearEmpleado("Admin Uno", true);
        var (servicio, _) = Crear(admin);

        servicio.Ejecutar(admin, admin, "Admin Principal", true);

        Assert.Equal("Admin Principal", admin.Nombre);
        Assert.True(admin.EsAdministrador);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinNombre_LanzaExcepcion(string? nombre)
    {
        var admin = CrearEmpleado("Admin Uno", true);
        var ana = CrearEmpleado("Ana Torres", false);
        var (servicio, _) = Crear(admin, ana);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(admin, ana, nombre!, false));
        Assert.Equal("Ana Torres", ana.Nombre);
    }

    [Fact]
    public void Ejecutar_ConNombreDeMasDe150Caracteres_LanzaExcepcion()
    {
        var admin = CrearEmpleado("Admin Uno", true);
        var ana = CrearEmpleado("Ana Torres", false);
        var (servicio, _) = Crear(admin, ana);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(admin, ana, new string('a', 151), false));
    }

    [Fact]
    public void Ejecutar_SinUsuarioActualOSinEmpleado_LanzaExcepcion()
    {
        var admin = CrearEmpleado("Admin Uno", true);
        var (servicio, _) = Crear(admin);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, admin, "Admin Uno", true));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(admin, null!, "Admin Uno", true));
    }
}
