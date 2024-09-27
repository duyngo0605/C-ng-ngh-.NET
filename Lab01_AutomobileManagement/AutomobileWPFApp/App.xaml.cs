using Microsoft.Extensions.DependencyInjection;
using AutomobileLibrary.Repository;
using System.Windows;
using System;
namespace AutomobileWPFApp
{
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            // Config for Dependency Injection (DI)
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        // --------------------------------------------------

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton(typeof(ICarRepository), typeof(CarRepository));
            services.AddSingleton<WindowCarManagement>();
        }

        // --------------------------------------------------

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var windowCarManagement = serviceProvider.GetService<WindowCarManagement>();
            windowCarManagement.Show();
        }
    }
}
