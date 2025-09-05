using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MyDashboard.WPF.Configuration;
using MyDashboard.WPF.Views.Auth;

namespace MyDashboard.WPF
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Configure services for REST API
            ServiceProvider = ServiceConfiguration.ConfigureServices();

            // Show login page first
            var loginWindow = new LoginPage();
            loginWindow.Show();
            MainWindow = loginWindow;
        }
    }
}
