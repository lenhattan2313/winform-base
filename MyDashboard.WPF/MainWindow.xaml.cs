using System.Windows;
using MyDashboard.WPF.Views;

namespace MyDashboard.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ContentArea.Content = new AlarmPage();
        }

        private void Alarm_Click(object sender, RoutedEventArgs e) => ContentArea.Content = new AlarmPage();
        private void Report_Click(object sender, RoutedEventArgs e) => ContentArea.Content = new ReportPage();
        private void Setting_Click(object sender, RoutedEventArgs e) => ContentArea.Content = new SettingPage();
    }
}
