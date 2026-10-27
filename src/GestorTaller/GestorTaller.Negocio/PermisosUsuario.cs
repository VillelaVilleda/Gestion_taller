namespace GestorTaller.Negocio;

/// <summary>
/// Permisos de un empleado segun su rol (ver PermisosUsuarioTests):
///   - el administrador puede administrar empleados, y editar y eliminar
///   - cualquier empleado autenticado puede crear ordenes, clientes y repuestos
///   - el usuario comun no puede administrar empleados, ni editar ni eliminar
/// </summary>
public class PermisosUsuario
{
    private readonly Empleado _empleado;

    public PermisosUsuario(Empleado empleado)
    {
        _empleado = empleado ?? throw new ArgumentException("Se requiere un empleado.", nameof(empleado));
    }

    public bool EsAdministrador => _empleado.EsAdministrador;

    public bool PuedeAdministrarEmpleados => _empleado.EsAdministrador;

    public bool PuedeEditar => _empleado.EsAdministrador;

    public bool PuedeEliminar => _empleado.EsAdministrador;

    public bool PuedeCrearOrdenes => true;

    public bool PuedeCrearClientes => true;

    public bool PuedeCrearRepuestos => true;
}
