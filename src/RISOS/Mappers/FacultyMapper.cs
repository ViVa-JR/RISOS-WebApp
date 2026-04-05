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

        if (studyDuration <= 0)
        {
            studyDuration = 3.0;
            credits = credits != 0 ? credits : 180;
        }
        else if (credits > 0)
        {
            creditsWarn = false;
        }
        else
        {
            credits = (int)studyDuration * 60;
        }
        
        var name = dto.Name;
        if (dto.StudyType != string.Empty)
        {
            name += " (" + dto.StudyType + ")";   
        }
        
        return new StudyProgram(
            Title: name,
            Abbreviation: dto.Code,
            StudyDuration: studyDuration,
            Credits: credits,
            CreditsWarn: creditsWarn,
            Url: dto.Url,
            Specialization: dto.Specialization
        );
    }
}
