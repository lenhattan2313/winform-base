using System.Windows;
using System.Windows.Controls;
using MyDashboard.WPF.ViewModels;

namespace MyDashboard.WPF.Views.Auth
{
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
            
            // Handle PasswordBox changes since it doesn't support direct binding
            PasswordBox.PasswordChanged += OnPasswordChanged;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.Password = passwordBox.Password;
            }
        }
    }
}
