using RISOS.Pages.Home.Models;

namespace RISOS.Services;

public class DummyUniversityService
{
    public async Task<List<Faculty>> GetFacultiesWithProgramsAsync()
    {
        // Simulace delšího načítání ze serveru (např. 1.2 vteřiny)
        await Task.Delay(1200); 

        return new List<Faculty>
        {
            new("Faculty of Information Technology", "FIT", new List<StudyProgram>
            {
                new("Computer Science", "CS"),
                new("Software Engineering", "SWE"),
                new("Artificial Intelligence", "AI")
            }),
            new("Faculty of Electrical Engineering", "FEEC", new List<StudyProgram>
            {
                new("Telecommunications", "TEL"),
                new("Microelectronics", "MIC"),
                new("Power Engineering", "PE")
            }),
            new("Faculty of Business and Management", "FBM", new List<StudyProgram>
            {
                new("Economics and Management", "EM"),
                new("Corporate Finance", "FIN")
            }),
            new("Faculty of Mechanical Engineering", "FME", new List<StudyProgram>
            {
                new("Mechatronics", "MEC"),
                new("Robotics", "ROB")
            })
        };
    }
}