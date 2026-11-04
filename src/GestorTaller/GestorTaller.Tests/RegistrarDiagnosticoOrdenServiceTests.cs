using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "registrar diagnostico de una orden" (issue #25).
public class RegistrarDiagnosticoOrdenServiceTests
{
    private static readonly Empleado empleado = new() { Id = Guid.NewGuid(), Nombre = "Carlos Lopez" };

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

    private static RegistrarDiagnosticoOrdenService CrearServicio()
    {
        return new RegistrarDiagnosticoOrdenService(new AvanzarEstadoOrdenService());
    }

    [Fact]
    public void Ejecutar_DesdeDiagnosticoConDatosValidos_AvanzaACotizacion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, empleado, "Falla en el alternador");

        Assert.Equal(EstadoOrden.Cotizacion, orden.Estado);
    }

    [Fact]
    public void Ejecutar_DesdeDiagnosticoConDatosValidos_GuardaEmpleadoYDetalle()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, empleado, "Falla en el alternador");

        Assert.Same(empleado, orden.EmpleadoDiagnostico);
        Assert.Equal("Falla en el alternador", orden.DetalleDiagnostico);
    }

    [Fact]
    public void Ejecutar_DesdeDiagnosticoConDatosValidos_AgregaCotizacionAlHistorial()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, empleado, "Falla en el alternador");

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

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(orden, empleado, "Falla en el alternador"));
    }

    [Fact]
    public void Ejecutar_SinEmpleado_LanzaExcepcion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(orden, null!, "Falla en el alternador"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinDetalle_LanzaExcepcion(string? detalle)
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(orden, empleado, detalle!));
    }

    [Fact]
    public void Ejecutar_SinOrden_LanzaExcepcion()
    {
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, empleado, "Falla en el alternador"));
    }

    // Issue #175: la base de datos solo acepta 500 caracteres en el detalle.
    [Fact]
    public void Ejecutar_ConDetalleDeMasDe500Caracteres_LanzaExcepcionYNoAvanza()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() =>
            servicio.Ejecutar(orden, empleado, new string('a', RegistrarDiagnosticoOrdenService.LongitudMaximaDetalle + 1)));

        Assert.Equal(EstadoOrden.Diagnostico, orden.Estado);
        Assert.Null(orden.EmpleadoDiagnostico);
        Assert.Equal(string.Empty, orden.DetalleDiagnostico);
    }

    [Fact]
    public void Ejecutar_ConDetalleDe500CaracteresYEspacios_GuardaElTextoRecortado()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Diagnostico);
        var servicio = CrearServicio();
        var detalle = new string('a', RegistrarDiagnosticoOrdenService.LongitudMaximaDetalle);

        servicio.Ejecutar(orden, empleado, "  " + detalle + "  ");

        Assert.Equal(detalle, orden.DetalleDiagnostico);
        Assert.Equal(EstadoOrden.Cotizacion, orden.Estado);
    }
}
