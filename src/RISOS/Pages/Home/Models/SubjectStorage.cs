namespace RISOS.Pages.Home.Models;

public class SubjectStorageEntry
{
    public string SubjectId { get; set; } = "";
    public bool? Completed { get; set; }
    public RISOS.Enums.Grade? Grade { get; set; }
    public int Attempt { get; set; } = 1;
    public bool LatestAttempt { get; set; } = true;
    public int IndexInZone { get; set; }
    public string Semester { get; set; } = SubjectEntry.Unassigned;
}
