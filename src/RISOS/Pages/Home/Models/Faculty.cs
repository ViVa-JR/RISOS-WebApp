namespace RISOS.Pages.Home.Models;

public record Faculty(string Title, string Abbreviation, List<StudyProgram> Programs)
{
    public bool IsExpanded { get; set; }
}