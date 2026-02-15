using MudBlazor;

namespace RISOS.Services;

public class ThemeStateService(LocalStorageService localStorageService)
{
    public bool IsDarkMode { get; private set; } = true;
    public event Action? OnChange;

    public static MudTheme Theme => new()
    {
        PaletteLight = new PaletteLight // TODO vybrat paletu, tohle je nějaká default vygenerovaná od AI
        {
            Primary = "#2563EB", // Vibrant blue
            Secondary = "#F97316", // Warm orange
            Background = "#F9FAFB", // Almost white
            Surface = "#FFFFFF", // Pure white cards
            AppbarBackground = "#2563EB",
            DrawerBackground = "#FFFFFF",
            DrawerText = "#1F2937", // Neutral dark grey
            TextPrimary = "#111827", // Almost black
            TextSecondary = "#4B5563", // Medium grey
            Success = "#16A34A", // Emerald green
            Warning = "#D97706", // Amber
            Error = "#DC2626", // Strong red
            Info = "#0EA5E9" // Sky blue
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