using GestorTaller.Datos;
using GestorTaller.Negocio;
using Microsoft.EntityFrameworkCore;

namespace GestorTaller.Tests;

// Pruebas del repositorio de Repuesto con EF Core (issue #46).
public class RepuestoRepositoryEfCoreTests
{
    private static GestorTallerDbContext CrearDbContextEnMemoria()
    {
        var opciones = new DbContextOptionsBuilder<GestorTallerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new GestorTallerDbContext(opciones);
    }

    [Fact]
    public void Agregar_GuardaElRepuestoEnLaBaseDeDatos()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new RepuestoRepositoryEfCore(dbContext);
        var repuesto = new Repuesto { Id = Guid.NewGuid(), Nombre = "Filtro de aceite", Existencia = 10 };

        repositorio.Agregar(repuesto);

        Assert.Single(dbContext.Repuestos);
    }

    [Fact]
    public void ObtenerPorId_DevuelveElRepuestoCorrecto()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new RepuestoRepositoryEfCore(dbContext);
        var repuesto = new Repuesto { Id = Guid.NewGuid(), Nombre = "Bujia", Existencia = 25 };
        repositorio.Agregar(repuesto);

        var encontrado = repositorio.ObtenerPorId(repuesto.Id);

        Assert.NotNull(encontrado);
        Assert.Equal("Bujia", encontrado!.Nombre);
        Assert.Equal(25, encontrado.Existencia);
    }

    [Fact]
    public void ObtenerPorId_ConIdInexistente_DevuelveNull()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new RepuestoRepositoryEfCore(dbContext);

        var encontrado = repositorio.ObtenerPorId(Guid.NewGuid());

        Assert.Null(encontrado);
    }

    [Fact]
    public void ObtenerTodos_DevuelveTodosLosRepuestosGuardados()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new RepuestoRepositoryEfCore(dbContext);
        repositorio.Agregar(new Repuesto { Id = Guid.NewGuid(), Nombre = "Filtro de aire", Existencia = 5 });
        repositorio.Agregar(new Repuesto { Id = Guid.NewGuid(), Nombre = "Pastillas de freno", Existencia = 8 });

        var todos = repositorio.ObtenerTodos();

        Assert.Equal(2, todos.Count);
    }

    [Fact]
    public void Agregar_GuardaExistenciaEnCero()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new RepuestoRepositoryEfCore(dbContext);
        var repuesto = new Repuesto { Id = Guid.NewGuid(), Nombre = "Correa de distribucion", Existencia = 0 };

        repositorio.Agregar(repuesto);
        var encontrado = repositorio.ObtenerPorId(repuesto.Id);

        Assert.Equal(0, encontrado!.Existencia);
    }
}