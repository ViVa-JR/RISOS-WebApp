using System.Text.Json.Serialization;
using RISOS.Common.Models;
using RISOS.Enums;

namespace RISOS.Pages.Home.Models;

public class SubjectEntry(Subject subject)
{
    public Subject Subject { get; } = subject;
    public bool? Completed { get; set; }
    public bool Selected { get; set; } = subject.Type == SubjectType.Compulsory;
    public bool IsAssigned => Semester != Unassigned;
    public string Semester { get; set; } = subject.Type == SubjectType.Compulsory
        ? $"{(subject.MinSemester == 0 ? Unassigned : subject.MinSemester)}"
        : Unassigned;
    public bool IsCustomSubject { get; init; }
    public int Attempt { get; set; } = 1;
    public bool LatestAttempt { get; set; } = true;
    public int IndexInZone { get; set; }
    
    public const string Unassigned = "unassigned";

    [JsonIgnore]
    public SubjectEntryKey Key => new (Subject.Id, Attempt);
}

public record SubjectEntryKey(string Id, int Attempt);