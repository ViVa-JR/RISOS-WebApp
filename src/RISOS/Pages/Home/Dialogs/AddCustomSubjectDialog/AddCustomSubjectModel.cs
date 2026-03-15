using RISOS.Enums;
using RISOS.Models;
using RISOS.Pages.Home.Models;

namespace RISOS.Pages.Home.Dialogs.AddCustomSubjectDialog;

public class AddCustomSubjectModel
{
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public int Credits { get; set; } = 1;
    public int MinSemester { get; set; } = 1;
    public CompletionType CompletionType { get; set; } = CompletionType.Cr;

    public SemesterSeason SemesterSeason { get; set; } = SemesterSeason.Any;
    public SubjectType Type { get; set; } = SubjectType.Compulsory;

    public string? GroupId { get; set; } = null;

    public SubjectEntry ToSubjectEntry()
        => new(new Subject("custom-" + Name + ShortName, Name, ShortName, Credits, MinSemester, Type, SemesterSeason, CompletionType, GroupId))
        {
            Semester = SubjectEntry.Unassigned
        };
}