using System;
using System.Collections.Generic;
using System.Linq;
using MyDashboard.WPF.Models;

namespace MyDashboard.WPF.Services
{
    public class AlarmService
    {
        private static readonly List<AlarmRecord> MockData = new()
        {
            new AlarmRecord { DateTime = DateTime.Now.AddMinutes(-5), Line = "Line1", Message = "Overheat" },
            new AlarmRecord { DateTime = DateTime.Now.AddMinutes(-2), Line = "Line2", Message = "Stopped" }
        };

        public List<AlarmRecord> GetAlarms(string line, DateTime from, DateTime to, string search)
        {
            var data = MockData.Where(a => a.DateTime >= from && a.DateTime <= to).ToList();

            if (!string.IsNullOrEmpty(line) && line != "All")
                data = data.Where(a => a.Line == line).ToList();

            if (!string.IsNullOrEmpty(search))
                data = data.Where(a => a.Message.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            return data;
        }
    }
}
