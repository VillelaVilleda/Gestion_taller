using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "registrar reparacion de una orden" (issue #27).
public class RegistrarReparacionOrdenServiceTests
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

    private static RegistrarReparacionOrdenService CrearServicio()
    {
        return new RegistrarReparacionOrdenService(new AvanzarEstadoOrdenService());
    }

    [Fact]
    public void Ejecutar_DesdeEnReparacionConDatosValidos_AvanzaATerminado()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, "Pedro Gomez", "Se cambio el alternador");

        Assert.Equal(EstadoOrden.Terminado, orden.Estado);
    }

    [Fact]
    public void Ejecutar_DesdeEnReparacionConDatosValidos_GuardaEmpleadoYDetalle()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, "Pedro Gomez", "Se cambio el alternador");

        Assert.Equal("Pedro Gomez", orden.EmpleadoReparacion);
        Assert.Equal("Se cambio el alternador", orden.DetalleReparacion);
    }

    [Fact]
    public void Ejecutar_DesdeEnReparacionConDatosValidos_AgregaTerminadoAlHistorial()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, "Pedro Gomez", "Se cambio el alternador");

        Assert.Contains(EstadoOrden.Terminado, orden.HistorialEstados);
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
        var servicio = CrearServicio();

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(orden, "Pedro Gomez", "Se cambio el alternador"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinEmpleado_LanzaExcepcion(string? empleado)
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(orden, empleado!, "Se cambio el alternador"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinDetalle_LanzaExcepcion(string? detalle)
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.EnReparacion);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(orden, "Pedro Gomez", detalle!));
    }

    [Fact]
    public void Ejecutar_SinOrden_LanzaExcepcion()
    {
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, "Pedro Gomez", "Se cambio el alternador"));
    }
}