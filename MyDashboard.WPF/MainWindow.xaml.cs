using System;
using System.Windows;
using MyDashboard.WPF.Views;
using MyDashboard.WPF.Views.Auth;
using MyDashboard.WPF.Views.Alarm;
using MyDashboard.WPF.Views.Report;
using MyDashboard.WPF.Views.Setting;
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
            
            // Get services from DI container
            _authManager = App.ServiceProvider.GetService<IAuthManager>();
            _authService = App.ServiceProvider.GetService<AuthService>();
            
            // Check authentication status before loading content
            if (_authManager.IsAuthenticated)
            {
                ContentArea.Content = new AlarmPage();
            }
            else
            {
                // If not authenticated, redirect to login
                RedirectToLogin();
            }
        }

        private void Alarm_Click(object sender, RoutedEventArgs e) => NavigateToPage(() => new AlarmPage());
        private void Report_Click(object sender, RoutedEventArgs e) => NavigateToPage(() => new ReportPage());
        private void Setting_Click(object sender, RoutedEventArgs e) => NavigateToPage(() => new SettingPage());
        
        private void NavigateToPage(Func<object> pageFactory)
        {
            if (!_authManager.IsAuthenticated)
            {
                MessageBox.Show("Session expired. Please login again.", "Authentication Required", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                RedirectToLogin();
                return;
            }
            
            ContentArea.Content = pageFactory();
        }
        
        private async void Logout_Click(object sender, RoutedEventArgs e)
        
        {
            try
            {
                // Call logout service
                await _authService.LogoutAsync();
                
                // Clear user session
                _authManager.ClearUser();
                
                // Show login window and close main window
                RedirectToLogin();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Logout failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void RedirectToLogin()
        {
            var loginWindow = new LoginPage();
            loginWindow.Show();
            this.Close();
            Application.Current.MainWindow = loginWindow;
        }
    }
}
