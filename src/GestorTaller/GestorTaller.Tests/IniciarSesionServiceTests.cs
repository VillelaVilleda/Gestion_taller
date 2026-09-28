using GestorTaller.Datos;
using GestorTaller.Negocio;
using Microsoft.EntityFrameworkCore;

namespace GestorTaller.Tests;

public class IniciarSesionServiceTests
{
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
            Nombre = "Administrador",
            NombreUsuario = "admin",
            PasswordUsuario = PasswordHasher.Hash("admin123"),
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
    public void Autenticar_ConDatosDeAdmin_DevuelveEmpleadoAdministrador()
    {
        var servicio = new IniciarSesionService(CrearRepositorioConEmpleadosDePrueba());

        var empleado = servicio.Autenticar("admin", "admin123");

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

        var empleado = servicio.Autenticar("admin", "contrasena-incorrecta");

        Assert.Null(empleado);
    }

    [Fact]
    public void Autenticar_ConUsuarioInexistente_DevuelveNull()
    {
        var servicio = new IniciarSesionService(CrearRepositorioConEmpleadosDePrueba());

        var empleado = servicio.Autenticar("no-existe", "loquesea");

        Assert.Null(empleado);
    }
}