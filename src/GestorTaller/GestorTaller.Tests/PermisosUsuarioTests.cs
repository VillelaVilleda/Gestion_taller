using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas de los permisos segun el rol del empleado (issue #111).
public class PermisosUsuarioTests
{
    private static PermisosUsuario Para(bool esAdministrador)
    {
        return new PermisosUsuario(new Empleado { Id = Guid.NewGuid(), Nombre = "Carlos Lopez", EsAdministrador = esAdministrador });
    }

    [Fact]
    public void Administrador_PuedeAdministrarEditarYEliminar()
    {
        var permisos = Para(esAdministrador: true);

        Assert.True(permisos.PuedeAdministrarEmpleados);
        Assert.True(permisos.PuedeEditar);
        Assert.True(permisos.PuedeEliminar);
    }

    [Fact]
    public void UsuarioComun_NoPuedeAdministrarEditarNiEliminar()
    {
        var permisos = Para(esAdministrador: false);

        Assert.False(permisos.PuedeAdministrarEmpleados);
        Assert.False(permisos.PuedeEditar);
        Assert.False(permisos.PuedeEliminar);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void CualquierRol_PuedeCrearOrdenesClientesYRepuestos(bool esAdministrador)
    {
        var permisos = Para(esAdministrador);

        Assert.True(permisos.PuedeCrearOrdenes);
        Assert.True(permisos.PuedeCrearClientes);
        Assert.True(permisos.PuedeCrearRepuestos);
    }

    [Fact]
    public void SinEmpleado_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => new PermisosUsuario(null!));
    }
}
