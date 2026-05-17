using RISOS.Enums;
using RISOS.Extensions;
using RISOS.Pages.Home.Models;

namespace RISOS.Helpers;

public static class GpaCalculator
{
    public static double? CalculateWeightedGpa(IEnumerable<SubjectEntry> subjects, bool compulsoryOnly = false)
    {
        var filteredSubjects = subjects
            .Where(s => s.Grade.HasValue && s.Grade != Grade.F)
            .Where(s => s.Subject.CompletionType.IsGraded())
            .Where(s => !compulsoryOnly || s.Subject.Type == SubjectType.Compulsory)
            .ToList();

        if (filteredSubjects.Count == 0)
        {
            return null;
        }

        var totalWeight = filteredSubjects.Sum(s => GetGradeValue(s.Grade!.Value) * s.Subject.Credits);
        var totalCredits = filteredSubjects.Sum(s => s.Subject.Credits);

        if (totalCredits == 0)
        {
            return null;
        }

        return totalWeight / totalCredits;
    }

    private static double GetGradeValue(Grade grade) => grade switch
    {
        Grade.A => 1.0,
        Grade.B => 1.5,
        Grade.C => 2.0,
        Grade.D => 2.5,
        Grade.E => 3.0,
        _ => 4.0 // F
    };

    public static string FormatGpa(double? gpa) => gpa?.ToString("F2") ?? "-";
}
