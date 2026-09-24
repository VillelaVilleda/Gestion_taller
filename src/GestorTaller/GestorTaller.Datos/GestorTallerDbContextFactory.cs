using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GestorTaller.Datos;

/// <summary>
/// Le indica a las herramientas de EF Core (dotnet ef) como construir el
/// DbContext al generar o aplicar migraciones desde la linea de comandos.
/// No se usa cuando la aplicacion corre normalmente; eso se conecta en una
/// issue futura (#13, "Conectar la aplicacion a los repositorios reales").
/// </summary>
public class GestorTallerDbContextFactory : IDesignTimeDbContextFactory<GestorTallerDbContext>
{
    public GestorTallerDbContext CreateDbContext(string[] args)
    {
        var cadenaConexion = Environment.GetEnvironmentVariable("GESTORTALLER_CONNECTION_STRING")
            ?? "Host=localhost;Database=gestor_taller;Username=admin;Password=201944260";

        var opciones = new DbContextOptionsBuilder<GestorTallerDbContext>()
            .UseNpgsql(cadenaConexion)
            .Options;

        return new GestorTallerDbContext(opciones);
    }
}
