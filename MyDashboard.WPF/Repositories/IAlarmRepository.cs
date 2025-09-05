using MyDashboard.WPF.Models;

namespace MyDashboard.WPF.Repositories
{
    public interface IAlarmRepository
    {
        Task<List<AlarmRecord>> GetAlarmsAsync(string line, DateTime from, DateTime to, string search);
    }
}
