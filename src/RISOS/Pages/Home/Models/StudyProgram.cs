namespace RISOS.Pages.Home.Models;

public record StudyProgram(string Title, string Abbreviation, double StudyDuration, int Credits, bool CreditsWarn, string? Specialization = null);
