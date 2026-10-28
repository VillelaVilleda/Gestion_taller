using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "incrementar / decrementar existencia" (issue #145).
public class AjustarExistenciaServiceTests
{
    private static readonly PermisosUsuario Administrador = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Admin", EsAdministrador = true });
    private static readonly PermisosUsuario UsuarioComun = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Luis", EsAdministrador = false });

    private static (AjustarExistenciaService Servicio, Repuesto Repuesto, RepuestoRepositoryFalso Repositorio) Crear(int existencia = 10)
    {
        var repuesto = new Repuesto { Id = Guid.NewGuid(), Nombre = "Bujía", Existencia = existencia, Precio = 25m };
        var repositorio = new RepuestoRepositoryFalso();
        repositorio.Guardados.Add(repuesto);
        return (new AjustarExistenciaService(repositorio), repuesto, repositorio);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Incrementar_CualquierUsuario_SumaYGuarda(bool esAdministrador)
    {
        var (servicio, repuesto, repositorio) = Crear(10);

        servicio.Incrementar(esAdministrador ? Administrador : UsuarioComun, repuesto, 5);

        Assert.Equal(15, repuesto.Existencia);
        Assert.Equal(1, repositorio.Actualizaciones);
    }

    [Fact]
    public void Decrementar_ComoAdministrador_RestaYGuarda()
    {
        var (servicio, repuesto, repositorio) = Crear(10);

        servicio.Decrementar(Administrador, repuesto, 4);

        Assert.Equal(6, repuesto.Existencia);
        Assert.Equal(1, repositorio.Actualizaciones);
    }

    [Fact]
    public void Decrementar_HastaCero_EsValido()
    {
        var (servicio, repuesto, _) = Crear(10);

        servicio.Decrementar(Administrador, repuesto, 10);

        Assert.Equal(0, repuesto.Existencia);
    }

    [Fact]
    public void Decrementar_ComoUsuarioComun_LanzaYNoCambiaNada()
    {
        var (servicio, repuesto, repositorio) = Crear(10);

        var ex = Assert.Throws<InvalidOperationException>(() => servicio.Decrementar(UsuarioComun, repuesto, 1));

        Assert.Equal("Solo un administrador puede decrementar la existencia.", ex.Message);
        Assert.Equal(10, repuesto.Existencia);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Decrementar_MasDeLoQueHay_LanzaYNoCambiaNada()
    {
        var (servicio, repuesto, repositorio) = Crear(3);

        var ex = Assert.Throws<InvalidOperationException>(() => servicio.Decrementar(Administrador, repuesto, 4));

        Assert.Equal("No puedes quitar 4: solo hay 3 en existencia.", ex.Message);
        Assert.Equal(3, repuesto.Existencia);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Incrementar_PasandoDelLimite_LanzaYNoCambiaNada()
    {
        var (servicio, repuesto, repositorio) = Crear(AjustarExistenciaService.LimiteExistencia - 1);

        Assert.Throws<InvalidOperationException>(() => servicio.Incrementar(UsuarioComun, repuesto, 2));

        Assert.Equal(AjustarExistenciaService.LimiteExistencia - 1, repuesto.Existencia);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public void Ajustar_ConCantidadNoPositiva_Lanza(int cantidad)
    {
        var (servicio, repuesto, repositorio) = Crear(10);

        Assert.Throws<ArgumentException>(() => servicio.Incrementar(Administrador, repuesto, cantidad));
        Assert.Throws<ArgumentException>(() => servicio.Decrementar(Administrador, repuesto, cantidad));
        Assert.Equal(10, repuesto.Existencia);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ajustar_SinPermisosORepuesto_Lanza()
    {
        var (servicio, repuesto, _) = Crear();

        Assert.Throws<ArgumentException>(() => servicio.Incrementar(null!, repuesto, 1));
        Assert.Throws<ArgumentException>(() => servicio.Incrementar(Administrador, null!, 1));
        Assert.Throws<ArgumentException>(() => servicio.Decrementar(null!, repuesto, 1));
        Assert.Throws<ArgumentException>(() => servicio.Decrementar(Administrador, null!, 1));
    }
}
