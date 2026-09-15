using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "registrar diagnostico de una orden" (issue #25).
// Estan en rojo a proposito: RegistrarDiagnosticoOrdenService todavia no existe.
public class RegistrarDiagnosticoOrdenServiceTests
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

    private static RegistrarDiagnosticoOrdenService CrearServicio()
    {
        return new RegistrarDiagnosticoOrdenService(new AvanzarEstadoOrdenService());
    }

    [Fact]
    public void Ejecutar_DesdeRecepcionConDatosValidos_AvanzaADiagnostico()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Recepcion);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, "Carlos Lopez", "Falla en el alternador");

        Assert.Equal(EstadoOrden.Diagnostico, orden.Estado);
    }

    [Fact]
    public void Ejecutar_DesdeRecepcionConDatosValidos_GuardaEmpleadoYDetalle()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Recepcion);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, "Carlos Lopez", "Falla en el alternador");

        Assert.Equal("Carlos Lopez", orden.EmpleadoDiagnostico);
        Assert.Equal("Falla en el alternador", orden.DetalleDiagnostico);
    }

    [Fact]
    public void Ejecutar_DesdeRecepcionConDatosValidos_AgregaDiagnosticoAlHistorial()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Recepcion);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, "Carlos Lopez", "Falla en el alternador");

        Assert.Contains(EstadoOrden.Diagnostico, orden.HistorialEstados);
    }

    [Theory]
    [InlineData(EstadoOrden.Diagnostico)]
    [InlineData(EstadoOrden.Cotizacion)]
    [InlineData(EstadoOrden.EnReparacion)]
    [InlineData(EstadoOrden.Terminado)]
    [InlineData(EstadoOrden.Entregado)]
    public void Ejecutar_DesdeEstadoDistintoARecepcion_LanzaExcepcion(EstadoOrden estado)
    {
        var orden = CrearOrdenEnEstado(estado);
        var servicio = CrearServicio();

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(orden, "Carlos Lopez", "Falla en el alternador"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinEmpleado_LanzaExcepcion(string? empleado)
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Recepcion);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(orden, empleado!, "Falla en el alternador"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinDetalle_LanzaExcepcion(string? detalle)
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Recepcion);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(orden, "Carlos Lopez", detalle!));
    }

    [Fact]
    public void Ejecutar_SinOrden_LanzaExcepcion()
    {
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, "Carlos Lopez", "Falla en el alternador"));
    }
}