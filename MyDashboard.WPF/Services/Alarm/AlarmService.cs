using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using MyDashboard.WPF.Models;

namespace MyDashboard.WPF.Services.Alarm
{
    public class AlarmService
    {
        private readonly IAlarmApiService _apiService;
        private readonly IConfiguration _configuration;

        public AlarmService(IConfiguration configuration, IAlarmApiService apiService = null)
        {
            _configuration = configuration;
            _apiService = apiService;
        }

        public async Task<List<AlarmRecord>> GetAlarmsAsync(string line, DateTime from, DateTime to, string search)
        {
            // Check if we should use API or JSON mock data
            var useApi = _configuration.GetValue<bool>("UseApi", false);
            
            if (useApi && _apiService != null)
            {
                return await GetAlarmsFromApiAsync(line, from, to, search);
            }
            else
            {
                return await GetAlarmsFromJsonAsync(line, from, to, search);
            }
        }

        private async Task<List<AlarmRecord>> GetAlarmsFromApiAsync(string line, DateTime from, DateTime to, string search)
        {
            try
            {
                // Call API service with the correct parameters
                return await _apiService.GetAlarmsAsync(line, from, to, search);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"API Error: {ex.Message}");
                // Fallback to JSON data if API fails
                return await GetAlarmsFromJsonAsync(line, from, to, search);
            }
        }

        private async Task<List<AlarmRecord>> GetAlarmsFromJsonAsync(string line, DateTime from, DateTime to, string search)
        {
            try
            {
                // Get the path to the JSON file
                var assembly = Assembly.GetExecutingAssembly();
                var resourcePath = "MyDashboard.WPF.Data.alarms.json";
                
                using var stream = assembly.GetManifestResourceStream(resourcePath);
                if (stream == null)
                {
                    // Try file system path if embedded resource not found
                    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "alarms.json");
                    if (File.Exists(filePath))
                    {
                        var fileContent = await File.ReadAllTextAsync(filePath);
                        var fileAlarms = JsonConvert.DeserializeObject<List<AlarmRecord>>(fileContent) ?? new List<AlarmRecord>();
                        System.Diagnostics.Debug.WriteLine($"Loaded {fileAlarms.Count} alarms from file system");
                        
                        // Add debug logging for filter parameters
                        System.Diagnostics.Debug.WriteLine($"Filter Parameters - Line: '{line}', From: {from:yyyy-MM-dd}, To: {to:yyyy-MM-dd}, Search: '{search}'");
                        if (fileAlarms.Count > 0)
                        {
                            System.Diagnostics.Debug.WriteLine($"Sample alarm dates: {fileAlarms[0].DateTime:yyyy-MM-dd}, {fileAlarms[Math.Min(1, fileAlarms.Count - 1)].DateTime:yyyy-MM-dd}");
                        }
                        
                        return FilterAlarms(fileAlarms, line, from, to, search);
                    }
                    return new List<AlarmRecord>();
                }

                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                var alarms = JsonConvert.DeserializeObject<List<AlarmRecord>>(json) ?? new List<AlarmRecord>();
                
                return FilterAlarms(alarms, line, from, to, search);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON Loading Error: {ex.Message}");
                return new List<AlarmRecord>();
            }
        }

        private List<AlarmRecord> FilterAlarms(List<AlarmRecord> alarms, string line, DateTime from, DateTime to, string search)
        {
            var filtered = alarms.Where(alarm => 
                (string.IsNullOrEmpty(line) || line == "All" || alarm.Line.Contains(line, StringComparison.OrdinalIgnoreCase)) &&
                alarm.DateTime >= from && 
                alarm.DateTime <= to &&
                (string.IsNullOrEmpty(search) || 
                 alarm.Message.Contains(search, StringComparison.OrdinalIgnoreCase))
            ).ToList();
            
            System.Diagnostics.Debug.WriteLine($"Filtered {filtered.Count} alarms from {alarms.Count} total");
            return filtered;
        }
    }
}
