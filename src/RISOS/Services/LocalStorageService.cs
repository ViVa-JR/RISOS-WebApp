using Microsoft.JSInterop;
using RISOS.Enums;
using RISOS.Extensions;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace RISOS.Services;

public class LocalStorageService(IJSRuntime  js)
{
    private const string UserTheme = "user-theme";
    private const string UserLanguage = "user-language";
    private const string programAbbreviation = "subject-id";

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

    public async Task SaveProgramAbbreviation(string? ProgramAbbreviation)
    {
        if (string.IsNullOrEmpty(ProgramAbbreviation))
        {
            await js.InvokeVoidAsync("localStorage.removeItem", programAbbreviation);
        }
        else
        {
            await js.InvokeVoidAsync("localStorage.setItem", programAbbreviation, ProgramAbbreviation);
        }
    }

    public async Task<string?> LoadProgramAbbreviation()
    {
        try
        {
            var id = await js.InvokeAsync<string>("localStorage.getItem", programAbbreviation);
            return string.IsNullOrEmpty(id) ? null : id;
        }
        catch
        {
            await js.InvokeVoidAsync("localStorage.removeItem", programAbbreviation);
            return null;
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

    public async Task<AppState> GetExportStateAsync()
    {
        var state = new AppState();
        state.ProgramAbbreviation = await LoadProgramAbbreviation();
        return state;
    }
}