namespace RISOS.Pages.Home.Models;

public class StudyContext
{
    public int RequiredCredits { get; set; } = 180;
    public int? CreditOverride { get; set; } = null;
    public int Years { get; set; } = 3;
    public bool CreditsWarn { get; set; } = true;

    public int Credits => CreditOverride ?? RequiredCredits;
}
