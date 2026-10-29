using GestorTaller.Datos;
using GestorTaller.Negocio;
using Microsoft.EntityFrameworkCore;

namespace GestorTaller.Tests;

public class IniciarSesionServiceTests
{
    // Contraseña del administrador oculto, solo para estas pruebas.
    private const string PasswordAdministradorOculto = "201944260";

    private static IEmpleadoRepository CrearRepositorioConEmpleadosDePrueba()
    {
        var opciones = new DbContextOptionsBuilder<GestorTallerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var dbContext = new GestorTallerDbContext(opciones);
        var repositorio = new EmpleadoRepositoryEfCore(dbContext);

        repositorio.Agregar(new Empleado
        {
            Id = Guid.NewGuid(),
            Nombre = "Jefe de taller",
            NombreUsuario = "jefe",
            PasswordUsuario = PasswordHasher.Hash("jefe123"),
            EsAdministrador = true
        });
        repositorio.Agregar(new Empleado
        {
            Id = Guid.NewGuid(),
            Nombre = "Empleado de prueba",
            NombreUsuario = "usuario",
            PasswordUsuario = PasswordHasher.Hash("usuario123"),
            EsAdministrador = false
        });

        return repositorio;
    }

    [Fact]
    public void Autenticar_ConDatosDeAdministrador_DevuelveEmpleadoAdministrador()
    {
        var servicio = new IniciarSesionService(CrearRepositorioConEmpleadosDePrueba());

        var empleado = servicio.Autenticar("jefe", "jefe123");

        Assert.NotNull(empleado);
        Assert.True(empleado!.EsAdministrador);
    }

    [Fact]
    public void Autenticar_ConDatosDeEmpleadoComun_DevuelveEmpleadoSinAdministrador()
    {
        var servicio = new IniciarSesionService(CrearRepositorioConEmpleadosDePrueba());

        var empleado = servicio.Autenticar("usuario", "usuario123");

        Assert.NotNull(empleado);
        Assert.False(empleado!.EsAdministrador);
    }

    [Fact]
    public void Autenticar_ConContrasenaIncorrecta_DevuelveNull()
    {
        var servicio = new IniciarSesionService(CrearRepositorioConEmpleadosDePrueba());

        var empleado = servicio.Autenticar("jefe", "contrasena-incorrecta");

        Assert.Null(empleado);
    }

    [Fact]
    public void Autenticar_ConUsuarioInexistente_DevuelveNull()
    {
        var servicio = new IniciarSesionService(CrearRepositorioConEmpleadosDePrueba());

        var empleado = servicio.Autenticar("no-existe", "loquesea");

        Assert.Null(empleado);
    }

    [Fact]
    public void Autenticar_ConDatosDelAdministradorOculto_DevuelveAdministradorSinFilaEnLaBaseDeDatos()
    {
        var servicio = new IniciarSesionService(CrearRepositorioConEmpleadosDePrueba());

        var empleado = servicio.Autenticar("admin", PasswordAdministradorOculto);

        Assert.NotNull(empleado);
        Assert.True(empleado!.EsAdministrador);
        Assert.True(AdministradorOculto.EsEste(empleado));
    }

    [Theory]
    [InlineData("ADMIN")]
    [InlineData("  admin ")]
    public void Autenticar_ConElUsuarioReservadoEscritoDistinto_TambienEntra(string usuario)
    {
        var servicio = new IniciarSesionService(CrearRepositorioConEmpleadosDePrueba());

        Assert.NotNull(servicio.Autenticar(usuario, PasswordAdministradorOculto));
    }

    [Theory]
    [InlineData("incorrecta")]
    [InlineData("")]
    [InlineData("admin123")]
    public void Autenticar_ConElUsuarioReservadoYContrasenaIncorrecta_DevuelveNull(string password)
    {
        var servicio = new IniciarSesionService(CrearRepositorioConEmpleadosDePrueba());

        Assert.Null(servicio.Autenticar("admin", password));
    }

    [Fact]
    public void Autenticar_ConUnEmpleadoDeLaBaseLlamadoAdmin_SeIgnoraYSoloEntraElOculto()
    {
        var repositorio = CrearRepositorioConEmpleadosDePrueba();
        repositorio.Agregar(new Empleado
        {
            Id = Guid.NewGuid(),
            Nombre = "Intruso",
            NombreUsuario = "admin",
            PasswordUsuario = PasswordHasher.Hash("otra-clave"),
            EsAdministrador = false
        });
        var servicio = new IniciarSesionService(repositorio);

        Assert.Null(servicio.Autenticar("admin", "otra-clave"));
        Assert.True(AdministradorOculto.EsEste(servicio.Autenticar("admin", PasswordAdministradorOculto)));
    }
}
