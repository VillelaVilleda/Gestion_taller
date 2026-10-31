using GestorTaller.Datos;
using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "editar orden" (issue #133).
public class ModificarOrdenServiceTests
{
    private static readonly PermisosUsuario Administrador = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Admin", EsAdministrador = true });
    private static readonly PermisosUsuario UsuarioComun = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Luis", EsAdministrador = false });

    private static Orden CrearOrden(EstadoOrden estado = EstadoOrden.Diagnostico) => new()
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

    private static ModificarOrdenService CrearServicio(out OrdenRepositoryEnMemoria repositorio)
    {
        repositorio = new OrdenRepositoryEnMemoria();
        return new ModificarOrdenService(repositorio);
    }

    [Fact]
    public void Ejecutar_ComoAdministrador_ActualizaLosDatosRecortados()
    {
        var servicio = CrearServicio(out _);
        var orden = CrearOrden();

        servicio.Ejecutar(Administrador, orden, "  Moto azul  ", " No frena ", 30m);

        Assert.Equal("Moto azul", orden.DescripcionObjeto);
        Assert.Equal("No frena", orden.DescripcionProblema);
        Assert.Equal(30m, orden.CostoDiagnostico);
    }

    [Fact]
    public void Ejecutar_NoCambiaElClienteNiElEstado()
    {
        var servicio = CrearServicio(out _);
        var orden = CrearOrden(EstadoOrden.Cotizacion);
        var cliente = orden.Cliente;

        servicio.Ejecutar(Administrador, orden, "Moto azul", "No frena", 30m);

        Assert.Same(cliente, orden.Cliente);
        Assert.Equal(EstadoOrden.Cotizacion, orden.Estado);
    }

    [Fact]
    public void Ejecutar_ConCostoDeDiagnosticoEnCero_EsValido()
    {
        var servicio = CrearServicio(out _);
        var orden = CrearOrden();

        servicio.Ejecutar(Administrador, orden, "Moto azul", "No frena", 0m);

        Assert.Equal(0m, orden.CostoDiagnostico);
    }

    [Fact]
    public void Ejecutar_EnOrdenCotizadaYAceptada_RecalculaElTotalConElNuevoDiagnostico()
    {
        var servicio = CrearServicio(out _);
        var orden = CrearOrden(EstadoOrden.EnReparacion);
        orden.CotizacionAceptada = true;
        orden.MontoCotizado = 100m;
        orden.Repuestos.Add(new OrdenRepuesto { Cantidad = 2, PrecioUnitario = 25m });
        var calculador = new CalcularTotalOrdenService();
        Assert.Equal(165m, calculador.Calcular(orden).Total);

        servicio.Ejecutar(Administrador, orden, "Vehiculo sedan color rojo", "El vehiculo no enciende", 40m);

        Assert.Equal(190m, calculador.Calcular(orden).Total);
    }

    [Theory]
    [InlineData(EstadoOrden.Recepcion)]
    [InlineData(EstadoOrden.Diagnostico)]
    [InlineData(EstadoOrden.Cotizacion)]
    [InlineData(EstadoOrden.EnReparacion)]
    [InlineData(EstadoOrden.Terminado)]
    public void Ejecutar_EnCualquierEstadoNoEntregado_PermiteEditar(EstadoOrden estado)
    {
        var servicio = CrearServicio(out _);
        var orden = CrearOrden(estado);

        servicio.Ejecutar(Administrador, orden, "Moto azul", "No frena", 30m);

        Assert.Equal("Moto azul", orden.DescripcionObjeto);
    }

    [Fact]
    public void Ejecutar_EnOrdenEntregada_LanzaExcepcionYNoCambiaNada()
    {
        var servicio = CrearServicio(out _);
        var orden = CrearOrden(EstadoOrden.Entregado);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(Administrador, orden, "Moto azul", "No frena", 30m));

        Assert.Equal("Vehiculo sedan color rojo", orden.DescripcionObjeto);
        Assert.Equal(15m, orden.CostoDiagnostico);
    }

    [Fact]
    public void Ejecutar_ComoUsuarioComun_LanzaExcepcionYNoCambiaNada()
    {
        var servicio = CrearServicio(out _);
        var orden = CrearOrden();

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(UsuarioComun, orden, "Moto azul", "No frena", 30m));

        Assert.Equal("Vehiculo sedan color rojo", orden.DescripcionObjeto);
    }

    [Theory]
    [InlineData("", "No frena")]
    [InlineData("   ", "No frena")]
    [InlineData(null, "No frena")]
    [InlineData("Moto azul", "")]
    [InlineData("Moto azul", "   ")]
    [InlineData("Moto azul", null)]
    public void Ejecutar_SinDescripcionDelObjetoODelProblema_LanzaExcepcion(string? objeto, string? problema)
    {
        var servicio = CrearServicio(out _);
        var orden = CrearOrden();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, orden, objeto!, problema!, 30m));

        Assert.Equal("Vehiculo sedan color rojo", orden.DescripcionObjeto);
        Assert.Equal("El vehiculo no enciende", orden.DescripcionProblema);
    }

    [Fact]
    public void Ejecutar_ConCostoDeDiagnosticoNegativo_LanzaExcepcion()
    {
        var servicio = CrearServicio(out _);
        var orden = CrearOrden();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, orden, "Moto azul", "No frena", -0.01m));

        Assert.Equal(15m, orden.CostoDiagnostico);
    }

    [Fact]
    public void Ejecutar_SinPermisosOSinOrden_LanzaExcepcion()
    {
        var servicio = CrearServicio(out _);
        var orden = CrearOrden();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, orden, "Moto azul", "No frena", 30m));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, null!, "Moto azul", "No frena", 30m));
    }

    [Fact]
    public void Ejecutar_ConCambios_RegistraLaFechaDeUltimaEdicionYGuarda()
    {
        var repositorio = new OrdenRepositoryFalso();
        var servicio = new ModificarOrdenService(repositorio);
        var orden = CrearOrden();

        var cambio = servicio.Ejecutar(Administrador, orden, "Moto azul", "No frena", 30m);

        Assert.True(cambio);
        Assert.NotNull(orden.FechaUltimaEdicion);
        Assert.True((DateTime.Now - orden.FechaUltimaEdicion!.Value).TotalSeconds < 5);
        Assert.Equal(1, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_SinCambios_NoGuardaNiTocaLaFecha()
    {
        var repositorio = new OrdenRepositoryFalso();
        var servicio = new ModificarOrdenService(repositorio);
        var orden = CrearOrden();

        var cambio = servicio.Ejecutar(Administrador, orden, " Vehiculo sedan color rojo ", "El vehiculo no enciende", 15m);

        Assert.False(cambio);
        Assert.Null(orden.FechaUltimaEdicion);
        Assert.Equal(0, repositorio.Actualizaciones);
    }
}
