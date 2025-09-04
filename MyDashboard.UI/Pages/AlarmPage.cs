// Alarm page logic
using System;
using System.Windows.Forms;
using MyDashboard.Core.Services;
using MyDashboard.UI.Controls;

namespace MyDashboard.UI.Pages
{
    public partial class AlarmPage : UserControl
    {
        private readonly AlarmService _alarmService = new();

        public AlarmPage()
        {
            InitializeComponent();
            filterBar.FilterChanged += OnFilterChanged;
        }

        private void OnFilterChanged(object sender, FilterEventArgs e)
        {
            var alarms = _alarmService.GetAlarms(e.Line, e.FromDate, e.ToDate, e.SearchText);
            dataGridView1.DataSource = alarms;
        }
    }
}
