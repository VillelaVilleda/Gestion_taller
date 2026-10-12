using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "quitar repuesto asignado a una orden".
public class QuitarRepuestoOrdenServiceTests
{
    private static Orden CrearOrdenEnEstado(EstadoOrden estado)
    {
        return new Orden
        {
            Id = Guid.NewGuid(),
            Cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" },
            DescripcionObjeto = "Vehiculo sedan color rojo",
            DescripcionProblema = "El vehiculo no enciende",
            CostoDiagnostico = 15m,
            Estado = estado,
            FechaRecepcion = DateTime.Now,
            HistorialEstados = new List<EstadoOrden> { estado }
        };
    }

    private static Repuesto CrearRepuesto(int existencia)
    {
        return new Repuesto { Id = Guid.NewGuid(), Nombre = "Filtro de aceite", Existencia = existencia };
    }

    // Deja una orden EnReparacion con el repuesto ya asignado (y el inventario descontado).
    private static (Orden orden, Repuesto repuesto) CrearOrdenConAsignado(int existenciaInicial, int cantidadAsignada)
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);
        var repuesto = CrearRepuesto(existenciaInicial);
        new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, cantidadAsignada);
        return (orden, repuesto);
    }

    [Fact]
    public void Ejecutar_ConCantidadParcial_RestaLaCantidadAsignada()
    {
        var (orden, repuesto) = CrearOrdenConAsignado(10, 5);

        new QuitarRepuestoOrdenService().Ejecutar(orden, repuesto, 2);

        Assert.Equal(3, Assert.Single(orden.Repuestos).Cantidad);
    }

    [Fact]
    public void Ejecutar_DevuelveLaCantidadAlInventario()
    {
        var (orden, repuesto) = CrearOrdenConAsignado(10, 5);

        new QuitarRepuestoOrdenService().Ejecutar(orden, repuesto, 2);

        Assert.Equal(7, repuesto.Existencia);
    }

    [Fact]
    public void Ejecutar_ConTodaLaCantidad_EliminaLaLineaDeLaOrden()
    {
        var (orden, repuesto) = CrearOrdenConAsignado(10, 5);

        new QuitarRepuestoOrdenService().Ejecutar(orden, repuesto, 5);

        Assert.Empty(orden.Repuestos);
        Assert.Equal(10, repuesto.Existencia);
    }

    [Fact]
    public void Ejecutar_ConOrdenNula_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new QuitarRepuestoOrdenService().Ejecutar(null!, CrearRepuesto(1), 1));
    }

    [Fact]
    public void Ejecutar_ConRepuestoNulo_LanzaArgumentException()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);

        Assert.Throws<ArgumentException>(() => new QuitarRepuestoOrdenService().Ejecutar(orden, null!, 1));
    }

    [Theory]
    [InlineData(EstadoOrden.Recepcion)]
    [InlineData(EstadoOrden.Diagnostico)]
    [InlineData(EstadoOrden.Cotizacion)]
    [InlineData(EstadoOrden.Terminado)]
    [InlineData(EstadoOrden.Entregado)]
    public void Ejecutar_FueraDeEnReparacion_LanzaInvalidOperationException(EstadoOrden estado)
    {
        var (orden, repuesto) = CrearOrdenConAsignado(10, 5);
        orden.Estado = estado;

        Assert.Throws<InvalidOperationException>(() => new QuitarRepuestoOrdenService().Ejecutar(orden, repuesto, 1));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Ejecutar_ConCantidadNoPositiva_LanzaArgumentException(int cantidad)
    {
        var (orden, repuesto) = CrearOrdenConAsignado(10, 5);

        Assert.Throws<ArgumentException>(() => new QuitarRepuestoOrdenService().Ejecutar(orden, repuesto, cantidad));
    }

    [Fact]
    public void Ejecutar_ConRepuestoNoAsignado_LanzaInvalidOperationException()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);
        var repuesto = CrearRepuesto(10);

        Assert.Throws<InvalidOperationException>(() => new QuitarRepuestoOrdenService().Ejecutar(orden, repuesto, 1));
    }

    [Fact]
    public void Ejecutar_ConMasDeLoAsignado_LanzaInvalidOperationException()
    {
        var (orden, repuesto) = CrearOrdenConAsignado(10, 5);

        Assert.Throws<InvalidOperationException>(() => new QuitarRepuestoOrdenService().Ejecutar(orden, repuesto, 6));
    }

    [Fact]
    public void Ejecutar_CuandoFalla_NoModificaOrdenNiInventario()
    {
        var (orden, repuesto) = CrearOrdenConAsignado(10, 5);

        Assert.Throws<InvalidOperationException>(() => new QuitarRepuestoOrdenService().Ejecutar(orden, repuesto, 6));

        Assert.Equal(5, Assert.Single(orden.Repuestos).Cantidad);
        Assert.Equal(5, repuesto.Existencia);
    }
}
