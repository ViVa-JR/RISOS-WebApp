using MudBlazor;
using RISOS.Enums;
using RISOS.Localization;

namespace RISOS.Extensions;

public static class SemesterSeasonExtensions
{
    extension(SemesterSeason season)
    {
        public Color ToColor() => season switch
        {
            SemesterSeason.Winter => Color.Info,
            SemesterSeason.Summer => Color.Warning,
            _ => Color.Success
        };

        public bool IsValidForSemester(int semester)
        {
            if (season is SemesterSeason.Any)
            {
                return true;
            }

            var isEvenSemester = semester % 2 == 0;

            return season is SemesterSeason.Summer ? isEvenSemester : !isEvenSemester;
        }

        public string ToLabel() => season switch
        {
            SemesterSeason.Winter => LanguageHelper.Winter,
            SemesterSeason.Summer => LanguageHelper.Summer,
            _ => season.ToString()
        };
    }
}