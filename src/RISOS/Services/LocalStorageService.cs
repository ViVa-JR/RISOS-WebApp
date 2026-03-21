using Microsoft.JSInterop;
using RISOS.Enums;
using RISOS.Extensions;
using RISOS.Mappers;
using RISOS.Pages.Home.Models;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace RISOS.Services;

public class LocalStorageService(IJSRuntime js)
{
    private const string UserTheme = "user-theme";
    private const string UserLanguage = "user-language";
    private const string programAbbreviation = "subject-id";
    private const string programSpecialization = "subject-specialization";

    private const string Subjects = "user-subjects";
    private const string CustomSubjects = "custom-subjects";
    private const string CreditOverride= "credit-override";
    private const string StudyYears = "study-years";

    public async Task SaveTheme(bool isDark)
    {
        await js.InvokeVoidAsync("localStorage.setItem", UserTheme, isDark ? "dark" : "light");
    }

    public async Task<bool> LoadTheme()
    {
        try
        {
            return await js.InvokeAsync<string>("localStorage.getItem", UserTheme) == "dark";
        }
        catch
        {
            await js.InvokeVoidAsync("localStorage.removeItem", UserTheme);
            return true;
        }
    }

    public async Task SaveProgramAbbreviation(string? ProgramAbbreviation)
    {
        if (string.IsNullOrEmpty(ProgramAbbreviation))
        {
            await js.InvokeVoidAsync("localStorage.removeItem", programAbbreviation);
        }
        else
        {
            await js.InvokeVoidAsync("localStorage.setItem", programAbbreviation, ProgramAbbreviation);
        }
    }

    public async Task<string?> LoadProgramAbbreviation()
    {
        try
        {
            var id = await js.InvokeAsync<string>("localStorage.getItem", programAbbreviation);
            return string.IsNullOrEmpty(id) ? null : id;
        }
        catch
        {
            await js.InvokeVoidAsync("localStorage.removeItem", programAbbreviation);
            return null;
        }
    }

    public async Task SaveProgramSpecialization(string? ProgramSpecialization)
    {
        if (string.IsNullOrEmpty(ProgramSpecialization))
        {
            await js.InvokeVoidAsync("localStorage.removeItem", programSpecialization);
        }
        else
        {
            await js.InvokeVoidAsync("localStorage.setItem", programSpecialization, ProgramSpecialization);
        }
    }

    public async Task<string?> LoadProgramSpecialization()
    {
        try
        {
            var id = await js.InvokeAsync<string>("localStorage.getItem", programSpecialization);
            return string.IsNullOrEmpty(id) ? null : id;
        }
        catch
        {
            await js.InvokeVoidAsync("localStorage.removeItem", programSpecialization);
            return null;
        }
    }

    public async Task SaveCreditOverride(int? creditOverride)
    {
        if (creditOverride == null)
        {
            await js.InvokeVoidAsync("localStorage.removeItem", CreditOverride);
        }
        else
        {
            await js.InvokeVoidAsync("localStorage.setItem", CreditOverride, creditOverride.ToString());
        }
    }

    public async Task<int?> LoadCreditOverride()
    {
        try
        {
            var value = await js.InvokeAsync<string>("localStorage.getItem", CreditOverride);
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            return null;
        }
        catch
        {
            await js.InvokeVoidAsync("localStorage.removeItem", CreditOverride);
            return null;
        }
    }

    public async Task SaveStudyYears(int? studyYears)
    {
        if (studyYears == null)
        {
            await js.InvokeVoidAsync("localStorage.removeItem", StudyYears);
        }
        else
        {
            await js.InvokeVoidAsync("localStorage.setItem", StudyYears, studyYears.ToString());
        }
    }

    public async Task<int?> LoadStudyYears()
    {
        try
        {
            var value = await js.InvokeAsync<string>("localStorage.getItem", StudyYears);
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            return null;
        }
        catch
        {
            await js.InvokeVoidAsync("localStorage.removeItem", StudyYears);
            return null;
        }
    }

    public async Task SaveSubjects(ICollection<SubjectStorage> subjects)
    {
        if (subjects == null) return;

        var json = JsonSerializer.Serialize(subjects);
        await js.InvokeVoidAsync("localStorage.setItem", Subjects, json);
    }

    public async Task<List<SubjectStorage>> LoadSubjectsAsync()
    {
        var json = await js.InvokeAsync<string>("localStorage.getItem", Subjects);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<SubjectStorage>();
        }

        try
        {
            var subjects = JsonSerializer.Deserialize<List<SubjectStorage>>(json);
            return subjects ?? new List<SubjectStorage>();
        }
        catch (Exception)
        {
            return new List<SubjectStorage>();
        }
    }

    public async Task SaveCustomSubjects(List<SubjectEntry> customSubjects)
    {
        if (customSubjects == null) return;

        var json = JsonSerializer.Serialize(customSubjects);
        await js.InvokeVoidAsync("localStorage.setItem", CustomSubjects, json);
    }

    public async Task SaveCustomSubject(SubjectEntry customSubject)
    {
        if (customSubject == null) return;
        var customSubjects= await LoadCustomSubjectsAsync();
        if(customSubjects == null)
        {
            customSubjects = new List<SubjectEntry>();

        }
        
         customSubjects.Add(customSubject);
        var json = JsonSerializer.Serialize(customSubjects);
        await js.InvokeVoidAsync("localStorage.setItem", CustomSubjects, json);
    }

    public async Task<List<SubjectEntry>> LoadCustomSubjectsAsync()
    {
        var json = await js.InvokeAsync<string>("localStorage.getItem", CustomSubjects);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<SubjectEntry>();
        }

        try
        {
            var subjects = JsonSerializer.Deserialize<List<SubjectEntry>>(json);
            return subjects ?? new List<SubjectEntry>();
        }
        catch (Exception)
        {
            return new List<SubjectEntry>();
        }
    }

    public async Task ResetSubjectsAsync()
    {
        var emptyList = new List<SubjectStorage>();
        await SaveSubjects(emptyList);
    }

    public async Task UpdateSubjectAsync(SubjectEntry updatedSubjectEntry)
    {
        var updatedSubject = SubjectStorageMapper.ToStorage(updatedSubjectEntry);
        if (updatedSubject == null) return;

        var subjects = await LoadSubjectsAsync();
        var index = subjects.FindIndex(s => s.SubjectId == updatedSubject.SubjectId);

        if (index != -1)
        {
            subjects[index] = updatedSubject;
        }
        else
        {
            subjects.Add(updatedSubject);
        }
        await SaveSubjects(subjects);
    }

    public async Task RemoveSubjectAsync(string deletedSubjectId)
    {

        var subjects = await LoadSubjectsAsync();
        subjects.RemoveAll(s => s.SubjectId == deletedSubjectId);

        await SaveSubjects(subjects);
    }


    public async Task SaveLanguage(string language)
    {
        await js.InvokeVoidAsync("localStorage.setItem", UserLanguage, language);
    }

    public async Task<string> LoadLanguage()
    {
        try
        {
            var language = await js.InvokeAsync<string>("localStorage.getItem", UserLanguage);
            return string.IsNullOrEmpty(language) ? AppLanguage.English.ToCultureString() : language;
        }
        catch
        {
            await js.InvokeVoidAsync("localStorage.removeItem", UserLanguage);
            return AppLanguage.English.ToCultureString();
        }
    }

    public async Task<AppState> GetExportStateAsync()
    {
        var state = new AppState();
        state.ProgramAbbreviation = await LoadProgramAbbreviation();
        state.ProgramSpecialization = await LoadProgramSpecialization();
        state.Subjects = await LoadSubjectsAsync();
        state.CustomSubjects = await LoadCustomSubjectsAsync();
        state.CreditOverride = await LoadCreditOverride();
        state.StudyYears = await LoadStudyYears();
        return state;
    }
}