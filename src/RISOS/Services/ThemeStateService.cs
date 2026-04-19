using MudBlazor;
using RISOS.Enums;

namespace RISOS.Services;

public class ThemeStateService(LocalStorageService localStorageService)
{
    public bool IsDarkMode { get; private set; } = true;

    public ThemeType? CurrentThemeType { get; private set; }
    public bool IsInitialized { get; private set; }

    public MudTheme Theme => CurrentThemeType switch
    {
        ThemeType.Deuteranopia or ThemeType.Protanopia => GetDeuteranopiaTheme(),
        ThemeType.Tritanopia => GetTritanopiaTheme(),
        ThemeType.HighContrast => GetHighContrastTheme(),
        ThemeType.Default => GetDefaultTheme(),
        _ => GetLoadingTheme()
    };

    public event Action? OnChange;

    private static MudTheme GetLoadingTheme() => new()
    {
        PaletteLight = new PaletteLight { Primary = "#00000000" },
        PaletteDark = new PaletteDark { Primary = "#00000000" }
    };

    public async Task InitializeAsync()
    {
        IsDarkMode = await localStorageService.LoadTheme();
        CurrentThemeType = await localStorageService.LoadThemeType();
        IsInitialized = true;
        OnChange?.Invoke();
    }

    public async Task SetDarkMode(bool isDark)
    {
        IsDarkMode = isDark;
        await localStorageService.SaveTheme(isDark);
        OnChange?.Invoke();
    }

    public async Task SetThemeType(ThemeType type)
    {
        CurrentThemeType = type;
        await localStorageService.SaveThemeType(type);
        OnChange?.Invoke();
    }

    private static MudTheme GetDefaultTheme() => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#134E4A",
            Secondary = "#10B981",
            Tertiary = "#f43f5e",
            Background = "#F8FAFC",
            BackgroundGray = "#e4f5e8",
            Surface = "#FFFFFF",
            AppbarBackground = "#064e3b",
            DrawerBackground = "#064e3b",
            DrawerText = "#FFFFFF",
            DrawerIcon = "#FFFFFF",
            TextPrimary = "#1e293b",
            TextSecondary = "#64748b",
            Success = "#16A34A",
            Warning = "#ff8d37",
            Error = "#DC2626",
            Info = "#0068d4",
            Divider = "#e2e8f0",
            TableLines = "#e2e8f0",
            LinesDefault = "#e2e8f0"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#137045",
            DrawerText = "#FFFFFFEF",
            DrawerIcon = "#FFFFFFEF",
            TextPrimary = "#FFFFFFEF",
            ActionDefault = "#FFFFFFEF",
            Secondary = "#19bbd5",
            Tertiary = "#FF204E"
        }
    };

    private static MudTheme GetDeuteranopiaTheme() => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#0072B2",
            Secondary = "#E69F00",
            Background = "#F8FAFC",
            Surface = "#FFFFFF",
            TextPrimary = "#1A1A1A",
            TextSecondary = "#424242",
            AppbarBackground = "#0072B2",
            DrawerBackground = "#FFFFFF",
            DrawerText = "#0072B2",
            Success = "#009E73",
            ActionDefault = "#424242",
            Divider = "#E2E8F0",
            Error = "#D55E00"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#56B4E9",
            Secondary = "#F0E442",
            Background = "#0F172A",
            Surface = "#1E293B",
            TextPrimary = "#F8FAFC",
            TextSecondary = "#94A3B8"
        }
    };

    private static MudTheme GetTritanopiaTheme() => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#004852",
            Secondary = "#F4002D",
            Background = "#F8FAFC",
            Surface = "#FFFFFF",
            TextPrimary = "#1A1A1A",
            AppbarBackground = "#004852",
            DrawerBackground = "#FFFFFF",
            DrawerText = "#004852",
            Success = "#00F6FF",
            Error = "#FF3000"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#00CED1",
            Secondary = "#FF4500",
            Background = "#0A191B",
            Surface = "#152A2D",
            TextPrimary = "#F0F9FA"
        }
    };

    private static MudTheme GetHighContrastTheme() => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#000000",
            Secondary = "#1A1A1A",
            Success = "#006400",
            Error = "#B22222",
            Info = "#0000FF",
            Warning = "#FF8C00",
            AppbarBackground = "#000000",
            AppbarText = "#FFFFFF",
            Background = "#FFFFFF",
            Surface = "#F0F0F0",
            TextPrimary = "#000000",
            TextSecondary = "#212121",
            DrawerBackground = "#FFFFFF",
            DrawerText = "#000000",
            DrawerIcon = "#000000"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#FFFF00",
            Secondary = "#00FFFF",
            Success = "#00FF00",
            Error = "#FF0000",
            Background = "#000000",
            Surface = "#121212",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#FFFF00",
            AppbarBackground = "#000000",
            AppbarText = "#FFFF00",
            DrawerBackground = "#000000",
            DrawerText = "#FFFFFF",
            ActionDefault = "#FFFF00"
        }
    };
}