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
    public void Ejecutar_AceptadaDesdeCotizacion_AvanzaAEnReparacion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, "Maria Diaz", 350m, aceptada: true);

        Assert.Equal(EstadoOrden.EnReparacion, orden.Estado);
        Assert.Contains(EstadoOrden.EnReparacion, orden.HistorialEstados);
    }

    [Fact]
    public void Ejecutar_RechazadaDesdeCotizacion_AvanzaATerminadoYSaltaReparacion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, "Maria Diaz", 350m, aceptada: false);

        Assert.Equal(EstadoOrden.Terminado, orden.Estado);
        Assert.DoesNotContain(EstadoOrden.EnReparacion, orden.HistorialEstados);
    }

    [Fact]
    public void Ejecutar_ConDatosValidos_GuardaEmpleadoMontoYAceptacion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var servicio = CrearServicio();

        servicio.Ejecutar(orden, "Maria Diaz", 350m, aceptada: true);

        Assert.Equal("Maria Diaz", orden.EmpleadoCotizacion);
        Assert.Equal(350m, orden.MontoCotizado);
        Assert.True(orden.CotizacionAceptada);
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
        var servicio = CrearServicio();

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(orden, "Maria Diaz", 350m, aceptada: true));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinEmpleado_LanzaExcepcion(string? empleado)
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(orden, empleado!, 350m, aceptada: true));
    }

    [Fact]
    public void Ejecutar_ConMontoCero_LanzaExcepcion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(orden, "Maria Diaz", 0m, aceptada: true));
    }

    [Fact]
    public void Ejecutar_ConMontoNegativo_LanzaExcepcion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(orden, "Maria Diaz", -5m, aceptada: true));
    }

    [Fact]
    public void Ejecutar_SinOrden_LanzaExcepcion()
    {
        var servicio = CrearServicio();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, "Maria Diaz", 350m, aceptada: true));
    }

    // ---- Repuestos de la cotizacion (issue #103) ----

    private static Repuesto AgregarRepuesto(Orden orden, string nombre, int existencia, int cantidad)
    {
        var repuesto = new Repuesto { Id = Guid.NewGuid(), Nombre = nombre, Existencia = existencia };
        orden.Repuestos.Add(new OrdenRepuesto
        {
            OrdenId = orden.Id,
            RepuestoId = repuesto.Id,
            Repuesto = repuesto,
            Cantidad = cantidad
        });
        return repuesto;
    }

    [Fact]
    public void Ejecutar_Aceptada_DescuentaElInventarioDeCadaRepuesto()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var filtro = AgregarRepuesto(orden, "Filtro de aceite", 10, 3);
        var bujia = AgregarRepuesto(orden, "Bujia", 8, 8);

        CrearServicio().Ejecutar(orden, "Maria Diaz", 350m, aceptada: true);

        Assert.Equal(7, filtro.Existencia);
        Assert.Equal(0, bujia.Existencia);
        Assert.Equal(2, orden.Repuestos.Count);
    }

    [Fact]
    public void Ejecutar_AceptadaConExistenciaInsuficiente_LanzaExcepcionConElNombreDelRepuesto()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        AgregarRepuesto(orden, "Filtro de aceite", 2, 3);

        var ex = Assert.Throws<InvalidOperationException>(
            () => CrearServicio().Ejecutar(orden, "Maria Diaz", 350m, aceptada: true));

        Assert.Contains("Filtro de aceite", ex.Message);
    }

    [Fact]
    public void Ejecutar_AceptadaConExistenciaInsuficiente_NoCambiaNada()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var suficiente = AgregarRepuesto(orden, "Bujia", 10, 2);
        var insuficiente = AgregarRepuesto(orden, "Filtro de aceite", 1, 3);

        Assert.Throws<InvalidOperationException>(
            () => CrearServicio().Ejecutar(orden, "Maria Diaz", 350m, aceptada: true));

        Assert.Equal(EstadoOrden.Cotizacion, orden.Estado);
        Assert.Equal(10, suficiente.Existencia);
        Assert.Equal(1, insuficiente.Existencia);
        Assert.Equal(string.Empty, orden.EmpleadoCotizacion);
        Assert.Equal(2, orden.Repuestos.Count);
    }

    [Fact]
    public void Ejecutar_Rechazada_QuitaLosRepuestosSinTocarElInventario()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        var filtro = AgregarRepuesto(orden, "Filtro de aceite", 10, 3);

        CrearServicio().Ejecutar(orden, "Maria Diaz", 350m, aceptada: false);

        Assert.Empty(orden.Repuestos);
        Assert.Equal(10, filtro.Existencia);
    }

    [Fact]
    public void Ejecutar_RechazadaConExistenciaInsuficiente_NoLanzaExcepcion()
    {
        var orden = CrearOrdenEnEstado(EstadoOrden.Cotizacion);
        AgregarRepuesto(orden, "Filtro de aceite", 0, 3);

        CrearServicio().Ejecutar(orden, "Maria Diaz", 350m, aceptada: false);

        Assert.Equal(EstadoOrden.Terminado, orden.Estado);
    }
}
