using RISOS.Common.Models;
using RISOS.Enums;
using RISOS.Models;
using RISOS.Pages.Home.Models;

namespace RISOS.Pages.Home.Dialogs.AddCustomSubjectDialog;

public class CustomSubjectModel
{
    public CustomSubjectModel()
    {
    }

    public CustomSubjectModel(SubjectEntry subjectEntry)
    {
        Id = subjectEntry.Subject.Id;
        Name = subjectEntry.Subject.Name;
        ShortName = subjectEntry.Subject.ShortName;
        Credits = subjectEntry.Subject.Credits;
        MinSemester = subjectEntry.Subject.MinSemester;
        CompletionType = subjectEntry.Subject.CompletionType;
        SemesterSeason = subjectEntry.Subject.SemesterSeason;
        Type = subjectEntry.Subject.Type;
        GroupId = subjectEntry.Subject.GroupId;
        Semester = subjectEntry.Semester;
        Completed = subjectEntry.Completed;
        
    }

    public string Id { get; set; } = GenerateNewId();
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public int Credits { get; set; } = 1;
    public int MinSemester { get; set; } = 1;
    public CompletionType CompletionType { get; set; } = CompletionType.Cr;

    public SemesterSeason SemesterSeason { get; set; } = SemesterSeason.Any;
    public SubjectType Type { get; set; } = SubjectType.Compulsory;
    public string? GroupId { get; set; }
    private string Semester { get; set; } = SubjectEntry.Unassigned;
    private bool? Completed { get; set; }

    public static string GenerateNewId() => "custom-" + Guid.NewGuid();

    public SubjectEntry ToSubjectEntry(bool isCustom = false)
        => new(new Subject(Id, Name, ShortName, Credits, MinSemester, Type, SemesterSeason, CompletionType, GroupId))
        {
            Semester = Semester,
            Completed = Completed,
            IsCustomSubject = isCustom
        };
}