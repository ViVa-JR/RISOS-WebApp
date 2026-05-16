namespace RISOS.Pages.Home.Models;

public class StudyContext
{
    public string? ProgramAbbreviation { get; set; }
    public string? ProgramSpecialization { get; set; }
    public string? FacultyAbbreviation { get; set; }
    public int RequiredCredits { get; set; } = 180;
    public int? CreditOverride { get; set; }
    public int Years { get; set; } = 3;
    public bool CreditsWarn { get; set; } = true;
    public bool GpaPredictor { get; set; }

    public int Credits => CreditOverride ?? RequiredCredits;
}
