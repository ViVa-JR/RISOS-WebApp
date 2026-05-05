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
    public static IServiceCollection AddWebApplication(this IServiceCollection services, IConfiguration configuration) => services
        .AddBlazorServices()
        .AddScoped<ApiService>()
        .AddScoped<AnalyticsService>()
        .AddSingleton<ThemeStateService>()
        .AddSingleton<ThemePreviewService>()
        .AddSingleton<LocalStorageService>()
        .AddSingleton<ExportService>()
        .AddSingleton<ImportService>()
        .AddSingleton<LanguageService>()
        .AddScoped<GitRepositoryInfoService>()
        .AddScoped<UniversityService>()
        .AddScoped<StudyPlanService>()
        .AddScoped(_ => new HttpClient())
        .AddOptions(configuration)
        .AddLocalization();

    private static IServiceCollection AddBlazorServices(this IServiceCollection services) => services
        .AddMudServices(config =>
        {
            config.SnackbarConfiguration.VisibleStateDuration = 3000;
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
        })
        .AddMudExtensions();

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration) => services.Configure<ApiOptions>(options =>
        configuration.GetSection(ApiOptions.Section).Bind(options));

    public static async Task InitializeAppState(this WebAssemblyHost host)
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
