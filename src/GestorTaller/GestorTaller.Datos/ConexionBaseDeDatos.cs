namespace GestorTaller.Datos;

/// <summary>
/// Punto unico para resolver la cadena de conexion a la base de datos, usada
/// tanto por las herramientas de EF Core (dotnet ef) como por la aplicacion
/// en tiempo de ejecucion, para no duplicar el valor por defecto en dos
/// lugares.
/// </summary>
public static class ConexionBaseDeDatos
{
    public static string ObtenerCadenaDeConexion()
    {
        return Environment.GetEnvironmentVariable("GESTORTALLER_CONNECTION_STRING")
            ?? "Host=localhost;Database=gestor_taller;Username=admin;Password=201944260";
    }
}