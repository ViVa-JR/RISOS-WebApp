using Microsoft.JSInterop;

namespace RISOS.Services;

public class LocalStorageService(IJSRuntime js)
{
    private const string UserTheme = "user-theme";

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
}