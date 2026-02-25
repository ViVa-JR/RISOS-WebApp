using Microsoft.Extensions.Options;
using RISOS.Dto;
using RISOS.Mappers;
using RISOS.Models;
using RISOS.Options;
using RISOS.Pages.Home.Models;

namespace RISOS.Services;

public class UniversityService(IOptions<ApiOptions> options, LocalStorageService localStorageService, HttpClient httpClient)
{
    private const string ProgramsUrl = "/study_programmes/programmes.json";
    private const string SubjectsUrl = "/subjects/{programme_code}.json";
    private const string SpecificSubjectsUrl = "/subjects/{programme_code}/{specialization_code}.json";
    
    public async Task<List<Faculty>> GetFacultiesWithProgramsAsync()
    {
        var lang = await localStorageService.LoadLanguage();
        
        var response = await httpClient.GetAsync(options.Value.BaseUrl.Replace("{lang}", "en") + ProgramsUrl);
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to load faculties and programs");
        }
        
        var json = await response.Content.ReadAsStringAsync();
        var dtos = System.Text.Json.JsonSerializer.Deserialize<List<FacultyDto>>(json);
        
        return FacultyMapper.ToFaculties(dtos);
    }
    
    public async Task<List<Subject>> GetSubjectsAsync(StudyProgram program)
    {
        var lang = await localStorageService.LoadLanguage();

        HttpResponseMessage response;
        if (program.Specialization != null)
        {
            response = await httpClient.GetAsync(options.Value.BaseUrl.Replace("{lang}", lang) + SpecificSubjectsUrl
                .Replace("{programme_code}", program.Abbreviation)
                .Replace("{specialization_code}", program.Specialization));
        }
        else
        {
            response = await httpClient.GetAsync(options.Value.BaseUrl.Replace("{lang}", lang) + SubjectsUrl
                .Replace("{programme_code}", program.Abbreviation));
        }
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to load subjects");
        }
        
        var json = await response.Content.ReadAsStringAsync();
        var dtos = System.Text.Json.JsonSerializer.Deserialize<List<SubjectDto>>(json);
        
        return SubjectMapper.ToSubjects(dtos);
    }
}
