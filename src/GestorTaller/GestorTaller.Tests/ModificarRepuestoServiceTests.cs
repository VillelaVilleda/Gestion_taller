using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "editar repuesto" (issue #125).
public class ModificarRepuestoServiceTests
{
    private static readonly PermisosUsuario Administrador = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Admin", EsAdministrador = true });
    private static readonly PermisosUsuario UsuarioComun = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Luis", EsAdministrador = false });

    private static Repuesto CrearRepuesto(string nombre, int existencia = 10, decimal precio = 25m) =>
        new() { Id = Guid.NewGuid(), Nombre = nombre, Existencia = existencia, Precio = precio };

    private static (ModificarRepuestoService Servicio, RepuestoRepositoryFalso Repositorio) Crear(params Repuesto[] repuestos)
    {
        var repositorio = new RepuestoRepositoryFalso();
        repositorio.Guardados.AddRange(repuestos);
        return (new ModificarRepuestoService(repositorio), repositorio);
    }

    [Fact]
    public void Ejecutar_ComoAdministrador_ActualizaLosDatos()
    {
        var bujia = CrearRepuesto("Bujia");
        var (servicio, repositorio) = Crear(bujia);

        servicio.Ejecutar(Administrador, bujia, "  Bujia NGK ", 4, 30.50m);

        Assert.Equal("Bujia NGK", bujia.Nombre);
        Assert.Equal(4, bujia.Existencia);
        Assert.Equal(30.50m, bujia.Precio);
        Assert.Equal(1, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_ConExistenciaYPrecioEnCero_EsValido()
    {
        var bujia = CrearRepuesto("Bujia");
        var (servicio, _) = Crear(bujia);

        servicio.Ejecutar(Administrador, bujia, "Bujia", 0, 0m);

        Assert.Equal(0, bujia.Existencia);
        Assert.Equal(0m, bujia.Precio);
    }

    [Fact]
    public void Ejecutar_ConservandoSuPropioNombre_NoEsDuplicado()
    {
        var bujia = CrearRepuesto("Bujía");
        var (servicio, _) = Crear(bujia);

        servicio.Ejecutar(Administrador, bujia, "BUJIA", 10, 40m);

        Assert.Equal("BUJIA", bujia.Nombre);
    }

    [Fact]
    public void Ejecutar_CambiarElPrecio_NoAlteraElPrecioYaGuardadoEnLaOrden()
    {
        var bujia = CrearRepuesto("Bujia", precio: 25m);
        var asignado = new OrdenRepuesto { RepuestoId = bujia.Id, Repuesto = bujia, Cantidad = 2, PrecioUnitario = 25m };
        var (servicio, _) = Crear(bujia);

        servicio.Ejecutar(Administrador, bujia, "Bujia", 10, 99m);

        Assert.Equal(25m, asignado.PrecioUnitario);
    }

    [Fact]
    public void Ejecutar_ComoUsuarioComun_LanzaExcepcionYNoCambiaNada()
    {
        var bujia = CrearRepuesto("Bujia");
        var (servicio, repositorio) = Crear(bujia);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(UsuarioComun, bujia, "Otra", 1, 1m));

        Assert.Equal("Bujia", bujia.Nombre);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinNombre_LanzaExcepcion(string? nombre)
    {
        var bujia = CrearRepuesto("Bujia");
        var (servicio, _) = Crear(bujia);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, bujia, nombre!, 10, 25m));
        Assert.Equal("Bujia", bujia.Nombre);
    }

    [Fact]
    public void Ejecutar_ConNombreDeMasDe150Caracteres_LanzaExcepcion()
    {
        var bujia = CrearRepuesto("Bujia");
        var (servicio, _) = Crear(bujia);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, bujia, new string('a', 151), 10, 25m));
    }

    [Theory]
    [InlineData("filtro")]
    [InlineData("  FILTRO ")]
    public void Ejecutar_ConNombreDeOtroRepuesto_LanzaExcepcion(string nombre)
    {
        var bujia = CrearRepuesto("Bujia");
        var filtro = CrearRepuesto("Filtro");
        var (servicio, repositorio) = Crear(bujia, filtro);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(Administrador, bujia, nombre, 10, 25m));

        Assert.Equal("Bujia", bujia.Nombre);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_ConExistenciaNegativa_LanzaExcepcion()
    {
        var bujia = CrearRepuesto("Bujia");
        var (servicio, _) = Crear(bujia);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, bujia, "Bujia", -1, 25m));
    }

    [Fact]
    public void Ejecutar_ConPrecioNegativo_LanzaExcepcion()
    {
        var bujia = CrearRepuesto("Bujia");
        var (servicio, _) = Crear(bujia);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, bujia, "Bujia", 10, -0.01m));
    }

    [Fact]
    public void Ejecutar_SinPermisosOSinRepuesto_LanzaExcepcion()
    {
        var bujia = CrearRepuesto("Bujia");
        var (servicio, _) = Crear(bujia);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, bujia, "Bujia", 10, 25m));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, null!, "Bujia", 10, 25m));
    }
}
