using RISOS.Enums;

namespace RISOS.Services;

public class ThemePreviewService
{
    private const string SvgTemplate = """
                                       <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 400 200" width="100%" style="max-width: 400px;">
                                           <defs>
                                               <clipPath id="clip-{ThemeId}">
                                                   <rect width="400" height="200" rx="16" />
                                               </clipPath>
                                               <filter id="shadow-{ThemeId}" x="-10%" y="-10%" width="120%" height="120%">
                                                   <feDropShadow dx="0" dy="4" stdDeviation="6" flood-opacity="0.1" />
                                               </filter>
                                           </defs>

                                           <g clip-path="url(#clip-{ThemeId})">
                                               <rect x="0" y="0" width="200" height="200" fill="{LightBg}" />
                                               <rect x="0" y="0" width="60" height="200" fill="{LightDrawer}" />
                                               <line x1="60" y1="0" x2="60" y2="200" stroke="#000000" stroke-opacity="0.1" stroke-width="1" />
                                               
                                               <rect x="75" y="25" width="110" height="35" rx="6" fill="{LightPrimary}" filter="url(#shadow-{ThemeId})" />
                                               <rect x="75" y="70" width="85" height="20" rx="4" fill="{LightSurface}" filter="url(#shadow-{ThemeId})" stroke="#000000" stroke-opacity="0.05" />
                                               <circle cx="170" cy="80" r="7" fill="{LightSecondary}" />
                                               
                                               <rect x="75" y="100" width="100" height="20" rx="4" fill="{LightSurface}" filter="url(#shadow-{ThemeId})" stroke="#000000" stroke-opacity="0.05" />
                                               <circle cx="170" cy="110" r="7" fill="{LightTertiary}" />

                                               <rect x="200" y="0" width="200" height="200" fill="{DarkBg}" />
                                               <rect x="200" y="0" width="60" height="200" fill="{DarkDrawer}" />
                                               <line x1="260" y1="0" x2="260" y2="200" stroke="#FFFFFF" stroke-opacity="0.1" stroke-width="1" />
                                               
                                               <rect x="275" y="25" width="110" height="35" rx="6" fill="{DarkPrimary}" filter="url(#shadow-{ThemeId})" />
                                               <rect x="275" y="70" width="85" height="20" rx="4" fill="{DarkSurface}" filter="url(#shadow-{ThemeId})" stroke="#FFFFFF" stroke-opacity="0.05" />
                                               <circle cx="370" cy="80" r="7" fill="{DarkSecondary}" />
                                               
                                               <rect x="275" y="100" width="100" height="20" rx="4" fill="{DarkSurface}" filter="url(#shadow-{ThemeId})" stroke="#FFFFFF" stroke-opacity="0.05" />
                                               <circle cx="370" cy="110" r="7" fill="{DarkTertiary}" />

                                               <line x1="200" y1="0" x2="200" y2="200" stroke="#888888" stroke-opacity="0.3" stroke-width="2" />
                                           </g>
                                       </svg>
                                       """;

    public static Task<string> GetPreviewSvgAsync(ThemeType theme)
    {
        var mudTheme = ThemeStateService.GetThemeByThemeType(theme);
        var light = mudTheme.PaletteLight;
        var dark = mudTheme.PaletteDark;

        var result = SvgTemplate
            .Replace("{ThemeId}", Guid.NewGuid().ToString())
            .Replace("{LightBg}", light.Background.ToString())
            .Replace("{LightDrawer}", light.DrawerBackground.ToString())
            .Replace("{LightPrimary}", light.Primary.ToString())
            .Replace("{LightSurface}", light.Surface.ToString())
            .Replace("{LightSecondary}", light.Secondary.ToString())
            .Replace("{LightTertiary}", light.Tertiary?.ToString() ?? light.Error.ToString())
            .Replace("{DarkBg}", dark.Background.ToString())
            .Replace("{DarkDrawer}", dark.DrawerBackground.ToString())
            .Replace("{DarkPrimary}", dark.Primary.ToString())
            .Replace("{DarkSurface}", dark.Surface.ToString())
            .Replace("{DarkSecondary}", dark.Secondary.ToString())
            .Replace("{DarkTertiary}", dark.Tertiary?.ToString() ?? dark.Error.ToString());

        return Task.FromResult(result);
    }
}
