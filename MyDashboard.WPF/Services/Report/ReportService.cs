using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using MyDashboard.WPF.Models;

namespace MyDashboard.WPF.Services.Report
{
    public class ReportService
    {
        private readonly IReportApiService _apiService;
        private readonly IConfiguration _configuration;

        public ReportService(IConfiguration configuration, IReportApiService apiService = null)
        {
            _configuration = configuration;
            _apiService = apiService;
        }

        public async Task<List<ReportRecord>> GetReportsAsync(string station, DateTime fromDate, DateTime toDate, string search)
        {
            // Check if we should use API or JSON mock data
            var useApi = _configuration.GetValue<bool>("UseApi", false);
            
            if (useApi && _apiService != null)
            {
                return await GetReportsFromApiAsync(station, fromDate, toDate, search);
            }
            else
            {
                return await GetReportsFromJsonAsync(station, fromDate, toDate, search);
            }
        }

        private async Task<List<ReportRecord>> GetReportsFromApiAsync(string station, DateTime fromDate, DateTime toDate, string search)
        {
            try
            {
                // Call API service with the correct parameters
                return await _apiService.GetReportsAsync(station, fromDate, toDate, search);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"API Error: {ex.Message}");
                // Fallback to JSON data if API fails
                return await GetReportsFromJsonAsync(station, fromDate, toDate, search);
            }
        }

        private async Task<List<ReportRecord>> GetReportsFromJsonAsync(string station, DateTime fromDate, DateTime toDate, string search)
        {
            try
            {
                // Get the path to the JSON file
                var assembly = Assembly.GetExecutingAssembly();
                var resourcePath = "MyDashboard.WPF.Data.reports.json";
                
                using var stream = assembly.GetManifestResourceStream(resourcePath);
                if (stream == null)
                {
                    // Try file system path if embedded resource not found
                    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "reports.json");
                    if (File.Exists(filePath))
                    {
                        var fileContent = await File.ReadAllTextAsync(filePath);
                        var fileReports = JsonConvert.DeserializeObject<List<ReportRecord>>(fileContent) ?? new List<ReportRecord>();
                        System.Diagnostics.Debug.WriteLine($"Loaded {fileReports.Count} reports from file system");
                        
                        // Add debug logging for filter parameters
                        System.Diagnostics.Debug.WriteLine($"Station: {station}, From Date: {fromDate:yyyy-MM-dd}, To Date: {toDate:yyyy-MM-dd}, Search: '{search}'");
                        
                        return FilterReports(fileReports, station, fromDate, toDate, search);
                    }
                    return new List<ReportRecord>();
                }

                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                var reports = JsonConvert.DeserializeObject<List<ReportRecord>>(json) ?? new List<ReportRecord>();
                
                return FilterReports(reports, station, fromDate, toDate, search);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON Loading Error: {ex.Message}");
                return new List<ReportRecord>();
            }
        }

        private List<ReportRecord> FilterReports(List<ReportRecord> reports, string station, DateTime fromDate, DateTime toDate, string search)
        {
            var filtered = reports.Where(report => 
                (string.IsNullOrEmpty(station) || station == "All" || report.StationDisplay.Contains(station, StringComparison.OrdinalIgnoreCase)) &&
                report.EndTime >= fromDate && 
                report.EndTime <= toDate &&
                (string.IsNullOrEmpty(search) || 
                 report.Description.Contains(search, StringComparison.OrdinalIgnoreCase))
            ).ToList();
            
            System.Diagnostics.Debug.WriteLine($"Filtered {filtered.Count} reports from {reports.Count} total");
            return filtered;
        }
    }
} 