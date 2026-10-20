using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "crear empleado con credenciales generadas" (issue #114).
public class CrearEmpleadoServiceTests
{
    private class EmpleadoRepositoryFalso : IEmpleadoRepository
    {
        public List<Empleado> Guardados { get; } = new();

        public void Agregar(Empleado empleado) => Guardados.Add(empleado);

        public Empleado? ObtenerPorId(Guid id) => Guardados.FirstOrDefault(e => e.Id == id);

        public Empleado? ObtenerPorNombreUsuario(string nombreUsuario) => Guardados.FirstOrDefault(e => e.NombreUsuario == nombreUsuario);

        public IReadOnlyList<Empleado> ObtenerTodos() => Guardados;
    }

    private static (CrearEmpleadoService Servicio, EmpleadoRepositoryFalso Repositorio) Crear()
    {
        var repositorio = new EmpleadoRepositoryFalso();
        return (new CrearEmpleadoService(repositorio), repositorio);
    }

    [Fact]
    public void Ejecutar_ConNombreValido_GuardaElEmpleadoConUsuarioGenerado()
    {
        var (servicio, repositorio) = Crear();

        var creado = servicio.Ejecutar("  Carlos Méndez López ", esAdministrador: false);

        Assert.Same(creado.Empleado, Assert.Single(repositorio.Guardados));
        Assert.Equal("Carlos Méndez López", creado.Empleado.Nombre);
        Assert.Equal("carlos.lopez", creado.Empleado.NombreUsuario);
        Assert.False(creado.Empleado.EsAdministrador);
    }

    [Fact]
    public void Ejecutar_ConUnaSolaPalabra_UsaEsaPalabraComoUsuario()
    {
        var (servicio, _) = Crear();

        Assert.Equal("carlos", servicio.Ejecutar("Carlos", false).Empleado.NombreUsuario);
    }

    [Fact]
    public void Ejecutar_ComoAdministrador_GuardaElRol()
    {
        var (servicio, _) = Crear();

        Assert.True(servicio.Ejecutar("Ana Torres", esAdministrador: true).Empleado.EsAdministrador);
    }

    [Fact]
    public void Ejecutar_ConUsuarioRepetido_AgregaUnNumero()
    {
        var (servicio, _) = Crear();

        var primero = servicio.Ejecutar("Ana Torres", false);
        var segundo = servicio.Ejecutar("Ana Torres", false);
        var tercero = servicio.Ejecutar("Ana Torres", false);

        Assert.Equal("ana.torres", primero.Empleado.NombreUsuario);
        Assert.Equal("ana.torres2", segundo.Empleado.NombreUsuario);
        Assert.Equal("ana.torres3", tercero.Empleado.NombreUsuario);
    }

    [Fact]
    public void Ejecutar_GuardaLaContrasenaTemporalSoloConHash()
    {
        var (servicio, _) = Crear();

        var creado = servicio.Ejecutar("Ana Torres", false);

        Assert.Equal(GeneradorContrasena.Longitud, creado.PasswordTemporal.Length);
        Assert.NotEqual(creado.PasswordTemporal, creado.Empleado.PasswordUsuario);
        Assert.True(PasswordHasher.Verificar(creado.PasswordTemporal, creado.Empleado.PasswordUsuario));
    }

    [Fact]
    public void Ejecutar_ConDosEmpleados_GeneraContrasenasDistintas()
    {
        var (servicio, _) = Crear();

        var uno = servicio.Ejecutar("Ana Torres", false);
        var dos = servicio.Ejecutar("Luis Perez", false);

        Assert.NotEqual(uno.PasswordTemporal, dos.PasswordTemporal);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Ejecutar_SinNombre_LanzaExcepcion(string? nombre)
    {
        var (servicio, repositorio) = Crear();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(nombre!, false));
        Assert.Empty(repositorio.Guardados);
    }

    [Fact]
    public void Ejecutar_ConNombreDeMasDe150Caracteres_LanzaExcepcion()
    {
        var (servicio, _) = Crear();

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(new string('a', 151), false));
    }
}
