using GestorTaller.Datos;
using GestorTaller.Negocio;
using Microsoft.EntityFrameworkCore;

namespace GestorTaller.Tests;

// Pruebas del repositorio de Empleado con EF Core (issue #44).
public class EmpleadoRepositoryEfCoreTests
{
    private static GestorTallerDbContext CrearDbContextEnMemoria()
    {
        var opciones = new DbContextOptionsBuilder<GestorTallerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new GestorTallerDbContext(opciones);
    }

    [Fact]
    public void Agregar_GuardaElEmpleadoEnLaBaseDeDatos()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new EmpleadoRepositoryEfCore(dbContext);
        var empleado = new Empleado
        {
            Id = Guid.NewGuid(),
            Nombre = "Carlos Lopez",
            NombreUsuario = "clopez",
            PasswordUsuario = "clave123",
            EsAdministrador = false
        };

        repositorio.Agregar(empleado);

        Assert.Single(dbContext.Empleados);
    }

    [Fact]
    public void ObtenerPorId_DevuelveElEmpleadoCorrecto()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new EmpleadoRepositoryEfCore(dbContext);
        var empleado = new Empleado { Id = Guid.NewGuid(), Nombre = "Ana Torres", NombreUsuario = "atorres", PasswordUsuario = "clave123" };
        repositorio.Agregar(empleado);

        var encontrado = repositorio.ObtenerPorId(empleado.Id);

        Assert.NotNull(encontrado);
        Assert.Equal("Ana Torres", encontrado!.Nombre);
    }

    [Fact]
    public void ObtenerPorNombreUsuario_DevuelveElEmpleadoCorrecto()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new EmpleadoRepositoryEfCore(dbContext);
        var empleado = new Empleado { Id = Guid.NewGuid(), Nombre = "Ana Torres", NombreUsuario = "atorres", PasswordUsuario = "clave123" };
        repositorio.Agregar(empleado);

        var encontrado = repositorio.ObtenerPorNombreUsuario("atorres");

        Assert.NotNull(encontrado);
        Assert.Equal(empleado.Id, encontrado!.Id);
    }

    [Fact]
    public void ObtenerPorNombreUsuario_ConUsuarioInexistente_DevuelveNull()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new EmpleadoRepositoryEfCore(dbContext);

        var encontrado = repositorio.ObtenerPorNombreUsuario("no-existe");

        Assert.Null(encontrado);
    }

    [Fact]
    public void ObtenerTodos_DevuelveTodosLosEmpleadosGuardados()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new EmpleadoRepositoryEfCore(dbContext);
        repositorio.Agregar(new Empleado { Id = Guid.NewGuid(), Nombre = "A", NombreUsuario = "a", PasswordUsuario = "x" });
        repositorio.Agregar(new Empleado { Id = Guid.NewGuid(), Nombre = "B", NombreUsuario = "b", PasswordUsuario = "x" });

        var todos = repositorio.ObtenerTodos();

        Assert.Equal(2, todos.Count);
    }

    [Fact]
    public void Agregar_GuardaEsAdministradorCorrectamente()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new EmpleadoRepositoryEfCore(dbContext);
        var empleado = new Empleado { Id = Guid.NewGuid(), Nombre = "Admin", NombreUsuario = "admin2", PasswordUsuario = "x", EsAdministrador = true };

        repositorio.Agregar(empleado);
        var encontrado = repositorio.ObtenerPorId(empleado.Id);

        Assert.True(encontrado!.EsAdministrador);
    }
}
