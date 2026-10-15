using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del calculo del total a cobrar de una orden (issue #104).
public class CalcularTotalOrdenServiceTests
{
    private static Orden CrearOrden(bool aceptada, decimal manoDeObra = 350m)
    {
        var orden = new Orden
        {
            Id = Guid.NewGuid(),
            Cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" },
            DescripcionObjeto = "Vehiculo sedan color rojo",
            DescripcionProblema = "El vehiculo no enciende",
            CostoDiagnostico = 15m,
            MontoCotizado = manoDeObra,
            CotizacionAceptada = aceptada,
            Estado = EstadoOrden.Terminado,
            FechaRecepcion = DateTime.Now,
            HistorialEstados = new List<EstadoOrden> { EstadoOrden.Terminado }
        };

        orden.Repuestos.Add(CrearAsignado(orden, 2, 45m));
        orden.Repuestos.Add(CrearAsignado(orden, 1, 180m));
        return orden;
    }

    private static OrdenRepuesto CrearAsignado(Orden orden, int cantidad, decimal precioUnitario)
    {
        var repuesto = new Repuesto { Id = Guid.NewGuid(), Nombre = "Repuesto", Existencia = 10, Precio = precioUnitario };
        return new OrdenRepuesto
        {
            OrdenId = orden.Id,
            RepuestoId = repuesto.Id,
            Repuesto = repuesto,
            Cantidad = cantidad,
            PrecioUnitario = precioUnitario
        };
    }

    [Fact]
    public void Calcular_ConCotizacionAceptada_SumaDiagnosticoManoDeObraYRepuestos()
    {
        var desglose = new CalcularTotalOrdenService().Calcular(CrearOrden(aceptada: true));

        Assert.Equal(15m, desglose.CostoDiagnostico);
        Assert.Equal(350m, desglose.ManoDeObra);
        Assert.Equal(270m, desglose.Repuestos); // 2 x 45 + 1 x 180
        Assert.Equal(635m, desglose.Total);
    }

    [Fact]
    public void Calcular_ConCotizacionRechazada_SoloCobraElDiagnostico()
    {
        var desglose = new CalcularTotalOrdenService().Calcular(CrearOrden(aceptada: false));

        Assert.Equal(15m, desglose.CostoDiagnostico);
        Assert.Equal(0m, desglose.ManoDeObra);
        Assert.Equal(0m, desglose.Repuestos);
        Assert.Equal(15m, desglose.Total);
    }

    [Fact]
    public void Calcular_AceptadaSinRepuestos_SumaDiagnosticoYManoDeObra()
    {
        var orden = CrearOrden(aceptada: true);
        orden.Repuestos.Clear();

        var desglose = new CalcularTotalOrdenService().Calcular(orden);

        Assert.Equal(365m, desglose.Total);
    }

    [Fact]
    public void Calcular_UsaElPrecioGuardadoEnLaAsignacion_NoElPrecioActualDelRepuesto()
    {
        var orden = CrearOrden(aceptada: true);
        orden.Repuestos.ForEach(a => a.Repuesto.Precio = 9999m);

        var desglose = new CalcularTotalOrdenService().Calcular(orden);

        Assert.Equal(270m, desglose.Repuestos);
    }

    [Fact]
    public void Calcular_ConOrdenNula_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new CalcularTotalOrdenService().Calcular(null!));
    }

    [Fact]
    public void Calcular_ConValoresSueltos_SumaLosTresConceptos()
    {
        var orden = CrearOrden(aceptada: true);

        var desglose = new CalcularTotalOrdenService().Calcular(10m, 100m, orden.Repuestos);

        Assert.Equal(10m, desglose.CostoDiagnostico);
        Assert.Equal(100m, desglose.ManoDeObra);
        Assert.Equal(270m, desglose.Repuestos);
        Assert.Equal(380m, desglose.Total);
    }

    [Fact]
    public void Calcular_ConValoresSueltosYRepuestosNulos_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new CalcularTotalOrdenService().Calcular(10m, 100m, null!));
    }
}
