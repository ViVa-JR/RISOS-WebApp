using Microsoft.JSInterop;
using RISOS.Enums;
using RISOS.Extensions;

namespace RISOS.Services;

public class LocalStorageService(IJSRuntime js)
{
    private const string UserTheme = "user-theme";
    private const string UserLanguage = "user-language";

    public async Task SaveTheme(bool isDark)
    {
        await js.InvokeVoidAsync("localStorage.setItem", UserTheme, isDark ? "dark" : "light");
    }

    public async Task<bool> LoadTheme()
    {
        try
        {
            return await js.InvokeAsync<string>("localStorage.getItem", UserTheme) == "dark";
        }
        catch
        {
            await js.InvokeVoidAsync("localStorage.removeItem", UserTheme);
            return true;
        }
    }
    
    public async Task SaveLanguage(string language)
    {
        await js.InvokeVoidAsync("localStorage.setItem", UserLanguage, language);
    }

    public async Task<string> LoadLanguage()
    {
        try
        {
            var language = await js.InvokeAsync<string>("localStorage.getItem", UserLanguage);
            return string.IsNullOrEmpty(language) ? AppLanguage.English.ToCultureString() : language;
        }
        catch
        {
            await js.InvokeVoidAsync("localStorage.removeItem", UserLanguage);
            return AppLanguage.English.ToCultureString();
        }
    }
}