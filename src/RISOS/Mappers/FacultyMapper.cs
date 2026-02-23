using RISOS.Dto;
using RISOS.Pages.Home.Models;

namespace RISOS.Mappers;

public static class FacultyMapper
{
    public static List<Faculty> ToFaculties(List<FacultyDto>? dtos)
    {
        return dtos?.Select(ToFaculty).ToList() ?? [];
    }

    private static Faculty ToFaculty(FacultyDto dto)
    {
        return new Faculty(
            Title: dto.Name,
            Abbreviation: dto.Code,
            Programs: dto.Programs.Select(ToProgram).ToList()
        );
    }

    private static StudyProgram ToProgram(ProgramDto dto)
    {
        var studyDuration = double.TryParse(dto.StudyDuration, out var duration) ? duration : 0.0;
        var credits = int.TryParse(dto.Credits, out var creditsValue) ? creditsValue : 0;
        var creditsWarn = true;
        
        return new StudyProgram(
            Title: dto.Name,
            Abbreviation: dto.Code,
            StudyDuration: studyDuration,
            Credits: credits,
            CreditsWarn: creditsWarn,
            Specialization: dto.Specialization
        );
    }
}