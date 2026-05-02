using RISOS.Common.Models;
using RISOS.Dto;
using RISOS.Enums;

namespace RISOS.Mappers;

public static class SubjectMapper
{
    public static List<Subject> ToSubjects(List<SubjectDto>? dtos) => dtos?.Select(ToSubject).ToList() ?? [];

    private static Subject ToSubject(SubjectDto dto)
    {
        var credits = int.TryParse(dto.Credits, out var creditsValue) ? creditsValue : 0;
        var year = int.TryParse(dto.Year, out var minSemesterValue) ? minSemesterValue : 1;
        var subjectType = MapSubjectType(dto.Obligation);
        var semesterSeason = MapSemesterSeason(dto.Semesters);
        var completionType = MapCompletionType(dto.CompletionType);

        var minSemester = 0;
        if (year > 0)
        {
            minSemester = (year - 1) * 2 + (semesterSeason == SemesterSeason.Winter ? 1 : 2);
        }

        var id = dto.Url.Split("/").LastOrDefault() ?? "" + dto.Name + dto.Code;

        return new Subject(
            id,
            dto.Name,
            dto.Code,
            credits,
            minSemester,
            subjectType,
            semesterSeason,
            completionType,
            url: dto.Url,
            groupId: dto.GroupId
        );
    }

    private static SubjectType MapSubjectType(string obligation) => obligation.ToLowerInvariant() switch
    {
        "p" or "povinný" or "compulsory" => SubjectType.Compulsory,
        "pv" or "povinně volitelný" or "compulsory-optional" => SubjectType.CompulsoryElective,
        "sport" => SubjectType.Sport,
        _ => SubjectType.Elective
    };

    private static SemesterSeason MapSemesterSeason(List<string> semesters)
    {
        if (semesters.Count == 0)
        {
            return SemesterSeason.Any;
        }

        var hasWinter = semesters.Any(s => s.Contains('Z') || s.Contains("zim") || s.Contains("winter", StringComparison.InvariantCultureIgnoreCase));
        var hasSummer = semesters.Any(s => s.Contains('L') || s.Contains("let") || s.Contains("summer", StringComparison.InvariantCultureIgnoreCase));

        return (hasWinter, hasSummer) switch
        {
            (true, true) => SemesterSeason.Any,
            (true, false) => SemesterSeason.Winter,
            (false, true) => SemesterSeason.Summer,
            _ => SemesterSeason.Any
        };
    }

    private static CompletionType MapCompletionType(string completionType)
    {
        return completionType.Trim().ToLowerInvariant() switch
        {
            "cr"      or "zá"      => CompletionType.Cr,
            "gcr"     or "kl"      => CompletionType.GCr,
            "cr,ex"   or "zá,zk"   => CompletionType.CrEx,
            "ex"      or "zk"      => CompletionType.Ex,
            "col"     or "kol"     => CompletionType.Col,
            "kp"                   => CompletionType.Kp,
            "rec"     or "u"       => CompletionType.Rec,
            "szz"                  => CompletionType.Szz,
            "rcr"     or "uzá"     => CompletionType.RCr,
            "rgcr"    or "ukl"     => CompletionType.RgCr,
            "rcr,ex"  or "uzá,uzk" => CompletionType.RCrEx,
            "rex"     or "uzk"     => CompletionType.REx,
            "hs"                   => CompletionType.Hs,
            "hds"                  => CompletionType.Hds,
            "upzá"                 => CompletionType.UpZa,
            "rvol"    or "uro"     => CompletionType.RVol,
            "cr,fsp"  or "zá,kp"   => CompletionType.CrFsp,
            "drex"    or "drzk"    => CompletionType.DrEx,
            "udrzk"                => CompletionType.RecCr,
            "-dd"                  => CompletionType.ExDd,
            "spex"    or "spzk"    => CompletionType.SpEx,
            "spcr,ex" or "spzá,zk" => CompletionType.SpCrEx,
            "rcol"    or "ukol"    => CompletionType.RCol,
            _                      => CompletionType.Cr
        };
    }
}
