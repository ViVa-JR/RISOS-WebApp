using MudBlazor;
using RISOS.Enums;

namespace RISOS.Extensions;

public static class SubjectTypeExtensions
{
    public static Color ToColor(this SubjectType subjectType) => subjectType switch
    {
        SubjectType.Elective => Color.Secondary,
        SubjectType.Compulsory => Color.Primary,
        SubjectType.CompulsoryElective => Color.Tertiary,
        _ => Color.Success
    };
}