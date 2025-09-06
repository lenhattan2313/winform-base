using System.Collections.Generic;
using System.Threading.Tasks;
using MyDashboard.WPF.Models;

namespace MyDashboard.WPF.Services.Report
{
    public interface IExcelExportService
    {
        Task<bool> ExportReportsToExcelAsync(IEnumerable<ReportRecord> reports, string filePath = null);
    }
} 