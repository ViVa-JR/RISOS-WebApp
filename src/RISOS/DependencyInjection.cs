using System.Globalization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MudExtensions.Services;
using RISOS.Extensions;
using RISOS.Options;
using RISOS.Services;

namespace RISOS;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddWebApplication(IConfiguration configuration) => services
            .AddBlazorServices()
            .AddScoped<ApiService>()
            .AddSingleton<ThemeStateService>()
            .AddSingleton<LocalStorageService>()
            .AddSingleton<ExportService>()
            .AddSingleton<ImportService>()
            .AddSingleton<LanguageService>()
            .AddScoped<GitRepositoryInfoService>()
            .AddScoped<UniversityService>()
            .AddScoped(_ => new HttpClient())
            .AddOptions(configuration)
            .AddLocalization();

        private IServiceCollection AddBlazorServices() => services
            .AddMudServices(config =>
            {
                config.SnackbarConfiguration.VisibleStateDuration = 3000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
            })
            .AddMudExtensions();

        private IServiceCollection AddOptions(IConfiguration configuration) => services.Configure<ApiOptions>(options =>
            configuration.GetSection(ApiOptions.Section).Bind(options));
    }

    extension(WebAssemblyHost host)
    {
        public async Task InitializeAppState()
        {
            var themeService = host.Services.GetRequiredService<ThemeStateService>();
            await themeService.InitializeAsync();

            var languageService = host.Services.GetRequiredService<LanguageService>();
            var appLanguage = await languageService.GetAppLanguageAsync();
            var culture = new CultureInfo(appLanguage.ToCultureString());
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}
