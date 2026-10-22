namespace GestorTaller.Datos;

/// <summary>
/// Punto unico para resolver la cadena de conexion a la base de datos, usada
/// tanto por las herramientas de EF Core (dotnet ef) como por la aplicacion
/// en tiempo de ejecucion. La cadena nunca se guarda en el codigo: debe
/// definirse en la variable de entorno GESTORTALLER_CONNECTION_STRING.
/// </summary>
public static class ConexionBaseDeDatos
{
    public const string NombreVariable = "GESTORTALLER_CONNECTION_STRING";

    public static string ObtenerCadenaDeConexion()
    {
        var cadena = Environment.GetEnvironmentVariable(NombreVariable);

        if (string.IsNullOrWhiteSpace(cadena))
        {
            throw new InvalidOperationException(
                $"Falta la variable de entorno {NombreVariable} con la cadena de conexion a PostgreSQL " +
                "(por ejemplo: Host=localhost;Port=5432;Database=<base>;Username=<usuario>;Password=<clave>).");
        }

        return cadena;
    }
}