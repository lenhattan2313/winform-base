using System;
using System.Threading.Tasks;
using MyDashboard.WPF.Models;

namespace MyDashboard.WPF.Services.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly IAuthApiService _apiService;
        private User _currentUser;
        private string _token;
        private string _refreshToken;
        private DateTime _tokenExpiresAt;

        public AuthManager(IAuthApiService apiService)
        {
            _apiService = apiService;
        }

        public bool IsAuthenticated => _currentUser != null && IsTokenValid();
        public User CurrentUser => _currentUser;
        public string Token => _token;
        public string RefreshToken => _refreshToken;
        public DateTime TokenExpiresAt => _tokenExpiresAt;

        public void SetUser(LoginResponse loginResponse)
        {
            if (loginResponse.Success)
            {
                _currentUser = loginResponse.User;
                _token = loginResponse.Token;
                _refreshToken = loginResponse.RefreshToken;
                _tokenExpiresAt = loginResponse.ExpiresAt;
            }
        }

        public void ClearUser()
        {
            _currentUser = null;
            _token = null;
            _refreshToken = null;
            _tokenExpiresAt = DateTime.MinValue;
        }

        public bool IsTokenValid()
        {
            return !string.IsNullOrEmpty(_token) && _tokenExpiresAt > DateTime.Now;
        }

        public async Task<bool> RefreshTokenIfNeededAsync()
        {
            if (string.IsNullOrEmpty(_refreshToken) || _tokenExpiresAt > DateTime.Now.AddMinutes(5))
            {
                return true; // Token is still valid or no refresh token
            }

            try
            {
                var response = await _apiService.RefreshTokenAsync(_refreshToken);
                if (response.Success)
                {
                    SetUser(response);
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Token refresh failed: {ex.Message}");
            }

            return false;
        }
    }
}
