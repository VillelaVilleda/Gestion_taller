using GestorTaller.Datos;
using GestorTaller.Negocio;
using Microsoft.EntityFrameworkCore;

namespace GestorTaller
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var opciones = new DbContextOptionsBuilder<GestorTallerDbContext>()
                .UseNpgsql(ConexionBaseDeDatos.ObtenerCadenaDeConexion())
                .Options;
            var dbContext = new GestorTallerDbContext(opciones);

            IOrdenRepository ordenRepository = new OrdenRepositoryEfCore(dbContext);
            IEmpleadoRepository empleadoRepository = new EmpleadoRepositoryEfCore(dbContext);

            Application.Run(new FormLogin(ordenRepository, empleadoRepository));
        }
    }
}