using AttachInstance.Core.Abstractions;
using AttachInstance.Infastructure.DataAccess;
using AttachInstance.Infastructure.Service;
using AttachInstance.UI.Screens;
using AttachInstance.UI.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace AttachInstance
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            // Core ETABS services (singleton to share across windows)
            services.AddSingleton<EtabsReader>();
            services.AddSingleton<EtabsRepository>();
            services.AddSingleton<IEtabsConnectionService, EtabsConnectionService>();

            // ViewModels
            services.AddTransient<MainViewModel>();

            // Windows
            services.AddTransient<MainWindow>();

            Services = services.BuildServiceProvider();

            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
