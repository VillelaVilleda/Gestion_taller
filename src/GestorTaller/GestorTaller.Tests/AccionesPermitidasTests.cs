using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Acciones que cada rol puede hacer en cada modulo (issue #142). El orden de la lista es el
// orden en que se muestran en el combobox de acciones.
public class AccionesPermitidasTests
{
    private static AccionesPermitidas Para(bool esAdministrador)
    {
        var empleado = new Empleado { Id = Guid.NewGuid(), Nombre = "Carlos Lopez", EsAdministrador = esAdministrador };

        return new AccionesPermitidas(new PermisosUsuario(empleado));
    }

    [Fact]
    public void UsuarioComun_EnClientes_PuedeInsertarYModificar()
    {
        var acciones = Para(esAdministrador: false).Para(ModuloTabla.Clientes);

        Assert.Equal(new[] { AccionTabla.Insertar, AccionTabla.Modificar }, acciones);
    }

    [Fact]
    public void Administrador_EnClientes_PuedeInsertarModificarYEliminar()
    {
        var acciones = Para(esAdministrador: true).Para(ModuloTabla.Clientes);

        Assert.Equal(new[] { AccionTabla.Insertar, AccionTabla.Modificar, AccionTabla.Eliminar }, acciones);
    }

    [Fact]
    public void UsuarioComun_EnRepuestos_PuedeInsertarEIncrementar()
    {
        var acciones = Para(esAdministrador: false).Para(ModuloTabla.Repuestos);

        Assert.Equal(new[] { AccionTabla.Insertar, AccionTabla.Incrementar }, acciones);
    }

    [Fact]
    public void Administrador_EnRepuestos_PuedeTodo()
    {
        var acciones = Para(esAdministrador: true).Para(ModuloTabla.Repuestos);

        Assert.Equal(
            new[] { AccionTabla.Insertar, AccionTabla.Incrementar, AccionTabla.Decrementar, AccionTabla.Modificar, AccionTabla.Eliminar },
            acciones);
    }

    [Fact]
    public void UsuarioComun_EnEmpleados_NoPuedeHacerNada()
    {
        Assert.Empty(Para(esAdministrador: false).Para(ModuloTabla.Empleados));
    }

    [Fact]
    public void Administrador_EnEmpleados_PuedeTodo()
    {
        var acciones = Para(esAdministrador: true).Para(ModuloTabla.Empleados);

        Assert.Equal(
            new[]
            {
                AccionTabla.Insertar,
                AccionTabla.EditarEmpleado,
                AccionTabla.EditarUsuario,
                AccionTabla.RestablecerContrasena,
                AccionTabla.Eliminar
            },
            acciones);
    }

    [Fact]
    public void UsuarioComun_EnOrdenes_SoloPuedeVerPasos()
    {
        var acciones = Para(esAdministrador: false).Para(ModuloTabla.Ordenes);

        Assert.Equal(new[] { AccionTabla.VerPasos }, acciones);
    }

    [Fact]
    public void Administrador_EnOrdenes_PuedeVerPasosEditarRetrocederYEliminar()
    {
        var acciones = Para(esAdministrador: true).Para(ModuloTabla.Ordenes);

        Assert.Equal(
            new[] { AccionTabla.VerPasos, AccionTabla.EditarEtapa, AccionTabla.Retroceder, AccionTabla.Eliminar },
            acciones);
    }

    [Fact]
    public void Permite_DevuelveTrueSoloParaLasAccionesDelRol()
    {
        var comun = Para(esAdministrador: false);
        var administrador = Para(esAdministrador: true);

        Assert.True(comun.Permite(ModuloTabla.Repuestos, AccionTabla.Incrementar));
        Assert.False(comun.Permite(ModuloTabla.Repuestos, AccionTabla.Decrementar));
        Assert.True(administrador.Permite(ModuloTabla.Repuestos, AccionTabla.Decrementar));
        Assert.False(administrador.Permite(ModuloTabla.Clientes, AccionTabla.Incrementar));
    }

    [Fact]
    public void SinPermisos_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new AccionesPermitidas(null!));
    }

    [Fact]
    public void ModuloInexistente_LanzaArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Para(esAdministrador: true).Para((ModuloTabla)99));
    }
}
