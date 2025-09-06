using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyDashboard.WPF.Models;
using MyDashboard.WPF.Services;

namespace MyDashboard.WPF.Services.Report
{
    public interface IReportApiService : IBaseApiService<ReportRecord>
    {
        Task<List<ReportRecord>> GetReportsAsync(string station, DateTime fromDate, DateTime toDate, string search);
    }
} 