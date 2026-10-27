namespace GestorTaller.Negocio;

public enum ModuloTabla
{
    Clientes,
    Repuestos,
    Empleados,
    Ordenes
}

public enum AccionTabla
{
    Insertar,
    Modificar,
    Eliminar,
    Incrementar,
    Decrementar,
    EditarEmpleado,
    EditarUsuario,
    RestablecerContrasena,
    VerPasos,
    EditarEtapa,
    Retroceder
}

/// <summary>
/// Acciones que el usuario actual puede hacer en cada modulo (ver AccionesPermitidasTests). La lista
/// alimenta el combobox de acciones de cada fila y el boton para agregar: si una accion no esta aqui,
/// la pantalla no la ofrece. El orden de la lista es el orden en que se muestran.
/// </summary>
public class AccionesPermitidas
{
    private readonly PermisosUsuario _permisos;

    public AccionesPermitidas(PermisosUsuario permisos)
    {
        _permisos = permisos ?? throw new ArgumentException("Se requieren los permisos del usuario.", nameof(permisos));
    }

    public IReadOnlyList<AccionTabla> Para(ModuloTabla modulo)
    {
        var administrador = _permisos.EsAdministrador;

        return modulo switch
        {
            ModuloTabla.Clientes => Filtrar(
                (AccionTabla.Insertar, true),
                (AccionTabla.Modificar, true),
                (AccionTabla.Eliminar, administrador)),
            ModuloTabla.Repuestos => Filtrar(
                (AccionTabla.Insertar, true),
                (AccionTabla.Incrementar, true),
                (AccionTabla.Decrementar, administrador),
                (AccionTabla.Modificar, administrador),
                (AccionTabla.Eliminar, administrador)),
            ModuloTabla.Empleados => Filtrar(
                (AccionTabla.Insertar, administrador),
                (AccionTabla.EditarEmpleado, administrador),
                (AccionTabla.EditarUsuario, administrador),
                (AccionTabla.RestablecerContrasena, administrador),
                (AccionTabla.Eliminar, administrador)),
            ModuloTabla.Ordenes => Filtrar(
                (AccionTabla.VerPasos, true),
                (AccionTabla.EditarEtapa, administrador),
                (AccionTabla.Retroceder, administrador),
                (AccionTabla.Eliminar, administrador)),
            _ => throw new ArgumentOutOfRangeException(nameof(modulo), modulo, "Modulo desconocido.")
        };
    }

    public bool Permite(ModuloTabla modulo, AccionTabla accion) => Para(modulo).Contains(accion);

    private static IReadOnlyList<AccionTabla> Filtrar(params (AccionTabla Accion, bool Permitida)[] acciones) =>
        acciones.Where(a => a.Permitida).Select(a => a.Accion).ToList();
}
