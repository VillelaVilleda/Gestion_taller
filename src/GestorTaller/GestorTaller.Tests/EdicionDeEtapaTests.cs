using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas de qué etapas de una orden se pueden editar (issue #152).
public class EdicionDeEtapaTests
{
    // La orden pasó por todas las etapas hasta estadoActual, en orden.
    private static Orden CrearOrden(EstadoOrden estadoActual) => new()
    {
        Id = Guid.NewGuid(),
        Cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" },
        DescripcionObjeto = "Vehiculo sedan color rojo",
        DescripcionProblema = "El vehiculo no enciende",
        Estado = estadoActual,
        FechaRecepcion = DateTime.Now,
        HistorialEstados = Enum.GetValues<EstadoOrden>().Where(e => e <= estadoActual).ToList()
    };

    [Theory]
    [InlineData(EstadoOrden.Diagnostico, EstadoOrden.Diagnostico, false)]  // etapa en curso
    [InlineData(EstadoOrden.Cotizacion, EstadoOrden.Diagnostico, true)]    // etapa ya completada
    [InlineData(EstadoOrden.Terminado, EstadoOrden.Diagnostico, true)]
    [InlineData(EstadoOrden.Cotizacion, EstadoOrden.EnReparacion, false)]  // la orden aún no llegó
    [InlineData(EstadoOrden.EnReparacion, EstadoOrden.EnReparacion, false)]
    [InlineData(EstadoOrden.Terminado, EstadoOrden.EnReparacion, true)]
    [InlineData(EstadoOrden.Cotizacion, EstadoOrden.Recepcion, true)]
    [InlineData(EstadoOrden.Terminado, EstadoOrden.Cotizacion, false)]     // otras etapas
    [InlineData(EstadoOrden.Terminado, EstadoOrden.Terminado, false)]
    [InlineData(EstadoOrden.Entregado, EstadoOrden.Recepcion, false)]      // orden entregada
    [InlineData(EstadoOrden.Entregado, EstadoOrden.Diagnostico, false)]
    [InlineData(EstadoOrden.Entregado, EstadoOrden.EnReparacion, false)]
    public void PuedeEditar_SegunLaEtapaYElEstadoDeLaOrden(EstadoOrden estadoActual, EstadoOrden etapa, bool esperado)
    {
        Assert.Equal(esperado, EdicionDeEtapa.PuedeEditar(CrearOrden(estadoActual), etapa));
    }

    [Fact]
    public void Reparacion_SiLaCotizacionSeRechazo_NoSePuedeEditar()
    {
        var orden = CrearOrden(EstadoOrden.Terminado);
        orden.HistorialEstados.Remove(EstadoOrden.EnReparacion); // rechazada: pasó de Cotización a Terminado

        Assert.False(EdicionDeEtapa.PuedeEditar(orden, EstadoOrden.EnReparacion));
    }

    [Fact]
    public void SinOrden_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => EdicionDeEtapa.PuedeEditar(null!, EstadoOrden.Diagnostico));
    }
}
