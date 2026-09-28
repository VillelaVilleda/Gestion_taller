using GestorTaller.Datos;
using GestorTaller.Negocio;
using Microsoft.EntityFrameworkCore;

namespace GestorTaller.Tests;

// Pruebas del repositorio de Orden con EF Core (issue #45).
public class OrdenRepositoryEfCoreTests
{
    private static GestorTallerDbContext CrearDbContextEnMemoria()
    {
        var opciones = new DbContextOptionsBuilder<GestorTallerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new GestorTallerDbContext(opciones);
    }

    private static Orden CrearOrdenDePrueba(Cliente cliente, params EstadoOrden[] historial)
    {
        return new Orden
        {
            Id = Guid.NewGuid(),
            Cliente = cliente,
            DescripcionObjeto = "Vehiculo sedan color rojo",
            DescripcionProblema = "El vehiculo no enciende",
            CostoDiagnostico = 15m,
            Estado = historial[^1],
            FechaRecepcion = DateTime.Now,
            HistorialEstados = historial.ToList()
        };
    }

    [Fact]
    public void Agregar_GuardaLaOrdenEnLaBaseDeDatos()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };
        dbContext.Clientes.Add(cliente);
        dbContext.SaveChanges();

        var repositorio = new OrdenRepositoryEfCore(dbContext);
        var orden = CrearOrdenDePrueba(cliente, EstadoOrden.Recepcion);

        repositorio.Agregar(orden);

        Assert.Single(dbContext.Ordenes);
    }

    [Fact]
    public void Agregar_GuardaElHistorialCompleto()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };
        dbContext.Clientes.Add(cliente);
        dbContext.SaveChanges();

        var repositorio = new OrdenRepositoryEfCore(dbContext);
        var orden = CrearOrdenDePrueba(cliente, EstadoOrden.Recepcion, EstadoOrden.Diagnostico, EstadoOrden.Cotizacion);

        repositorio.Agregar(orden);
        var encontrada = repositorio.ObtenerPorId(orden.Id);

        Assert.Equal(
            new[] { EstadoOrden.Recepcion, EstadoOrden.Diagnostico, EstadoOrden.Cotizacion },
            encontrada!.HistorialEstados);
    }

    [Fact]
    public void Agregar_ConCotizacionRechazada_GuardaElHistorialSaltandoReparacion()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };
        dbContext.Clientes.Add(cliente);
        dbContext.SaveChanges();

        var repositorio = new OrdenRepositoryEfCore(dbContext);
        var orden = CrearOrdenDePrueba(cliente, EstadoOrden.Recepcion, EstadoOrden.Diagnostico, EstadoOrden.Cotizacion, EstadoOrden.Terminado);

        repositorio.Agregar(orden);
        var encontrada = repositorio.ObtenerPorId(orden.Id);

        Assert.DoesNotContain(EstadoOrden.EnReparacion, encontrada!.HistorialEstados);
        Assert.Equal(EstadoOrden.Terminado, encontrada.HistorialEstados[^1]);
    }

    [Fact]
    public void ObtenerPorId_IncluyeElClienteAsociado()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Maria Lopez" };
        dbContext.Clientes.Add(cliente);
        dbContext.SaveChanges();

        var repositorio = new OrdenRepositoryEfCore(dbContext);
        var orden = CrearOrdenDePrueba(cliente, EstadoOrden.Recepcion);
        repositorio.Agregar(orden);

        var encontrada = repositorio.ObtenerPorId(orden.Id);

        Assert.Equal("Maria Lopez", encontrada!.Cliente.Nombre);
    }

    [Fact]
    public void ObtenerPorId_ConIdInexistente_DevuelveNull()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var repositorio = new OrdenRepositoryEfCore(dbContext);

        var encontrada = repositorio.ObtenerPorId(Guid.NewGuid());

        Assert.Null(encontrada);
    }

    [Fact]
    public void ObtenerTodas_DevuelveTodasLasOrdenesConSuHistorial()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };
        dbContext.Clientes.Add(cliente);
        dbContext.SaveChanges();

        var repositorio = new OrdenRepositoryEfCore(dbContext);
        repositorio.Agregar(CrearOrdenDePrueba(cliente, EstadoOrden.Recepcion));
        repositorio.Agregar(CrearOrdenDePrueba(cliente, EstadoOrden.Recepcion, EstadoOrden.Diagnostico));

        var todas = repositorio.ObtenerTodas();

        Assert.Equal(2, todas.Count);
        Assert.Contains(todas, o => o.HistorialEstados.Count == 1);
        Assert.Contains(todas, o => o.HistorialEstados.Count == 2);
    }

    [Fact]
    public void Actualizar_GuardaLosCambiosDeLaOrden()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };
        dbContext.Clientes.Add(cliente);
        dbContext.SaveChanges();

        var repositorio = new OrdenRepositoryEfCore(dbContext);
        var orden = CrearOrdenDePrueba(cliente, EstadoOrden.Recepcion);
        repositorio.Agregar(orden);

        orden.Estado = EstadoOrden.Diagnostico;
        orden.HistorialEstados.Add(EstadoOrden.Diagnostico);
        repositorio.Actualizar(orden);

        var encontrada = repositorio.ObtenerPorId(orden.Id);
        Assert.Equal(EstadoOrden.Diagnostico, encontrada!.Estado);
    }

    [Fact]
    public void Actualizar_AgregaSoloLasEntradasNuevasDelHistorial()
    {
        using var dbContext = CrearDbContextEnMemoria();
        var cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" };
        dbContext.Clientes.Add(cliente);
        dbContext.SaveChanges();

        var repositorio = new OrdenRepositoryEfCore(dbContext);
        var orden = CrearOrdenDePrueba(cliente, EstadoOrden.Recepcion);
        repositorio.Agregar(orden);

        orden.Estado = EstadoOrden.Diagnostico;
        orden.HistorialEstados.Add(EstadoOrden.Diagnostico);
        repositorio.Actualizar(orden);

        orden.Estado = EstadoOrden.Cotizacion;
        orden.HistorialEstados.Add(EstadoOrden.Cotizacion);
        repositorio.Actualizar(orden);

        var encontrada = repositorio.ObtenerPorId(orden.Id);
        Assert.Equal(
            new[] { EstadoOrden.Recepcion, EstadoOrden.Diagnostico, EstadoOrden.Cotizacion },
            encontrada!.HistorialEstados);
    }
}