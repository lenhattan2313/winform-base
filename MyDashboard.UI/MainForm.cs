// Main shell form with navigation
using System;
using System.Windows.Forms;
using MyDashboard.UI.Pages;

namespace MyDashboard.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadPage(new AlarmPage());
        }

        private void LoadPage(UserControl page)
        {
            contentPanel.Controls.Clear();
            page.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(page);
        }

        private void btnAlarm_Click(object sender, EventArgs e) => LoadPage(new AlarmPage());
        private void btnReport_Click(object sender, EventArgs e) => LoadPage(new ReportPage());
        private void btnSetting_Click(object sender, EventArgs e) => LoadPage(new SettingPage());
    }
}
