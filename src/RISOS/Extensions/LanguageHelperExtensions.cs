using RISOS.Localization;

namespace RISOS.Extensions;

public static class LanguageHelperExtensions
{
    public static string Years(int count)
    {
        var absCount = Math.Abs(count);

        return absCount == 1
            ? string.Format(LanguageHelper.Years_One, count)
            : string.Format(absCount is >= 2 and <= 4
                ? LanguageHelper.Years_Few
                : LanguageHelper.Years_Many, count);
    }

    public static string Semester(int number)
    {
        return number <= 0
            ? LanguageHelper.RecognizedSemester
            : $"{number}. {LanguageHelper.Semester}";
    }
}
