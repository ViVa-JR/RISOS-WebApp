using MudBlazor.Services;
using RISOS.Services;

namespace RISOS;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddWebApplication()
        {
            return services
                .AddBlazorServices()
                .AddSingleton<ThemeStateService>()
                .AddSingleton<LocalStorageService>();
        }

        private IServiceCollection AddBlazorServices()
        {
            return services
                .AddMudServices(config =>
                {
                    config.SnackbarConfiguration.VisibleStateDuration = 3000;
                    config.SnackbarConfiguration.HideTransitionDuration = 500;
                    config.SnackbarConfiguration.ShowTransitionDuration = 500;
                });
        }
    }
}