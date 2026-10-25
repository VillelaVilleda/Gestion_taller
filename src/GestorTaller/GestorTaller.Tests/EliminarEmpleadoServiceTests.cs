using GestorTaller.Datos;
using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del servicio "eliminar empleado" (issue #127).
public class EliminarEmpleadoServiceTests
{
    private static Empleado CrearEmpleado(string nombre, bool esAdministrador) => new()
    {
        Id = Guid.NewGuid(),
        Nombre = nombre,
        NombreUsuario = nombre.ToLowerInvariant().Replace(' ', '.'),
        PasswordUsuario = "hash",
        EsAdministrador = esAdministrador
    };

    private static Orden CrearOrden() => new()
    {
        Id = Guid.NewGuid(),
        Cliente = new Cliente { Id = Guid.NewGuid(), Nombre = "Juan Perez" },
        DescripcionObjeto = "Vehiculo sedan color rojo",
        DescripcionProblema = "El vehiculo no enciende",
        Estado = EstadoOrden.Diagnostico,
        FechaRecepcion = DateTime.Now,
        HistorialEstados = new List<EstadoOrden> { EstadoOrden.Recepcion, EstadoOrden.Diagnostico }
    };

    private static (EliminarEmpleadoService Servicio, EmpleadoRepositoryFalso Empleados, OrdenRepositoryEnMemoria Ordenes) Crear(params Empleado[] empleados)
    {
        var repositorio = new EmpleadoRepositoryFalso();
        repositorio.Guardados.AddRange(empleados);
        var ordenes = new OrdenRepositoryEnMemoria();
        return (new EliminarEmpleadoService(repositorio, ordenes), repositorio, ordenes);
    }

    [Fact]
    public void Ejecutar_ComoAdministradorSinOrdenes_EliminaAlEmpleado()
    {
        var admin = CrearEmpleado("Admin Uno", true);
        var ana = CrearEmpleado("Ana Torres", false);
        var (servicio, empleados, _) = Crear(admin, ana);

        servicio.Ejecutar(admin, ana);

        Assert.Same(admin, Assert.Single(empleados.Guardados));
    }

    [Fact]
    public void Ejecutar_ComoAdministrador_PuedeEliminarAOtroAdministrador()
    {
        var admin = CrearEmpleado("Admin Uno", true);
        var otro = CrearEmpleado("Admin Dos", true);
        var (servicio, empleados, _) = Crear(admin, otro);

        servicio.Ejecutar(admin, otro);

        Assert.Same(admin, Assert.Single(empleados.Guardados));
    }

    [Fact]
    public void Ejecutar_ComoUsuarioComun_LanzaExcepcionYNoElimina()
    {
        var comun = CrearEmpleado("Luis Perez", false);
        var ana = CrearEmpleado("Ana Torres", false);
        var (servicio, empleados, _) = Crear(comun, ana);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(comun, ana));

        Assert.Equal(2, empleados.Guardados.Count);
    }

    [Fact]
    public void Ejecutar_EliminandoseASiMismo_LanzaExcepcion()
    {
        var admin = CrearEmpleado("Admin Uno", true);
        var otro = CrearEmpleado("Admin Dos", true);
        var (servicio, empleados, _) = Crear(admin, otro);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(admin, admin));

        Assert.Equal(2, empleados.Guardados.Count);
    }

    [Fact]
    public void Ejecutar_EliminandoAlUnicoAdministrador_LanzaExcepcion()
    {
        var unico = CrearEmpleado("Admin Uno", true);
        var actual = CrearEmpleado("Admin Dos", true); // administrador de la sesion que ya no figura en la lista guardada
        var (servicio, empleados, _) = Crear(unico);

        Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(actual, unico));

        Assert.Single(empleados.Guardados);
    }

    [Theory]
    [InlineData("diagnostico")]
    [InlineData("cotizacion")]
    [InlineData("reparacion")]
    [InlineData("entrega")]
    public void Ejecutar_ConEmpleadoResponsableDeUnaOrden_LanzaExcepcionYNoElimina(string etapa)
    {
        var admin = CrearEmpleado("Admin Uno", true);
        var ana = CrearEmpleado("Ana Torres", false);
        var (servicio, empleados, ordenes) = Crear(admin, ana);
        var orden = CrearOrden();
        switch (etapa)
        {
            case "diagnostico": orden.EmpleadoDiagnostico = ana; break;
            case "cotizacion": orden.EmpleadoCotizacion = ana; break;
            case "reparacion": orden.EmpleadoReparacion = ana; break;
            default: orden.EmpleadoEntrega = ana; break;
        }
        ordenes.Agregar(orden);

        var error = Assert.Throws<InvalidOperationException>(() => servicio.Ejecutar(admin, ana));

        Assert.Contains("orden", error.Message);
        Assert.Equal(2, empleados.Guardados.Count);
    }

    [Fact]
    public void Ejecutar_ConOrdenesDeOtroEmpleado_EliminaAlEmpleadoLibre()
    {
        var admin = CrearEmpleado("Admin Uno", true);
        var ana = CrearEmpleado("Ana Torres", false);
        var luis = CrearEmpleado("Luis Perez", false);
        var (servicio, empleados, ordenes) = Crear(admin, ana, luis);
        var orden = CrearOrden();
        orden.EmpleadoDiagnostico = luis;
        ordenes.Agregar(orden);

        servicio.Ejecutar(admin, ana);

        Assert.DoesNotContain(ana, empleados.Guardados);
    }

    [Fact]
    public void Ejecutar_SinUsuarioActualOSinEmpleado_LanzaExcepcion()
    {
        var admin = CrearEmpleado("Admin Uno", true);
        var (servicio, _, _) = Crear(admin);

        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(null!, admin));
        Assert.Throws<ArgumentException>(() => servicio.Ejecutar(admin, null!));
    }
}
