using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "avanzar estado de una orden" (issue #21).
// Estan en rojo a proposito: AvanzarEstadoOrdenService todavia no existe.
// Crear el archivo AvanzarEstadoOrdenService.cs con el codigo minimo para
// que estas pruebas pasen (fase Green), luego refactorizar si hace falta.
public class AvanzarEstadoOrdenServiceTests
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

    [Fact]
    public void Ejecutar_DesdeRecepcion_AvanzaADiagnostico()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Recepcion);
        var servicio = new AvanzarEstadoOrdenService();

        servicio.Ejecutar(orden);

        Assert.Equal(EstadoOrden.Diagnostico, orden.Estado);
    }

    [Fact]
    public void Ejecutar_DesdeDiagnostico_AvanzaACotizacion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = new AvanzarEstadoOrdenService();

        servicio.Ejecutar(orden);

        Assert.Equal(EstadoOrden.Cotizacion, orden.Estado);
    }

    [Fact]
    public void Ejecutar_DesdeCotizacionAceptada_AvanzaAEnReparacion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var servicio = new AvanzarEstadoOrdenService();

        servicio.Ejecutar(orden, cotizacionAceptada: true);

        Assert.Equal(EstadoOrden.EnReparacion, orden.Estado);
    }

    [Fact]
    public void Ejecutar_DesdeCotizacionRechazada_AvanzaATerminadoYSaltaReparacion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var servicio = new AvanzarEstadoOrdenService();

        servicio.Ejecutar(orden, cotizacionAceptada: false);

        Assert.Equal(EstadoOrden.Terminado, orden.Estado);
        Assert.DoesNotContain(EstadoOrden.EnReparacion, orden.HistorialEstados);
    }

    [Fact]
    public void Ejecutar_DesdeEnReparacion_AvanzaATerminado()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);
        var servicio = new AvanzarEstadoOrdenService();

        servicio.Ejecutar(orden);

        Assert.Equal(EstadoOrden.Terminado, orden.Estado);
    }

    [Fact]
    public void Ejecutar_DesdeTerminado_AvanzaAEntregado()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Terminado);
        var servicio = new AvanzarEstadoOrdenService();

        servicio.Ejecutar(orden);

        Assert.Equal(EstadoOrden.Entregado, orden.Estado);
    }

    [Fact]
    public void Ejecutar_DesdeEntregado_LanzaExcepcion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Entregado);
        var servicio = new AvanzarEstadoOrdenService();

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(orden));
    }

    [Fact]
    public void Ejecutar_AgregaElNuevoEstadoAlHistorial()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Recepcion);
        var servicio = new AvanzarEstadoOrdenService();

        servicio.Ejecutar(orden);

        Assert.Equal(new[] { EstadoOrden.Recepcion, EstadoOrden.Diagnostico }, orden.HistorialEstados);
    }
}