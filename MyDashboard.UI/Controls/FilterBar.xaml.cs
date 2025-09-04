using System;
using System.Windows;
using System.Windows.Controls;

namespace MyDashboard.UI.Controls
{
    public partial class FilterBar : UserControl
    {
        public event EventHandler<FilterEventArgs> FilterChanged;

        public FilterBar()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            FilterChanged?.Invoke(this, new FilterEventArgs
            {
                Line = cboLine.SelectedItem?.ToString() ?? "All",
                FromDate = dtFrom.SelectedDate ?? DateTime.Today,
                ToDate = dtTo.SelectedDate ?? DateTime.Now,
                SearchText = txtSearch.Text
            });
        }
    }
}
