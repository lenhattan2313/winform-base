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

        public async Task<List<ReportRecord>> GetReportsAsync(string station, DateTime? endTimeFilter, string search)
        {
            // Check if we should use API or JSON mock data
            var useApi = _configuration.GetValue<bool>("UseApi", false);
            
            if (useApi && _apiService != null)
            {
                return await GetReportsFromApiAsync(station, endTimeFilter, search);
            }
            else
            {
                return await GetReportsFromJsonAsync(station, endTimeFilter, search);
            }
        }

        private async Task<List<ReportRecord>> GetReportsFromApiAsync(string station, DateTime? endTimeFilter, string search)
        {
            try
            {
                // Call API service with the correct parameters
                return await _apiService.GetReportsAsync(search);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"API Error: {ex.Message}");
                // Fallback to JSON data if API fails
                return await GetReportsFromJsonAsync(station, endTimeFilter, search);
            }
        }

        private async Task<List<ReportRecord>> GetReportsFromJsonAsync(string station, DateTime? endTimeFilter, string search)
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
                        var filterText = endTimeFilter?.ToString("yyyy-MM-dd") ?? "No date filter";
                        System.Diagnostics.Debug.WriteLine($"Station: {station}, EndTime Filter: {filterText}, Search: '{search}'");
                        
                        return FilterReports(fileReports, station, endTimeFilter, search);
                    }
                    return new List<ReportRecord>();
                }

                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                var reports = JsonConvert.DeserializeObject<List<ReportRecord>>(json) ?? new List<ReportRecord>();
                
                return FilterReports(reports, station, endTimeFilter, search);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON Loading Error: {ex.Message}");
                return new List<ReportRecord>();
            }
        }

        private List<ReportRecord> FilterReports(List<ReportRecord> reports, string station, DateTime? endTimeFilter, string search)
        {
            var filterText = endTimeFilter?.ToString("yyyy-MM-dd") ?? "No date filter";
            System.Diagnostics.Debug.WriteLine($"Filtering {reports.Count} reports with Station: {station}, EndTime filter: {filterText} and search criteria:");
            
            var filtered = reports.Where(report => 
            {
                // Station filtering (only if station is not "All")
                var matchesStation = string.IsNullOrEmpty(station) || station == "All" || 
                    report.StationDisplay == station; // Compare with StationDisplay format
                
                // EndTime filtering (only if filter is provided)
                var matchesEndTime = !endTimeFilter.HasValue || 
                    report.DateTime.Date == endTimeFilter.Value.Date; // Match exact date
                
                // Search filtering (only if search text is provided)
                var matchesSearch = string.IsNullOrEmpty(search) || (
                    // Search in computed properties
                    report.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.Description.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.ReportType.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.Line.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.Status.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.GeneratedBy.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    
                    // Search in direct string properties
                    report.SiloName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.EndTime.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.Shift.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.ScaleName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.GroupName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.ScaleType.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.Operator.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.Spare_Ch1.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    
                    // Search in numeric fields (converted to string)
                    report.Id.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.Tare.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.Net.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.PauseNet.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.Gross.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.Target.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.RecheckedWT.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.Remains.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.StartWT.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.TotalWT.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.StartBags.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.TotalBags.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.ScaleNo.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    report.Station.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)
                );
                
                return matchesStation && matchesEndTime && matchesSearch;
            }).ToList();
            
            System.Diagnostics.Debug.WriteLine($"Filtered {filtered.Count} reports from {reports.Count} total using Station: {station}, EndTime filter: {filterText} and search term: '{search}'");
            return filtered;
        }
    }
} 