using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyDashboard.WPF.Services;
using MyDashboard.WPF.Services.Alarm;
using MyDashboard.WPF.Services.Auth;

namespace MyDashboard.WPF.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            try
            {
                // Configuration
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                services.AddSingleton<IConfiguration>(configuration);

                // Only register HTTP clients if API is enabled and URL is valid
                var useApi = configuration.GetValue<bool>("UseApi", false);
                if (useApi)
                {
                    var baseUrl = configuration["ApiSettings:BaseUrl"];
                    if (!string.IsNullOrEmpty(baseUrl) && Uri.IsWellFormedUriString(baseUrl, UriKind.Absolute))
                    {
                        // HTTP Client for API calls
                        services.AddHttpClient<IAlarmApiService, AlarmApiService>(client =>
                        {
                            client.BaseAddress = new Uri(baseUrl);
                            client.Timeout = TimeSpan.FromSeconds(configuration.GetValue<int>("ApiSettings:Timeout", 30));
                        });

                        services.AddHttpClient<IAuthApiService, AuthApiService>(client =>
                        {
                            client.BaseAddress = new Uri(baseUrl);
                            client.Timeout = TimeSpan.FromSeconds(configuration.GetValue<int>("ApiSettings:Timeout", 30));
                        });
                    }
                }

                // Always register core services (they handle mock data internally when API is disabled)
                services.AddScoped<AlarmService>();
                services.AddScoped<AuthService>();
                services.AddSingleton<IAuthManager, AuthManager>();

                return services.BuildServiceProvider();
            }
            catch (Exception ex)
            {
                // Fallback service provider with minimal services
                System.Diagnostics.Debug.WriteLine($"Service configuration error: {ex.Message}");
                
                // Create a minimal configuration
                var fallbackServices = new ServiceCollection();
                var fallbackConfig = new ConfigurationBuilder().Build();
                fallbackServices.AddSingleton<IConfiguration>(fallbackConfig);
                fallbackServices.AddSingleton<IAuthManager, AuthManager>();
                fallbackServices.AddScoped<AuthService>();
                
                return fallbackServices.BuildServiceProvider();
            }
        }
    }
}
