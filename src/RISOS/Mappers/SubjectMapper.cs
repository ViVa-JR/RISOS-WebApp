using RISOS.Dto;
using RISOS.Enums;
using RISOS.Models;

namespace RISOS.Mappers;

public static class SubjectMapper
{
    public static List<Subject> ToSubjects(List<SubjectDto>? dtos)
    {
        return dtos?.Select(ToSubject).ToList() ?? [];
    }

    private static Subject ToSubject(SubjectDto dto)
    {
        var credits = int.TryParse(dto.Credits, out var creditsValue) ? creditsValue : 0;
        var minSemester = int.TryParse(dto.MinSemester, out var minSemesterValue) ? minSemesterValue : 1;
        var subjectType = MapSubjectType(dto.Obligation);
        var semesterSeason = MapSemesterSeason(dto.Semesters);
        var completionType = MapCompletionType(dto.CompletionType);
        
        return new Subject(
            name: dto.Name,
            shortName: dto.Code,
            credits: credits,
            minSemester: minSemester,
            type: subjectType,
            semesterSeason: semesterSeason,
            completionType: completionType,
            url: dto.Url,
            groupId: dto.GroupId
        );
    }

    private static SubjectType MapSubjectType(string obligation)
    {
        return obligation.ToLowerInvariant() switch
        {
            "p" or "povinný" or "compulsory" => SubjectType.Compulsory,
            "pv" or "povinně volitelný" or "compulsory-elective" => SubjectType.CompulsoryElective,
            _ => SubjectType.Elective
        };
    }

    private static SemesterSeason MapSemesterSeason(List<string> semesters)
    {
        if (semesters.Count == 0)
            return SemesterSeason.Any;

        var hasWinter = semesters.Any(s => s.Contains('Z') || s.Contains("zim") || s.Contains("winter", StringComparison.InvariantCultureIgnoreCase));
        var hasSummer = semesters.Any(s => s.Contains('L') || s.Contains("let") || s.Contains("summer", StringComparison.InvariantCultureIgnoreCase));

        if (hasWinter && hasSummer)
            return SemesterSeason.Any;
        if (hasWinter)
            return SemesterSeason.Winter;
        if (hasSummer)
            return SemesterSeason.Summer;

        return SemesterSeason.Any;
    }

    private static CompletionType MapCompletionType(string completionType)
    {
        return completionType.ToUpperInvariant() switch
        {
            "Z" => CompletionType.Cr,
            "KZ" => CompletionType.GCr,
            "Z+ZK" or "ZZK" => CompletionType.CrEx,
            "ZK" => CompletionType.Ex,
            "KL" => CompletionType.Col,
            "KP" => CompletionType.Kp,
            "REC" => CompletionType.Rec,
            "SZZ" => CompletionType.Szz,
            "RZ" => CompletionType.RCr,
            "RKZ" => CompletionType.RgCr,
            "RZ+ZK" or "RZZK" => CompletionType.RCrEx,
            "RZK" => CompletionType.REx,
            "HS" => CompletionType.Hs,
            "HDS" => CompletionType.Hds,
            "UPZA" => CompletionType.UpZa,
            "RVOL" => CompletionType.RVol,
            "Z-FSP" => CompletionType.CrFsp,
            "DRZK" => CompletionType.DrEx,
            "REC+Z" => CompletionType.RecCr,
            "ZK-DD" => CompletionType.ExDd,
            "SPZK" => CompletionType.SpEx,
            "SPZ+ZK" => CompletionType.SpCrEx,
            "RKL" => CompletionType.RCol,
            _ => CompletionType.Cr
        };
    }
}



