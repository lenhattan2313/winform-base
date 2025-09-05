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

            // Configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            // HTTP Client for API calls
            services.AddHttpClient<IAlarmApiService, AlarmApiService>(client =>
            {
                var baseUrl = configuration["ApiSettings:BaseUrl"];
                client.BaseAddress = new Uri(baseUrl);
                client.Timeout = TimeSpan.FromSeconds(configuration.GetValue<int>("ApiSettings:Timeout", 30));
            });

            services.AddHttpClient<IAuthApiService, AuthApiService>(client =>
            {
                var baseUrl = configuration["ApiSettings:BaseUrl"];
                client.BaseAddress = new Uri(baseUrl);
                client.Timeout = TimeSpan.FromSeconds(configuration.GetValue<int>("ApiSettings:Timeout", 30));
            });

            // Services
            services.AddScoped<AlarmService>();
            services.AddScoped<AuthService>();
            services.AddSingleton<IAuthManager, AuthManager>();

            return services.BuildServiceProvider();
        }
    }
}
