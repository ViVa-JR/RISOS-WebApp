using MudBlazor;

namespace RISOS.Services;

public class ThemeStateService(LocalStorageService localStorageService)
{
    public bool IsDarkMode { get; private set; } = true;
    public event Action? OnChange;

    public static MudTheme Theme => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#FF204E",
            Secondary = "#c81e4b",
            Tertiary = "#5D0E41",
            Background = "#F2F0F0",
            BackgroundGray = "#fff0f4",
            Surface = "#FFFFFF",
            AppbarBackground = "#c81e4b",
            DrawerBackground = "#c81e4b",
            DrawerText = "#FFFFFF",
            DrawerIcon = "#FFFFFF",
            TextPrimary = "#00224D",
            TextSecondary = "#505081",
            Success = "#16A34A",
            Warning = "#ff8d37",
            Error = "#DC2626",
            Info = "#0068d4"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#509e7a",
            DrawerText = "#FFFFFFEF",
            DrawerIcon = "#FFFFFFEF",
            TextPrimary = "#FFFFFFEF",
            ActionDefault = "#FFFFFFEF",
            Secondary = "#19bbd5",
            Tertiary = "#FF204E",
        }
    };

    public async Task InitializeAsync()
    {
        IsDarkMode = await localStorageService.LoadTheme();
        OnChange?.Invoke();
    }

    public async Task SetDarkMode(bool isDark)
    {
        IsDarkMode = isDark;
        await localStorageService.SaveTheme(isDark);
        OnChange?.Invoke();
    }
}