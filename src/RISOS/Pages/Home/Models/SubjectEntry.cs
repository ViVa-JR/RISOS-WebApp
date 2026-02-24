using RISOS.Enums;
using RISOS.Models;

namespace RISOS.Pages.Home.Models;

public class SubjectEntry(Subject subject)
{
    public Subject Subject { get; } = subject;
    public string Semester { get; set; } = subject.Type == SubjectType.Compulsory ? $"{subject.MinSemester}" : Unassigned;
    public bool? Completed { get; set; }
    public bool Selected { get; set; } = subject.Type == SubjectType.Compulsory;
    public bool IsAssigned => Semester != Unassigned;
    
    public const string Unassigned = "unassigned";
}