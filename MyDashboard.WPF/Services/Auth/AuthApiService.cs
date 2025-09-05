using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using MyDashboard.WPF.Models;

namespace MyDashboard.WPF.Services.Auth
{
    public class AuthApiService : IAuthApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AuthApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                var baseUrl = _configuration["ApiSettings:BaseUrl"];
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{baseUrl}/api/auth/login", content);
                response.EnsureSuccessStatusCode();
                
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginResponse>(responseJson);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login API Error: {ex.Message}");
                return new LoginResponse { Success = false, Message = "Login failed. Please try again." };
            }
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {
                var baseUrl = _configuration["ApiSettings:BaseUrl"];
                var response = await _httpClient.PostAsync($"{baseUrl}/api/auth/logout", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Logout API Error: {ex.Message}");
                return false;
            }
        }

        public async Task<LoginResponse> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                var baseUrl = _configuration["ApiSettings:BaseUrl"];
                var json = JsonConvert.SerializeObject(new { refreshToken });
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{baseUrl}/api/auth/refresh", content);
                response.EnsureSuccessStatusCode();
                
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginResponse>(responseJson);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Refresh Token API Error: {ex.Message}");
                return new LoginResponse { Success = false, Message = "Token refresh failed." };
            }
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                var baseUrl = _configuration["ApiSettings:BaseUrl"];
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                
                var response = await _httpClient.GetAsync($"{baseUrl}/api/auth/validate");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Token Validation API Error: {ex.Message}");
                return false;
            }
        }
    }
}
