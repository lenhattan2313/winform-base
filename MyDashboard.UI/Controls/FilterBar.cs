// Filter bar logic
using System;
using System.Windows.Forms;

namespace MyDashboard.UI.Controls
{
    public class FilterEventArgs : EventArgs
    {
        public string Line { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SearchText { get; set; }
    }

    public partial class FilterBar : UserControl
    {
        public event EventHandler<FilterEventArgs> FilterChanged;

        public FilterBar()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FilterChanged?.Invoke(this, new FilterEventArgs
            {
                Line = cboLine.SelectedItem?.ToString() ?? "All",
                FromDate = dtFrom.Value,
                ToDate = dtTo.Value,
                SearchText = txtSearch.Text
            });
        }
    }
}
