using RISOS.Pages.Home.Models;

namespace RISOS.Services;

public class AppState
{
    public string? ProgramAbbreviation { get; set; }

    public string? ProgramSpecialization { get; set; }
    public int? CreditOverride { get; set; }
    public int? StudyYears { get; set; }
    public bool RecognizedYear { get; set; } = false;
    public List<SubjectStorage> Subjects { get; set; } = new();

    public List<SubjectEntry> CustomSubjects { get; set; } = new();
}
