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

        public AlarmService(IAlarmApiService apiService, IConfiguration configuration)
        {
            _apiService = apiService;
            _configuration = configuration;
        }

        public async Task<List<AlarmRecord>> GetAlarmsAsync(string line, DateTime from, DateTime to, string search)
        {
            // Check if we should use API or JSON mock data
            var useApi = _configuration.GetValue<bool>("UseApi", false);
            
            if (useApi)
            {
                return await _apiService.GetAlarmsAsync(line, from, to, search);
            }
            else
            {
                return await GetJsonAlarmDataAsync(line, from, to, search);
            }
        }

        private async Task<List<AlarmRecord>> GetJsonAlarmDataAsync(string line, DateTime from, DateTime to, string search)
        {
            // Load JSON data from embedded resource
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "MyDashboard.WPF.Assets.Data.alarms.json";
            
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                // Fallback: try to load from file system
                var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Data", "alarms.json");
                if (File.Exists(jsonPath))
                {
                    var jsonContent = await File.ReadAllTextAsync(jsonPath);
                    var jsonData = JsonConvert.DeserializeObject<List<AlarmRecord>>(jsonContent) ?? new List<AlarmRecord>();
                    return ApplyFilters(jsonData, line, from, to, search);
                }
            }
            else
            {
                using var reader = new StreamReader(stream);
                var jsonContent = await reader.ReadToEndAsync();
                var jsonData = JsonConvert.DeserializeObject<List<AlarmRecord>>(jsonContent) ?? new List<AlarmRecord>();
                return ApplyFilters(jsonData, line, from, to, search);
            }

            // Return empty list if no data found
            return new List<AlarmRecord>();
        }

        private List<AlarmRecord> ApplyFilters(List<AlarmRecord> data, string line, DateTime from, DateTime to, string search)
        {
            var filteredData = data.Where(a => a.DateTime >= from && a.DateTime <= to).ToList();

            if (!string.IsNullOrEmpty(line) && line != "All")
                filteredData = filteredData.Where(a => a.Line == line).ToList();

            if (!string.IsNullOrEmpty(search))
                filteredData = filteredData.Where(a => a.Message.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredData.OrderByDescending(a => a.DateTime).ToList();
        }
    }
}
