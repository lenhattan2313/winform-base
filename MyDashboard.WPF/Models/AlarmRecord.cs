using System;

namespace MyDashboard.WPF.Models
{
    public class AlarmRecord
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Line { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
