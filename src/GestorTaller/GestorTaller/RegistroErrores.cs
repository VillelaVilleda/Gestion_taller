namespace GestorTaller
{
    /// <summary>
    /// Registro tecnico de errores inesperados: un archivo de texto por dia en la carpeta de datos
    /// locales del usuario de Windows (%LocalAppData%\GestorTaller\logs). Guarda la fecha, el nombre
    /// del empleado con sesion y el detalle del error. No guarda contrasenas, ni inicios de sesion,
    /// ni datos de clientes, y no tiene pantalla en la aplicacion.
    /// </summary>
    internal static class RegistroErrores
    {
        public static string? UsuarioActual { get; set; }

        public static string Carpeta { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GestorTaller", "logs");

        public static void Registrar(Exception error)
        {
            try
            {
                Directory.CreateDirectory(Carpeta);

                var archivo = Path.Combine(Carpeta, $"errores-{DateTime.Now:yyyy-MM-dd}.log");
                var texto = $"[{DateTime.Now:HH:mm:ss}] Usuario: {UsuarioActual ?? "(sin sesion)"}{Environment.NewLine}{error}{Environment.NewLine}{Environment.NewLine}";

                File.AppendAllText(archivo, texto);
            }
            catch (IOException)
            {
                // Si no se puede escribir el log, no se debe provocar otro error encima del original.
            }
            catch (UnauthorizedAccessException)
            {
            }
        }
    }
}
