using Microsoft.EntityFrameworkCore;
using MyDashboard.WPF.Data;
using MyDashboard.WPF.Models;

namespace MyDashboard.WPF.Repositories
{
    public class AlarmRepository : IAlarmRepository
    {
        private readonly AlarmDbContext _context;

        public AlarmRepository(AlarmDbContext context)
        {
            _context = context;
        }

        public async Task<List<AlarmRecord>> GetAlarmsAsync(string line, DateTime from, DateTime to, string search)
        {
            var query = _context.Alarms.AsQueryable();

            // Filter by date range
            query = query.Where(a => a.DateTime >= from && a.DateTime <= to);

            // Filter by line
            if (!string.IsNullOrEmpty(line) && line != "All")
            {
                query = query.Where(a => a.Line == line);
            }

            // Filter by search text
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => a.Message.Contains(search));
            }

            return await query
                .OrderByDescending(a => a.DateTime)
                .ToListAsync();
        }
    }
}
