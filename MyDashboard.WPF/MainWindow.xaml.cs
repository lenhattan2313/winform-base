using System;
using System.Windows;
using MyDashboard.WPF.Views;
using MyDashboard.WPF.Views.Auth;
using MyDashboard.WPF.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace MyDashboard.WPF
{
    public partial class MainWindow : Window
    {
        private readonly IAuthManager _authManager;
        private readonly AuthService _authService;

        public MainWindow()
        {
            InitializeComponent();
            ContentArea.Content = new AlarmPage();
            
            // Get services from DI container
            _authManager = App.ServiceProvider.GetService<IAuthManager>();
            _authService = App.ServiceProvider.GetService<AuthService>();
        }

        private void Alarm_Click(object sender, RoutedEventArgs e) => ContentArea.Content = new AlarmPage();
        private void Report_Click(object sender, RoutedEventArgs e) => ContentArea.Content = new ReportPage();
        private void Setting_Click(object sender, RoutedEventArgs e) => ContentArea.Content = new SettingPage();
        
        private async void Logout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Call logout service
                await _authService.LogoutAsync();
                
                // Clear user session
                _authManager.ClearUser();
                
                // Show login window and close main window
                var loginWindow = new LoginPage();
                loginWindow.Show();
                this.Close();
                Application.Current.MainWindow = loginWindow;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Logout failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
