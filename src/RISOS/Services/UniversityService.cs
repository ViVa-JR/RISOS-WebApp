using Microsoft.Extensions.Options;
using RISOS.Dto;
using RISOS.Mappers;
using RISOS.Options;
using RISOS.Pages.Home.Models;

namespace RISOS.Services;

public class UniversityService(IOptions<ApiOptions> options, LocalStorageService localStorageService, HttpClient httpClient)
{
    private const string Url = "/study_programmes/programmes.json";
    
    public async Task<List<Faculty>> GetFacultiesWithProgramsAsync()
    {
        var lang = await localStorageService.LoadLanguage();
        
        var response = await httpClient.GetAsync(options.Value.BaseUrl.Replace("{lang}", lang) + Url);
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to load faculties and programs");
        }
        
        var json = await response.Content.ReadAsStringAsync();
        var dtos = System.Text.Json.JsonSerializer.Deserialize<List<FacultyDto>>(json);
        
        return FacultyMapper.ToFaculties(dtos);
    }
}