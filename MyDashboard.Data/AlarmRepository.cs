using System;
using System.Collections.Generic;
using System.Linq;
using MyDashboard.Core.Models;

namespace MyDashboard.Data
{
    public class AlarmRepository
    {
        private static readonly List<AlarmRecord> MockData = new()
        {
            new AlarmRecord { DateTime = DateTime.Now.AddMinutes(-5), Line = "Line1", Message = "Overheat" },
            new AlarmRecord { DateTime = DateTime.Now.AddMinutes(-2), Line = "Line2", Message = "Stopped" }
        };

        public List<AlarmRecord> GetAlarms(DateTime from, DateTime to)
        {
            return MockData.Where(a => a.DateTime >= from && a.DateTime <= to).ToList();
        }
    }
}
