using System;
using System.Threading.Tasks;
using MyDashboard.WPF.Models;

namespace MyDashboard.WPF.Services.Auth
{
    public interface IAuthManager
    {
        bool IsAuthenticated { get; }
        User CurrentUser { get; }
        string Token { get; }
        string RefreshToken { get; }
        DateTime TokenExpiresAt { get; }
        
        void SetUser(LoginResponse loginResponse);
        void ClearUser();
        bool IsTokenValid();
        Task<bool> RefreshTokenIfNeededAsync();
    }
}
