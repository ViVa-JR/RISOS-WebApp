using RISOS.Enums;

namespace RISOS.Extensions;

public static class AppLanguageExtensions
{
    public static string ToCultureString(this AppLanguage language) => language switch
    {
        AppLanguage.English => "en-US",
        AppLanguage.Czech => "cs-CZ",
        _ => "en-US"
    };

    public static bool TryParseToAppLanguage(this string? culture, out AppLanguage language)
    {
        switch (culture)
        {
            case "en-US":
                language = AppLanguage.English;
                break;
            case "cs-CZ":
                language = AppLanguage.Czech;
                break;
            default:
                language = AppLanguage.English;
                return false;
        }

        return true;
    }
}