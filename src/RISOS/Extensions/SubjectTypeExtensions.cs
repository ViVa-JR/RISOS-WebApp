using MudBlazor;
using RISOS.Enums;
using RISOS.Localization;

namespace RISOS.Extensions;

public static class SubjectTypeExtensions
{
    extension(SubjectType subjectType)
    {
        public Color ToColor() => subjectType switch
        {
            SubjectType.Elective => Color.Tertiary,
            SubjectType.Compulsory => Color.Primary,
            SubjectType.CompulsoryElective => Color.Secondary,
            _ => Color.Success
        };

        public string ToLabel() => subjectType switch
        {
            SubjectType.Compulsory => LanguageHelper.CompulsorySingle,
            SubjectType.CompulsoryElective => LanguageHelper.CompulsoryElectiveSingle,
            SubjectType.Elective => LanguageHelper.ElectiveSingle,
            _ => subjectType.ToString()
        };
    }
}
