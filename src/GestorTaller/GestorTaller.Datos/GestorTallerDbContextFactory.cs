using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GestorTaller.Datos;

/// <summary>
/// Le indica a las herramientas de EF Core (dotnet ef) como construir el
/// DbContext al generar o aplicar migraciones desde la linea de comandos.
/// No se usa cuando la aplicacion corre normalmente; eso lo hace Program.cs,
/// usando la misma ConexionBaseDeDatos.
/// </summary>
public class GestorTallerDbContextFactory : IDesignTimeDbContextFactory<GestorTallerDbContext>
{
    public GestorTallerDbContext CreateDbContext(string[] args)
    {
        var opciones = new DbContextOptionsBuilder<GestorTallerDbContext>()
            .UseNpgsql(ConexionBaseDeDatos.ObtenerCadenaDeConexion())
            .Options;

        return new GestorTallerDbContext(opciones);
    }
}
