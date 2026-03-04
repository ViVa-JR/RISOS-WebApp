using Microsoft.Extensions.Options;
using RISOS.Common.Models;
using RISOS.Dto;
using RISOS.Extensions;
using RISOS.Mappers;
using RISOS.Models;
using RISOS.Options;
using RISOS.Pages.Home.Models;

namespace RISOS.Services;

public class UniversityService(IOptions<ApiOptions> options, LanguageService languageService, ApiService apiService)
{
    private const string ProgramsUrl = "/study_programmes/programmes.json";
    private const string SubjectsUrl = "/subjects/{programme_code}.json";
    private const string SpecificSubjectsUrl = "/subjects/{programme_code}/{specialization_code}.json";

    private async Task<string> PrepareUrlAsync(string endpoint)
    {
        var lang = await languageService.GetAppLanguageAsync();
        var baseUrl = options.Value.BaseUrl.Replace("{lang}", lang.ToCultureString());

        return $"{baseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
    }

    public async Task<Result<List<Faculty>>> GetFacultiesWithProgramsAsync()
    {
        var url = await PrepareUrlAsync(ProgramsUrl);
        var result = await apiService.TryRequestAsync<List<FacultyDto>>(client => client.GetAsync(url));

        return result.IsFailure
            ? Result.Failure<List<Faculty>>(result.Error)
            : FacultyMapper.ToFaculties(result.Value);
    }

    public async Task<Result<List<Subject>>> GetSubjectsAsync(StudyProgram program)
    {
        var template = program.Specialization is null ? SubjectsUrl : SpecificSubjectsUrl;
        var url = await PrepareUrlAsync(template);

        url = url.Replace("{programme_code}", program.Abbreviation);

        if (program.Specialization is not null)
        {
            url = url.Replace("{specialization_code}", program.Specialization);
        }

        var result = await apiService.TryRequestAsync<List<SubjectDto>>(client => client.GetAsync(url));

        return result.IsFailure
            ? Result.Failure<List<Subject>>(result.Error)
            : SubjectMapper.ToSubjects(result.Value);
    }
}