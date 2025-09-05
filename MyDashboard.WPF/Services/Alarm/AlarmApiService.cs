using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyDashboard.WPF.Models;

namespace MyDashboard.WPF.Services.Alarm
{
    public class AlarmApiService : BaseApiService<AlarmRecord>, IAlarmApiService
    {
        protected override string ApiEndpoint => "/api/alarms";

        public AlarmApiService(HttpClient httpClient, IConfiguration configuration) 
            : base(httpClient, configuration)
        {
        }

        public async Task<List<AlarmRecord>> GetAlarmsAsync(string line, DateTime from, DateTime to, string search)
        {
            return await GetDataAsync(line, from, to, search);
        }
    }
}
