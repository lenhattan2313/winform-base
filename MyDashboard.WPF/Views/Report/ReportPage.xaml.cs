using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using MyDashboard.WPF.Services.Report;
using MyDashboard.WPF.ViewModels;

namespace MyDashboard.WPF.Views.Report
{
    public partial class ReportPage : UserControl
    {
        public ReportPage()
        {
            InitializeComponent();
            
            // Get services from dependency injection
            var reportService = App.ServiceProvider.GetRequiredService<ReportService>();
            var excelExportService = App.ServiceProvider.GetRequiredService<IExcelExportService>();
            DataContext = new ReportViewModel(reportService, excelExportService);
        }
    }
}
