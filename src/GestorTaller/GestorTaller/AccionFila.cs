using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Una accion de la columna Acciones de una tabla: que accion es, que hace con la fila y, si hace
    /// falta, cuando esta disponible (por ejemplo, "Eliminar orden" solo si no esta entregada).
    /// </summary>
    public record AccionFila<T>(AccionTabla Accion, Action<T> Ejecutar, Func<T, bool>? Disponible = null);

    /// <summary>Nombre e icono con que se muestra cada accion en el menu de la columna Acciones.</summary>
    internal static class PresentacionAccion
    {
        public static (string Nombre, string Icono) De(AccionTabla accion) => accion switch
        {
            AccionTabla.Insertar => ("Agregar", "plus"),
            AccionTabla.Modificar => ("Modificar", "pencil"),
            AccionTabla.Eliminar => ("Eliminar", "trash-2"),
            AccionTabla.Incrementar => ("Incrementar existencia", "plus"),
            AccionTabla.Decrementar => ("Decrementar existencia", "minus"),
            AccionTabla.EditarEmpleado => ("Editar empleado", "pencil"),
            AccionTabla.EditarUsuario => ("Editar usuario", "user"),
            AccionTabla.RestablecerContrasena => ("Restablecer contraseña", "key-round"),
            AccionTabla.VerPasos => ("Ver pasos", "eye"),
            AccionTabla.EditarEtapa => ("Editar etapa", "pencil"),
            AccionTabla.Retroceder => ("Retroceder etapa", "rotate-ccw"),
            _ => throw new ArgumentOutOfRangeException(nameof(accion), accion, "Accion sin descripcion.")
        };

        public static bool EsPeligrosa(AccionTabla accion) => accion == AccionTabla.Eliminar;
    }
}
