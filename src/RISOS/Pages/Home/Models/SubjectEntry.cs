using RISOS.Enums;
using RISOS.Models;

namespace RISOS.Pages.Home.Models;

public class SubjectEntry(Subject subject)
{
    public Subject Subject { get; } = subject;
    public string Semester { get; set; } = subject.Type == SubjectType.Compulsory ? $"{subject.MinSemester}" : Unassigned;
    
    public bool IsAssigned => Semester != Unassigned;
    
    public const string Unassigned = "unassigned";
}