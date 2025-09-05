using System;

namespace MyDashboard.WPF.Models
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
    }
}
