using MudBlazor;
using RISOS.Enums;

namespace RISOS.Extensions;

public static class SemesterSeasonExtensions
{
    public static Color ToColor(this SemesterSeason season) => season switch
    {
        SemesterSeason.Winter => Color.Info,
        SemesterSeason.Summer => Color.Warning,
        _ => Color.Success
    };
}