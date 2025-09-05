using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyDashboard.WPF.Models;

namespace MyDashboard.WPF.Services.Auth
{
    public class AuthService
    {
        private readonly IAuthApiService _apiService;
        private readonly IConfiguration _configuration;
        private readonly IAuthManager _authManager;

        public AuthService(IConfiguration configuration, IAuthManager authManager, IAuthApiService apiService = null)
        {
            _configuration = configuration;
            _authManager = authManager;
            _apiService = apiService;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // Check if we should use API or mock data
            var useApi = _configuration.GetValue<bool>("UseApi", false);
            
            if (useApi && _apiService != null)
            {
                return await _apiService.LoginAsync(request);
            }
            else
            {
                return await GetMockLoginResponseAsync(request);
            }
        }

        public async Task<bool> LogoutAsync()
        {
            var useApi = _configuration.GetValue<bool>("UseApi", false);
            
            if (useApi && _apiService != null)
            {
                return await _apiService.LogoutAsync();
            }
            else
            {
                return await GetMockLogoutResponseAsync();
            }
        }

        private async Task<LoginResponse> GetMockLoginResponseAsync(LoginRequest request)
        {
            // Simulate API delay
            await Task.Delay(1000);

            // Mock authentication logic
            if (request.Username == "admin" && request.Password == "admin123")
            {
                var user = new User
                {
                    Id = 1,
                    Username = "admin",
                    Role = "Administrator"
                };

                return new LoginResponse
                {
                    Success = true,
                    Token = "mock-jwt-token-" + Guid.NewGuid().ToString("N")[..8],
                    RefreshToken = "mock-refresh-token-" + Guid.NewGuid().ToString("N")[..8],
                    ExpiresAt = DateTime.Now.AddHours(8),
                    User = user,
                    Message = "Login successful"
                };
            }
            else if (request.Username == "operator" && request.Password == "operator123")
            {
                var user = new User
                {
                    Id = 2,
                    Username = "operator",
                    Role = "Operator"
                };

                return new LoginResponse
                {
                    Success = true,
                    Token = "mock-jwt-token-" + Guid.NewGuid().ToString("N")[..8],
                    RefreshToken = "mock-refresh-token-" + Guid.NewGuid().ToString("N")[..8],
                    ExpiresAt = DateTime.Now.AddHours(8),
                    User = user,
                    Message = "Login successful"
                };
            }
            else
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid username or password"
                };
            }
        }

        private async Task<bool> GetMockLogoutResponseAsync()
        {
            // Simulate API delay
            await Task.Delay(500);
            return true;
        }
    }
}
