using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MyDashboard.WPF.Services
{
    public abstract class BaseApiService<T> : IBaseApiService<T> where T : class
    {
        protected readonly HttpClient _httpClient;
        protected readonly IConfiguration _configuration;
        protected abstract string ApiEndpoint { get; }

        protected BaseApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public virtual async Task<List<T>> GetDataAsync(string line, DateTime from, DateTime to, string search)
        {
            try
            {
                var baseUrl = _configuration["ApiSettings:BaseUrl"];
                var queryParams = $"?line={Uri.EscapeDataString(line ?? "")}&from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}&search={Uri.EscapeDataString(search ?? "")}";
                
                var response = await _httpClient.GetAsync($"{baseUrl}{ApiEndpoint}{queryParams}");
                response.EnsureSuccessStatusCode();
                
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"API Error for {typeof(T).Name}: {ex.Message}");
                return new List<T>();
            }
        }
    }
}
