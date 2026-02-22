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
            Info = "#0068d4",
            ActionDefault = "#FFFFFF"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#3B4CCA", // Soft bright blue for buttons and highlights
            Secondary = "#FFB86C", // Warm orange for accents
            Background = "#121421", // Very dark navy-blue, less harsh than pure black
            Surface = "#1E213A", // Dark slate blue for cards and surfaces
            AppbarBackground = "#272B47", // Slightly lighter than surface for app bar
            DrawerBackground = "#1A1C2B", // Darker drawer background
            DrawerText = "#E0E6F1", // Light gray text for contrast
            TextPrimary = "#E0E6F1", // Bright text, easy on eyes
            TextSecondary = "#A0A8C0", // Muted lighter gray for secondary text
            Success = "#4CAF50", // Fresh green for success
            Warning = "#FFC107", // Vibrant amber warning
            Error = "#FF5252", // Bright red error
            Info = "#40C4FF" // Cool cyan info
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