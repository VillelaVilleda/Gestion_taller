using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "registrar cotizacion de una orden" (issue #26).
public class RegistrarCotizacionOrdenServiceTests
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

    private static RegistrarCotizacionOrdenService CrearServicio()
    {
        return new RegistrarCotizacionOrdenService(new AvanzarEstadoOrdenService());
    }

    [Fact]
    public void Ejecutar_AceptadaDesdeDiagnostico_AvanzaAEnReparacion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, "Maria Diaz", 350m, aceptada: true);

        Assert.Equal(EstadoOrden.EnReparacion, orden.Estado);
    }

    [Fact]
    public void Ejecutar_RechazadaDesdeDiagnostico_AvanzaATerminadoYSaltaReparacion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, "Maria Diaz", 350m, aceptada: false);

        Assert.Equal(EstadoOrden.Terminado, orden.Estado);
        Assert.DoesNotContain(EstadoOrden.EnReparacion, orden.HistorialEstados);
    }

    [Fact]
    public void Ejecutar_ConDatosValidos_GuardaEmpleadoMontoYAceptacion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, "Maria Diaz", 350m, aceptada: true);

        Assert.Equal("Maria Diaz", orden.EmpleadoCotizacion);
        Assert.Equal(350m, orden.MontoCotizado);
        Assert.True(orden.CotizacionAceptada);
    }

    [Fact]
    public void Ejecutar_ConDatosValidos_CotizacionQuedaEnElHistorial()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, "Maria Diaz", 350m, aceptada: false);

        Assert.Contains(EstadoOrden.Cotizacion, orden.HistorialEstados);
    }

    [Theory]
    [InlineData(EstadoOrden.Recepcion)]
    [InlineData(EstadoOrden.Cotizacion)]
    [InlineData(EstadoOrden.EnReparacion)]
    [InlineData(EstadoOrden.Terminado)]
    [InlineData(EstadoOrden.Entregado)]
    public void Ejecutar_DesdeEstadoDistintoADiagnostico_LanzaExcepcion(EstadoOrden estado)
    {
        var orden = CrearOrdenEnEstado(estado);
        var servicio = CrearServicio();

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(orden, "Maria Diaz", 350m, aceptada: true));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinEmpleado_LanzaExcepcion(string? empleado)
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(orden, empleado!, 350m, aceptada: true));
    }

    [Fact]
    public void Ejecutar_ConMontoCero_LanzaExcepcion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(orden, "Maria Diaz", 0m, aceptada: true));
    }

    [Fact]
    public void Ejecutar_ConMontoNegativo_LanzaExcepcion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(orden, "Maria Diaz", -5m, aceptada: true));
    }

    [Fact]
    public void Ejecutar_SinOrden_LanzaExcepcion()
    {
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, "Maria Diaz", 350m, aceptada: true));
    }
}