using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "asignar repuesto a una orden" (issue #70).
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

    private static Repuesto CrearRepuesto(int existencia)
    {
        return new Repuesto { Id = Guid.NewGuid(), Nombre = "Filtro de aceite", Existencia = existencia };
    }

    [Fact]
    public void Ejecutar_EnReparacionConDatosValidos_AgregaElRepuestoALaOrden()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);
        var repuesto = CrearRepuesto(10);

        new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, 3);

        var asignado = Assert.Single(orden.Repuestos);
        Assert.Equal(repuesto.Id, asignado.RepuestoId);
        Assert.Equal(orden.Id, asignado.OrdenId);
        Assert.Equal(3, asignado.Cantidad);
    }

    [Fact]
    public void Ejecutar_DescuentaLaCantidadDeLaExistencia()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);
        var repuesto = CrearRepuesto(10);

        new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, 3);

        Assert.Equal(7, repuesto.Existencia);
    }

    [Fact]
    public void Ejecutar_ConCantidadIgualALaExistencia_DejaLaExistenciaEnCero()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);
        var repuesto = CrearRepuesto(4);

        new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, 4);

        Assert.Equal(0, repuesto.Existencia);
    }

    [Fact]
    public void Ejecutar_ConCantidadMayorALaExistencia_LanzaExcepcionYNoCambiaNada()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);
        var repuesto = CrearRepuesto(2);

        Assert.Throws<InvalidOperationException>(() => new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, 3));

        Assert.Equal(2, repuesto.Existencia);
        Assert.Empty(orden.Repuestos);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Ejecutar_ConCantidadNoPositiva_LanzaExcepcion(int cantidad)
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);
        var repuesto = CrearRepuesto(10);

        Assert.Throws<ArgumentException>(() => new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, cantidad));
    }

    [Theory]
    [InlineData(EstadoOrden.Recepcion)]
    [InlineData(EstadoOrden.Diagnostico)]
    [InlineData(EstadoOrden.Cotizacion)]
    [InlineData(EstadoOrden.Terminado)]
    [InlineData(EstadoOrden.Entregado)]
    public void Ejecutar_DesdeEstadoDistintoAEnReparacion_LanzaExcepcion(EstadoOrden estado)
    {
        var orden = CrearOrdenEnEstado(estado);
        var repuesto = CrearRepuesto(10);

        Assert.Throws<InvalidOperationException>(() => new AsignarRepuestoOrdenService().Ejecutar(orden, repuesto, 1));
    }

    [Fact]
    public void Ejecutar_ConElMismoRepuestoDosVeces_SumaLaCantidadYDescuentaAmbas()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);
        var repuesto = CrearRepuesto(10);
        var servicio = new AsignarRepuestoOrdenService();

        servicio.Ejecutar(orden, repuesto, 2);
        servicio.Ejecutar(orden, repuesto, 3);

        var asignado = Assert.Single(orden.Repuestos);
        Assert.Equal(5, asignado.Cantidad);
        Assert.Equal(5, repuesto.Existencia);
    }

    [Fact]
    public void Ejecutar_SinOrden_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => new AsignarRepuestoOrdenService().Ejecutar(null!, CrearRepuesto(10), 1));
    }

    [Fact]
    public void Ejecutar_SinRepuesto_LanzaExcepcion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);

        Assert.Throws<ArgumentException>(() => new AsignarRepuestoOrdenService().Ejecutar(orden, null!, 1));
    }
}
