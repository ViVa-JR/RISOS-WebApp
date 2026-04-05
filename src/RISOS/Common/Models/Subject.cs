using RISOS.Enums;

namespace RISOS.Common.Models;

public class Subject(
    string id,
    string name,
    string shortName,
    int credits,
    int minSemester = 1,
    SubjectType type = SubjectType.Elective,
    SemesterSeason semesterSeason = SemesterSeason.Winter,
    CompletionType completionType = CompletionType.Cr,
    string? groupId = null,
    string? url = null
)
{
    public string Id { get; set; } = id;
    public string Name { get; } = name;
    public string ShortName { get; } = shortName;
    public int Credits { get; } = credits;
    public int MinSemester { get; } = minSemester;
    public CompletionType CompletionType { get; set; } = completionType;
    
    public SemesterSeason SemesterSeason { get; } = semesterSeason;
    public SubjectType Type { get; } = type;
    public string? GroupId { get; } = groupId;

    public string? Url { get; } = url;
}