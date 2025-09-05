using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;

using MyDashboard.WPF.Models;
using MyDashboard.WPF.Services.Auth;

namespace MyDashboard.WPF.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;
        private readonly IAuthManager _authManager;

        private string _username;
        private string _password;
        private bool _isLoading;
        private string _errorMessage;
        private bool _hasError;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); ClearError(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); ClearError(); }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }

        public bool HasError
        {
            get => _hasError;
            set { _hasError = value; OnPropertyChanged(nameof(HasError)); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            // Get services from dependency injection
            _authService = App.ServiceProvider.GetRequiredService<AuthService>();
            _authManager = App.ServiceProvider.GetRequiredService<IAuthManager>();
            
            LoginCommand = new RelayCommand(async _ => await LoginAsync(), _ => !IsLoading && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password));
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                SetError("Please enter both username and password.");
                return;
            }

            IsLoading = true;
            ClearError();

            try
            {
                var loginRequest = new LoginRequest
                {
                    Username = Username,
                    Password = Password
                };

                var response = await _authService.LoginAsync(loginRequest);

                if (response.Success)
                {
                    _authManager.SetUser(response);
                    
                    // Close login window and show main window
                    var loginWindow = Application.Current.MainWindow;
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    loginWindow.Close();
                    Application.Current.MainWindow = mainWindow;
                }
                else
                {
                    SetError(response.Message ?? "Login failed. Please check your credentials.");
                }
            }
            catch (Exception ex)
            {
                SetError($"An error occurred: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void SetError(string message)
        {
            ErrorMessage = message;
            HasError = true;
        }

        private void ClearError()
        {
            if (HasError)
            {
                ErrorMessage = string.Empty;
                HasError = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
