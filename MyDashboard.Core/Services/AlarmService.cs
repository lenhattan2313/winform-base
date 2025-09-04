using System;
using System.Collections.Generic;
using System.Linq;
using MyDashboard.Core.Models;
using MyDashboard.Data;

namespace MyDashboard.Core.Services
{
    public class AlarmService
    {
        private readonly AlarmRepository _repo = new();

        public List<AlarmRecord> GetAlarms(string line, DateTime from, DateTime to, string search)
        {
            var data = _repo.GetAlarms(from, to);

            if (!string.IsNullOrEmpty(line) && line != "All")
                data = data.Where(a => a.Line == line).ToList();

            if (!string.IsNullOrEmpty(search))
                data = data.Where(a => a.Message.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            return data;
        }
    }
}
