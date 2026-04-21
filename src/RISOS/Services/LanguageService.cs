using RISOS.Enums;
using RISOS.Extensions;

namespace RISOS.Services;

public class LanguageService(LocalStorageService localStorageService)
{
    private AppLanguage? _appLanguage;

    public async Task<AppLanguage> GetAppLanguageAsync()
    {
        if (_appLanguage != null)
        {
            return _appLanguage.Value;
        }

        var savedCultureString = await localStorageService.LoadLanguage();
        if (!savedCultureString.TryParseToAppLanguage(out var appLanguage))
        {
            await SetAppLanguageAsync(appLanguage);
        }

        return appLanguage;
    }

    public async Task SetAppLanguageAsync(AppLanguage language)
    {
        _appLanguage = language;
        var cultureString = language.ToCultureString();
        await localStorageService.SaveLanguage(cultureString);
    }
}
