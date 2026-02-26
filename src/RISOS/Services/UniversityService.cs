using Microsoft.Extensions.Options;
using RISOS.Common.Models;
using RISOS.Dto;
using RISOS.Extensions;
using RISOS.Mappers;
using RISOS.Models;
using RISOS.Options;
using RISOS.Pages.Home.Models;

namespace RISOS.Services;

public class UniversityService(IOptions<ApiOptions> options, LanguageService languageService, HttpClient httpClient)
{
    private const string ProgramsUrl = "/study_programmes/programmes.json";
    private const string SubjectsUrl = "/subjects/{programme_code}.json";
    private const string SpecificSubjectsUrl = "/subjects/{programme_code}/{specialization_code}.json";

    public async Task<List<Faculty>> GetFacultiesWithProgramsAsync()
    {
        var lang = await languageService.GetAppLanguageAsync();
        
        var response = await httpClient.GetAsync(options.Value.BaseUrl.Replace("{lang}", lang.ToCultureString()) + ProgramsUrl);
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to load faculties and programs");
        }

        var json = await response.Content.ReadAsStringAsync();
        var dtos = System.Text.Json.JsonSerializer.Deserialize<List<FacultyDto>>(json);

        return FacultyMapper.ToFaculties(dtos);
    }

    public async Task<Result<List<Subject>>> GetSubjectsAsync(StudyProgram program)
    {
        try
        {
            var lang = await languageService.GetAppLanguageAsync();

            HttpResponseMessage response;
            if (program.Specialization != null)
            {
                response = await httpClient.GetAsync(options.Value.BaseUrl.Replace("{lang}", lang.ToCultureString()) + SpecificSubjectsUrl
                    .Replace("{programme_code}", program.Abbreviation)
                    .Replace("{specialization_code}", program.Specialization));
            }
            else
            {
                response = await httpClient.GetAsync(options.Value.BaseUrl.Replace("{lang}", lang.ToCultureString()) + SubjectsUrl
                    .Replace("{programme_code}", program.Abbreviation));
            }

            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure<List<Subject>>("Failed to load subjects");
            }

            var json = await response.Content.ReadAsStringAsync();
            var dtos = System.Text.Json.JsonSerializer.Deserialize<List<SubjectDto>>(json);

            return SubjectMapper.ToSubjects(dtos);
        }
        catch (Exception e)
        {
            return Result.Failure<List<Subject>>(e.Message);
        }
    }
}
