using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyDashboard.WPF.Models;

namespace MyDashboard.WPF.Services.Alarm
{
    public interface IAlarmApiService : IBaseApiService<AlarmRecord>
    {
        Task<List<AlarmRecord>> GetAlarmsAsync(string line, DateTime from, DateTime to, string search);
    }
}
