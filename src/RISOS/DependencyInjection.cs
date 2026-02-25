using MudBlazor.Services;
using MudExtensions.Services;
using RISOS.Options;
using RISOS.Services;

namespace RISOS;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddWebApplication(IConfiguration configuration)
        {
            return services
                .AddBlazorServices()
                .AddSingleton<ThemeStateService>()
                .AddSingleton<LocalStorageService>()
                .AddScoped<GitRepositoryInfoService>()
                .AddScoped<UniversityService>()
                .AddScoped(_ => new HttpClient())
                .AddOptions(configuration);
        }

        private IServiceCollection AddBlazorServices()
        {
            return services
                .AddMudServices(config =>
                {
                    config.SnackbarConfiguration.VisibleStateDuration = 3000;
                    config.SnackbarConfiguration.HideTransitionDuration = 500;
                    config.SnackbarConfiguration.ShowTransitionDuration = 500;
                })
                .AddMudExtensions();
        }

        private IServiceCollection AddOptions(IConfiguration configuration)
        {
            return services.Configure<ApiOptions>(options =>
                configuration.GetSection(ApiOptions.Section).Bind(options));
        }
    }
}
