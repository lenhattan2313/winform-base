using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using MyDashboard.WPF.ViewModels;

namespace MyDashboard.WPF.Views.Main
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            
            // Set DataContext using dependency injection if needed
            // For now, we'll keep it simple without a ViewModel
            // DataContext = App.ServiceProvider.GetRequiredService<MainViewModel>();
        }
    }
}
