using GestorTaller.Datos;
using GestorTaller.Negocio;

namespace GestorTaller
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            IOrdenRepository ordenRepository = new OrdenRepositoryEnMemoria();
            Application.Run(new FormLogin(ordenRepository));
        }
    }
}