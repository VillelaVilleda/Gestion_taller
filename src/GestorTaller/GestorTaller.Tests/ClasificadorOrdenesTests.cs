using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas de la separación del listado en órdenes pendientes y cerradas (issue #149).
public class ClasificadorOrdenesTests
{
    private static Orden CrearOrden(EstadoOrden estado) => new()
    {
        Id = Guid.NewGuid(),
        Cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" },
        DescripcionObjeto = "Vehiculo sedan color rojo",
        DescripcionProblema = "El vehiculo no enciende",
        Estado = estado,
        FechaRecepcion = DateTime.Now,
        HistorialEstados = new List<EstadoOrden> { estado }
    };

    [Theory]
    [InlineData(EstadoOrden.Recepcion)]
    [InlineData(EstadoOrden.Diagnostico)]
    [InlineData(EstadoOrden.Cotizacion)]
    [InlineData(EstadoOrden.EnReparacion)]
    [InlineData(EstadoOrden.Terminado)]
    public void EstaCerrada_ConUnaEtapaSinEntregar_EsFalso(EstadoOrden estado)
    {
        Assert.False(ClasificadorOrdenes.EstaCerrada(CrearOrden(estado)));
    }

    [Fact]
    public void EstaCerrada_ConUnaOrdenEntregada_EsVerdadero()
    {
        Assert.True(ClasificadorOrdenes.EstaCerrada(CrearOrden(EstadoOrden.Entregado)));
    }

    [Fact]
    public void PendientesYCerradas_SeparanLasOrdenesSinPerderNinguna()
    {
        var recepcion = CrearOrden(EstadoOrden.Recepcion);
        var terminada = CrearOrden(EstadoOrden.Terminado);
        var entregada = CrearOrden(EstadoOrden.Entregado);
        var otraEntregada = CrearOrden(EstadoOrden.Entregado);
        var ordenes = new[] { recepcion, entregada, terminada, otraEntregada };

        var pendientes = ClasificadorOrdenes.Pendientes(ordenes);
        var cerradas = ClasificadorOrdenes.Cerradas(ordenes);

        Assert.Equal(new[] { recepcion, terminada }, pendientes);
        Assert.Equal(new[] { entregada, otraEntregada }, cerradas);
        Assert.Equal(ordenes.Length, pendientes.Count + cerradas.Count);
    }

    [Fact]
    public void PendientesYCerradas_SinOrdenes_DevuelvenListasVacias()
    {
        Assert.Empty(ClasificadorOrdenes.Pendientes(Array.Empty<Orden>()));
        Assert.Empty(ClasificadorOrdenes.Cerradas(Array.Empty<Orden>()));
    }

    [Fact]
    public void EstaCerrada_SinOrden_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => ClasificadorOrdenes.EstaCerrada(null!));
    }
}
