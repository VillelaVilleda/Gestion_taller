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
            IClienteRepository clienteRepository = new ClienteRepositoryEfCore(dbContext);
            IEmpleadoRepository empleadoRepository = new EmpleadoRepositoryEfCore(dbContext);
            IRepuestoRepository repuestoRepository = new RepuestoRepositoryEfCore(dbContext);

            Application.Run(new FormLogin(ordenRepository, clienteRepository, empleadoRepository, repuestoRepository));
        }
    }
}