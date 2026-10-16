using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "registrar entrega de una orden" (issue #28).
public class RegistrarEntregaOrdenServiceTests
{
    private static readonly Empleado empleado = new() { Id = Guid.NewGuid(), Nombre = "Ana Torres" };

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

    private static RegistrarEntregaOrdenService CrearServicio()
    {
        return new RegistrarEntregaOrdenService(new AvanzarEstadoOrdenService());
    }

    [Fact]
    public void Ejecutar_DesdeTerminadoConDatosValidos_AvanzaAEntregado()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Terminado);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, empleado, 450m);

        Assert.Equal(EstadoOrden.Entregado, orden.Estado);
    }

    [Fact]
    public void Ejecutar_DesdeTerminadoConDatosValidos_GuardaEmpleadoMontoYFecha()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Terminado);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, empleado, 450m);

        Assert.Same(empleado, orden.EmpleadoEntrega);
        Assert.Equal(450m, orden.MontoPagado);
        Assert.NotNull(orden.FechaEntrega);
    }

    [Fact]
    public void Ejecutar_DesdeTerminadoConDatosValidos_AgregaEntregadoAlHistorial()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Terminado);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, empleado, 450m);

        Assert.Contains(EstadoOrden.Entregado, orden.HistorialEstados);
    }

    [Theory]
    [InlineData(EstadoOrden.Recepcion)]
    [InlineData(EstadoOrden.Diagnostico)]
    [InlineData(EstadoOrden.Cotizacion)]
    [InlineData(EstadoOrden.EnReparacion)]
    [InlineData(EstadoOrden.Entregado)]
    public void Ejecutar_DesdeEstadoDistintoATerminado_LanzaExcepcion(EstadoOrden estado)
    {
        var orden = CrearOrdenEnEstado(estado);
        var servicio = CrearServicio();

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(orden, empleado, 450m));
    }

    [Fact]
    public void Ejecutar_SinEmpleado_LanzaExcepcion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Terminado);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(orden, null!, 450m));
    }

    [Fact]
    public void Ejecutar_ConMontoCero_AvanzaAEntregado()
    {
        // El diagnostico pudo ser gratuito y el cliente pudo rechazar la
        // reparacion: el total a pagar valido puede ser 0.
        var orden = CrearOrdenEnEstado(EstadoOrden.Terminado);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, empleado, 0m);

        Assert.Equal(EstadoOrden.Entregado, orden.Estado);
        Assert.Equal(0m, orden.MontoPagado);
    }

    [Fact]
    public void Ejecutar_ConMontoNegativo_LanzaExcepcion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Terminado);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(orden, empleado, -10m));
    }

    [Fact]
    public void Ejecutar_SinOrden_LanzaExcepcion()
    {
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, empleado, 450m));
    }
}
