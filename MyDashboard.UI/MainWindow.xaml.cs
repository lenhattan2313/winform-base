using System.Windows;
using MyDashboard.UI.Pages;

namespace MyDashboard.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadPage(new AlarmPage());
        }

        private void LoadPage(UserControl page)
        {
            contentPanel.Content = page;
        }

        private void BtnAlarm_Click(object sender, RoutedEventArgs e)
        {
            LoadPage(new AlarmPage());
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            LoadPage(new ReportPage());
        }

        private void BtnSetting_Click(object sender, RoutedEventArgs e)
        {
            LoadPage(new SettingPage());
        }
    }
}
