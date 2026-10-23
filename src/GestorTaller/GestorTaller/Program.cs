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
            // Debe ir antes de crear la primera ventana: los errores de los eventos de la
            // interfaz llegan a ThreadException en lugar de cerrar la aplicacion.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (_, e) => ManejarError(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (_, e) =>
                ManejarError(e.ExceptionObject as Exception ?? new InvalidOperationException("Error desconocido."));

            ApplicationConfiguration.Initialize();

            string cadenaDeConexion;

            try
            {
                cadenaDeConexion = ConexionBaseDeDatos.ObtenerCadenaDeConexion();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "GestorTaller", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var opciones = new DbContextOptionsBuilder<GestorTallerDbContext>()
                .UseNpgsql(cadenaDeConexion)
                .Options;
            var dbContext = new GestorTallerDbContext(opciones);

            IOrdenRepository ordenRepository = new OrdenRepositoryEfCore(dbContext);
            IClienteRepository clienteRepository = new ClienteRepositoryEfCore(dbContext);
            IEmpleadoRepository empleadoRepository = new EmpleadoRepositoryEfCore(dbContext);
            IRepuestoRepository repuestoRepository = new RepuestoRepositoryEfCore(dbContext);

            Application.Run(new FormLogin(ordenRepository, clienteRepository, empleadoRepository, repuestoRepository));
        }

        private static void ManejarError(Exception error)
        {
            RegistroErrores.Registrar(error);

            MessageBox.Show(
                "Ocurrio un error inesperado y la operacion no se completo. Intenta de nuevo; si el problema continua, " +
                "contacta a soporte. El detalle tecnico quedo guardado en un registro de errores.",
                "GestorTaller",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}