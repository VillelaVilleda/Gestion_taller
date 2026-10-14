using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "agregar repuesto a la cotizacion de una orden" (issues #70 y #103).
// El repuesto se agrega durante la Cotizacion: no se descuenta del inventario
// (eso ocurre cuando el cliente acepta la cotizacion).
public class AsignarRepuestoOrdenServiceTests
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

    private static Repuesto CrearRepuesto(int existencia, decimal precio = 0m)
    {
        return new Repuesto { Id = Guid.NewGuid(), Nombre = "Filtro de aceite", Existencia = existencia, Precio = precio };
    }

    [Fact]
    public void Ejecutar_EnCotizacionConDatosValidos_AgregaElRepuestoALaOrden()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var repuesto = CrearRepuesto(10);

        new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, 3);

        var asignado = Assert.Single(orden.Repuestos);
        Assert.Equal(repuesto.Id, asignado.RepuestoId);
        Assert.Equal(orden.Id, asignado.OrdenId);
        Assert.Equal(3, asignado.Cantidad);
    }

    [Fact]
    public void Ejecutar_NoDescuentaLaExistencia()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var repuesto = CrearRepuesto(10);

        new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, 3);

        Assert.Equal(10, repuesto.Existencia);
    }

    [Fact]
    public void Ejecutar_GuardaElPrecioDelRepuestoEnLaAsignacion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var repuesto = CrearRepuesto(10, 25.50m);

        new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, 2);

        Assert.Equal(25.50m, Assert.Single(orden.Repuestos).PrecioUnitario);
    }

    [Fact]
    public void Ejecutar_SiCambiaElPrecioDespues_ElPrecioYaAsignadoNoCambia()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var repuesto = CrearRepuesto(10, 25.50m);
        new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, 2);

        repuesto.Precio = 40m;
        new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, 1);

        Assert.Equal(25.50m, Assert.Single(orden.Repuestos).PrecioUnitario);
    }

    [Fact]
    public void Ejecutar_ConCantidadIgualALaExistencia_EsValido()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var repuesto = CrearRepuesto(4);

        new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, 4);

        Assert.Equal(4, Assert.Single(orden.Repuestos).Cantidad);
    }

    [Fact]
    public void Ejecutar_ConCantidadMayorALaExistencia_LanzaExcepcionYNoCambiaNada()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var repuesto = CrearRepuesto(2);

        Assert.Throws<InvalidOperationException>(() => new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, 3));

        Assert.Empty(orden.Repuestos);
    }

    [Fact]
    public void Ejecutar_SiLaSumaConLoYaAgregadoSuperaLaExistencia_LanzaExcepcionYNoCambiaNada()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var repuesto = CrearRepuesto(5);
        var servicio = new AsignarRepuestoOrdenService();
        servicio.Ejecutar(orden, repuesto, 3);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(orden, repuesto, 3));

        Assert.Equal(3, Assert.Single(orden.Repuestos).Cantidad);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Ejecutar_ConCantidadNoPositiva_LanzaExcepcion(int cantidad)
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var repuesto = CrearRepuesto(10);

        Assert.Throws<ArgumentException>(() => new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, cantidad));
    }

    [Theory]
    [InlineData(EstadoOrden.Recepcion)]
    [InlineData(EstadoOrden.Diagnostico)]
    [InlineData(EstadoOrden.EnReparacion)]
    [InlineData(EstadoOrden.Terminado)]
    [InlineData(EstadoOrden.Entregado)]
    public void Ejecutar_DesdeEstadoDistintoACotizacion_LanzaExcepcion(EstadoOrden estado)
    {
        var orden = CrearOrdenEnEstado(estado);
        var repuesto = CrearRepuesto(10);

        Assert.Throws<InvalidOperationException>(() => new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, 1));
    }

    [Fact]
    public void Ejecutar_ConElMismoRepuestoDosVeces_SumaLaCantidad()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var repuesto = CrearRepuesto(10);
        var servicio = new AsignarRepuestoOrdenService();

        servicio.Ejecutar(orden, repuesto, 2);
        servicio.Ejecutar(orden, repuesto, 3);

        Assert.Equal(5, Assert.Single(orden.Repuestos).Cantidad);
    }

    [Fact]
    public void Ejecutar_SinOrden_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => new AsignarRepuestoOrdenService().Ejecutar(null!, CrearRepuesto(10), 1));
    }

    [Fact]
    public void Ejecutar_SinRepuesto_LanzaExcepcion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);

        Assert.Throws<ArgumentException>(() => new AsignarRepuestoOrdenService().Ejecutar(orden, null!, 1));
    }
}
