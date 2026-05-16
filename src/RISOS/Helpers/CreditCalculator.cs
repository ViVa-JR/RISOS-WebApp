using RISOS.Common.Models;
using RISOS.Enums;
using RISOS.Pages.Home.Models;

namespace RISOS.Helpers;

public static class CreditCalculator
{
    public static Credits CalculateTotalCredits(IEnumerable<SubjectEntry> subjects)
    {
        var assignedSubjects = subjects.Where(x => x.Semester != SubjectEntry.Unassigned).ToArray();
        var attemptedCredits = CalculateCredits(assignedSubjects.Where(x => x.Completed != false));
        var completedCredits = CalculateCredits(assignedSubjects.Where(x => x.Completed == true));

        return new Credits(attemptedCredits, completedCredits);
    }

    private static int CalculateCredits(IEnumerable<SubjectEntry> subjects)
    {
        return subjects.GroupBy(x => x.Semester).Sum(semester =>
        {
            if (semester.Key is "-1" or "0")
            {
                return semester.Sum(x => x.Subject.Credits);
            }

            var normalCredits = semester.Where(x => !x.IsSport).Sum(x => x.Subject.Credits);
            var sportCredits = semester.Where(x => x.IsSport).DistinctBy(x => x.Subject.ShortName).Count();

            return normalCredits + Math.Min(2, sportCredits);
        });
    }

    public static Credits CalculateSemesterCredits(IEnumerable<SubjectEntry> subjects, int semesterIdentifier)
    {
        var semesterSubjects = subjects.Where(s => s.Semester == semesterIdentifier.ToString()).ToArray();
        var hasUnprocessed = semesterSubjects.Any(s => s.Completed == null);

        return semesterIdentifier <= 0
            ? CalculateSpecialSemesterCredits(semesterSubjects, hasUnprocessed)
            : CalculateNormalSemesterCredits(semesterSubjects, hasUnprocessed);
    }

    private static Credits CalculateSpecialSemesterCredits(SubjectEntry[] subjects, bool hasUnprocessed)
    {
        var registered = subjects.Sum(s => s.Subject.Credits);
        var completed = subjects.Where(s => s.Completed == true).Sum(s => s.Subject.Credits);

        return new Credits(registered, completed, false, false, hasUnprocessed);
    }

    private static Credits CalculateNormalSemesterCredits(SubjectEntry[] subjects, bool hasUnprocessed)
    {
        var registeredNormal = subjects.Where(s => !s.IsSport).Sum(s => s.Subject.Credits);
        var completedNormal = subjects.Where(s => s is { Completed: true, IsSport: false }).Sum(s => s.Subject.Credits);

        var sports = subjects.Where(s => s.IsSport).ToArray();
        var totalSportsRegistered = sports.Length;
        var uniqueSportsRegistered = sports.DistinctBy(s => s.Subject.ShortName).Count();
        var uniqueSportsCompleted = sports.Where(s => s.Completed == true).DistinctBy(s => s.Subject.ShortName).Count();

        return new Credits(
            Registered: registeredNormal + Math.Min(2, uniqueSportsRegistered),
            Completed: completedNormal + Math.Min(2, uniqueSportsCompleted),
            SportLimitReached: uniqueSportsRegistered > 2,
            HasDuplicateSports: totalSportsRegistered > uniqueSportsRegistered,
            HasUnprocessed: hasUnprocessed
        );
    }
}
