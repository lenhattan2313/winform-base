using MyDashboard.WPF.Models;

namespace MyDashboard.WPF.Services.Auth
{
    public interface IAuthApiService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<bool> LogoutAsync();
        Task<LoginResponse> RefreshTokenAsync(string refreshToken);
        Task<bool> ValidateTokenAsync(string token);
    }
}
