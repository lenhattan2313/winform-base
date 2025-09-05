using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using MyDashboard.WPF.Services.Alarm;
using MyDashboard.WPF.ViewModels;

namespace MyDashboard.WPF.Views.Alarm
{
    public partial class AlarmPage : UserControl
    {
        public AlarmPage()
        {
            InitializeComponent();
            
            // Get services from dependency injection
            var alarmService = App.ServiceProvider.GetRequiredService<AlarmService>();
            DataContext = new AlarmViewModel(alarmService);
        }
    }
}
