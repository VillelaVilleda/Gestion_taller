using GestorTaller.Datos;
using GestorTaller.Negocio;
using Microsoft.EntityFrameworkCore;

namespace GestorTaller.Tests;

// Pruebas del repositorio de Cliente con EF Core (issue #43).
public class ClienteRepositoryEfCoreTests
{
    private static GestorTallerDbContext CrearDbContextEnMemoria()
    {
        var opciones = new DbContextOptionsBuilder<GestorTallerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new GestorTallerDbContext(opciones);
    }

    [Fact]
    public void Agregar_GuardaElClienteEnLaBaseDeDatos()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new ClienteRepositoryEfCore(dbContext);
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };

        repositorio.Agregar(cliente);

        Assert.Single(dbContext.Clientes);
    }

    [Fact]
    public void ObtenerPorId_DevuelveElClienteCorrecto()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new ClienteRepositoryEfCore(dbContext);
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Maria Lopez" };
        repositorio.Agregar(cliente);

        var encontrado = repositorio.ObtenerPorId(cliente.Id);

        Assert.NotNull(encontrado);
        Assert.Equal("Maria Lopez", encontrado!.Nombre);
    }

    [Fact]
    public void ObtenerPorId_ConIdInexistente_DevuelveNull()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new ClienteRepositoryEfCore(dbContext);

        var encontrado = repositorio.ObtenerPorId(Guid.NewGuid());

        Assert.Null(encontrado);
    }

    [Fact]
    public void ObtenerTodos_DevuelveTodosLosClientesGuardados()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new ClienteRepositoryEfCore(dbContext);
        repositorio.Agregar(new Cliente { Id = Guid.NewGuid(), Nombre = "Cliente A" });
        repositorio.Agregar(new Cliente { Id = Guid.NewGuid(), Nombre = "Cliente B" });

        var todos = repositorio.ObtenerTodos();

        Assert.Equal(2, todos.Count);
    }

    [Fact]
    public void Agregar_GuardaTelefonoCorreoYDireccion()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new ClienteRepositoryEfCore(dbContext);
        var cliente = new Cliente
        {
            Id = Guid.NewGuid(),
            Nombre = "Ana Torres",
            Telefono = "12345678",
            Correo = "ana@correo.com",
            Direccion = "Zona 1"
        };

        repositorio.Agregar(cliente);
        var encontrado = repositorio.ObtenerPorId(cliente.Id);

        Assert.Equal("12345678", encontrado!.Telefono);
        Assert.Equal("ana@correo.com", encontrado.Correo);
        Assert.Equal("Zona 1", encontrado.Direccion);
    }
}
