using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyDashboard.WPF.Models;
using MyDashboard.WPF.Services;

namespace MyDashboard.WPF.Services.Report
{
    public class ReportApiService : BaseApiService<ReportRecord>, IReportApiService
    {
        protected override string ApiEndpoint => "/api/reports";

        public ReportApiService(HttpClient httpClient, IConfiguration configuration) 
            : base(httpClient, configuration)
        {
        }

        public async Task<List<ReportRecord>> GetReportsAsync(string station, DateTime fromDate, DateTime toDate, string search)
        {
            return await GetDataAsync(station, fromDate, toDate, search);
        }
    }
} 