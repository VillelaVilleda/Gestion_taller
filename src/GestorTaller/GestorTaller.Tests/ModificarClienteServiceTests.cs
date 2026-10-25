using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "editar cliente" (issue #126). Las reglas de los datos se prueban en ValidadorDatosClienteTests.
public class ModificarClienteServiceTests
{
    private static readonly PermisosUsuario Administrador = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Admin", EsAdministrador = true });
    private static readonly PermisosUsuario UsuarioComun = new(new Empleado { Id = Guid.NewGuid(), Nombre = "Luis", EsAdministrador = false });

    private static Cliente CrearCliente(string nombre) =>
        new() { Id = Guid.NewGuid(), Nombre = nombre, Telefono = "5555-1234" };

    private static (ModificarClienteService Servicio, ClienteRepositoryFalso Repositorio) Crear(params Cliente[] clientes)
    {
        var repositorio = new ClienteRepositoryFalso();
        repositorio.Guardados.AddRange(clientes);
        return (new ModificarClienteService(repositorio), repositorio);
    }

    [Fact]
    public void Ejecutar_ComoAdministrador_ActualizaNombreYContactoRecortados()
    {
        var juan = CrearCliente("Juan Perez");
        var (servicio, repositorio) = Crear(juan);

        servicio.Ejecutar(Administrador, juan, "  Juan Perez Lopez ", " 5555-9999 ", " juan@correo.com ", "  Zona 1 ");

        Assert.Equal("Juan Perez Lopez", juan.Nombre);
        Assert.Equal("5555-9999", juan.Telefono);
        Assert.Equal("juan@correo.com", juan.Correo);
        Assert.Equal("Zona 1", juan.Direccion);
        Assert.Equal(1, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_ConCamposOpcionalesVacios_LosDejaEnNull()
    {
        var juan = CrearCliente("Juan Perez");
        juan.Correo = "juan@correo.com";
        juan.Direccion = "Zona 1";
        var (servicio, _) = Crear(juan);

        servicio.Ejecutar(Administrador, juan, "Juan Perez", "5555-1234", "  ", "");

        Assert.Null(juan.Correo);
        Assert.Null(juan.Direccion);
    }

    [Fact]
    public void Ejecutar_ConservandoSuPropioNombre_NoEsDuplicado()
    {
        var juan = CrearCliente("Juan Pérez");
        var (servicio, _) = Crear(juan);

        servicio.Ejecutar(Administrador, juan, "JUAN PEREZ", "5555-1234", null, null);

        Assert.Equal("JUAN PEREZ", juan.Nombre);
    }

    [Fact]
    public void Ejecutar_ConNombreDeOtroCliente_LanzaExcepcionYNoCambiaNada()
    {
        var juan = CrearCliente("Juan Perez");
        var maria = CrearCliente("Maria Lopez");
        var (servicio, repositorio) = Crear(juan, maria);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(Administrador, juan, "  maría lopez ", "5555-1234", null, null));

        Assert.Equal("Juan Perez", juan.Nombre);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_ConDatosInvalidos_LanzaExcepcionYNoCambiaNada()
    {
        var juan = CrearCliente("Juan Perez");
        var (servicio, repositorio) = Crear(juan);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, juan, "", "5555-1234", null, null));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, juan, "Juan", null, null, null));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, juan, "Juan", "abc", null, null));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, juan, "Juan", null, "sin-arroba", null));

        Assert.Equal("Juan Perez", juan.Nombre);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_ComoUsuarioComun_LanzaExcepcionYNoCambiaNada()
    {
        var juan = CrearCliente("Juan Perez");
        var (servicio, repositorio) = Crear(juan);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(UsuarioComun, juan, "Otro Nombre", "5555-1234", null, null));

        Assert.Equal("Juan Perez", juan.Nombre);
        Assert.Equal(0, repositorio.Actualizaciones);
    }

    [Fact]
    public void Ejecutar_SinPermisosOSinCliente_LanzaExcepcion()
    {
        var juan = CrearCliente("Juan Perez");
        var (servicio, _) = Crear(juan);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, juan, "Juan", "5555-1234", null, null));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(Administrador, null!, "Juan", "5555-1234", null, null));
    }
}
