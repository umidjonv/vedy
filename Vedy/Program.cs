using Autofac;
using System.Windows.Forms;
using Vedy.Forms;
using Vedy.Services;

namespace Vedy
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        private static IContainer Container { get; set; }
        [STAThread]
        static void Main()
        {
            // Create your builder.
            var builder = new ContainerBuilder();

            // Usually you're only interested in exposing the type
            // via its interface:
            builder.RegisterType<NetworkClient>().As<INetworkClient>();
            builder.RegisterType<CompanyService>().As<ICompanyService>();

            builder.RegisterType<Main>();
            builder.RegisterType<SettlementForm>();
            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                ApplicationConfiguration.Initialize();
                Application.Run(new Main());
            }
        }
    }
}