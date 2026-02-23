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
            dto.Name,
            dto.Code,
            dto.Programs.Select(ToProgram).ToList()
        );
    }

    private static StudyProgram ToProgram(ProgramDto dto)
    {
        return new StudyProgram(
            dto.Name,
            dto.Code,
            dto.Specialization
        );
    }
}