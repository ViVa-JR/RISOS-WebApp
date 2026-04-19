using System.Text.Json;
using Microsoft.JSInterop;
using RISOS.Enums;
using RISOS.Extensions;
using RISOS.Mappers;
using RISOS.Pages.Home.Models;

namespace RISOS.Services;

public class LocalStorageService(IJSRuntime js)
{
    private const string UserTheme = "user-theme";
    private const string UserLanguage = "user-language";

    private const string ProgramAbbreviation = "subject-id";
    private const string ProgramSpecialization = "subject-specialization";
    private const string Subjects = "user-subjects";
    private const string CustomSubjects = "custom-subjects";
    private const string CreditOverride = "credit-override";
    private const string StudyYears = "study-years";
    private const string RecognizedYear = "recognized-years";
    private const string ThemeTypeKey = "app_theme_type";

    public async Task SaveTheme(bool isDark) => await js.InvokeVoidAsync("localStorage.setItem", UserTheme, isDark ? "dark" : "light");

    public async Task SaveThemeType(ThemeType theme) => await js.InvokeVoidAsync("localStorage.setItem", ThemeTypeKey, theme.ToString());

    public async Task<ThemeType> LoadThemeType()
    {
        try
        {
            var value = await js.InvokeAsync<string>("localStorage.getItem", ThemeTypeKey);
            if (string.IsNullOrEmpty(value))
            {
                return ThemeType.Default;
            }

            return Enum.TryParse<ThemeType>(value, out var result) ? result : ThemeType.Default;
        }
        catch
        {
            return ThemeType.Default;
        }
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

    public async Task SaveProgramAbbreviation(string? programAbbreviation)
    {
        if (string.IsNullOrEmpty(programAbbreviation))
        {
            await js.InvokeVoidAsync("localStorage.removeItem", ProgramAbbreviation);
        }
        else
        {
            await js.InvokeVoidAsync("localStorage.setItem", ProgramAbbreviation, programAbbreviation);
        }
    }

    public async Task<string?> LoadProgramAbbreviation()
    {
        try
        {
            var id = await js.InvokeAsync<string>("localStorage.getItem", ProgramAbbreviation);
            return string.IsNullOrEmpty(id) ? null : id;
        }
        catch
        {
            await js.InvokeVoidAsync("localStorage.removeItem", ProgramAbbreviation);
            return null;
        }
    }

    public async Task SaveProgramSpecialization(string? programSpecializtion)
    {
        if (string.IsNullOrEmpty(programSpecializtion))
        {
            await js.InvokeVoidAsync("localStorage.removeItem", ProgramSpecialization);
        }
        else
        {
            await js.InvokeVoidAsync("localStorage.setItem", ProgramSpecialization, programSpecializtion);
        }
    }

    public async Task<string?> LoadProgramSpecialization()
    {
        try
        {
            var id = await js.InvokeAsync<string>("localStorage.getItem", ProgramSpecialization);
            return string.IsNullOrEmpty(id) ? null : id;
        }
        catch
        {
            await js.InvokeVoidAsync("localStorage.removeItem", ProgramSpecialization);
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
            if (int.TryParse(value, out var result))
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
            if (int.TryParse(value, out var result))
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

    public async Task SaveRecognizedYear(bool recognizedYear) => await js.InvokeVoidAsync("localStorage.setItem", RecognizedYear, recognizedYear.ToString());

    public async Task<bool> LoadRecognizedYear()
    {
        try
        {
            var value = await js.InvokeAsync<string>("localStorage.getItem", RecognizedYear);
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return bool.TryParse(value, out var result) && result;
        }
        catch
        {
            await js.InvokeVoidAsync("localStorage.removeItem", RecognizedYear);
            return false;
        }
    }

    public async Task SaveSubjects(ICollection<SubjectStorage> subjects)
    {
        var json = JsonSerializer.Serialize(subjects);
        await js.InvokeVoidAsync("localStorage.setItem", Subjects, json);
    }

    public async Task<List<SubjectStorage>> LoadSubjectsAsync()
    {
        var json = await js.InvokeAsync<string>("localStorage.getItem", Subjects);
        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        try
        {
            var subjects = JsonSerializer.Deserialize<List<SubjectStorage>>(json);
            return subjects ?? [];
        }
        catch (Exception)
        {
            return [];
        }
    }

    public async Task SaveCustomSubjects(List<SubjectEntry> customSubjects)
    {
        var json = JsonSerializer.Serialize(customSubjects);
        await js.InvokeVoidAsync("localStorage.setItem", CustomSubjects, json);
    }

    private async Task SaveCustomSubjectAsync(SubjectEntry customSubject)
    {
        if (!customSubject.IsCustomSubject)
        {
            return;
        }

        var customSubjects = await LoadCustomSubjectsAsync();

        var old = customSubjects.FirstOrDefault(x => x.Subject.Id == customSubject.Subject.Id && x.Attempt == customSubject.Attempt);
        if (old is not null)
        {
            customSubjects.Remove(old);
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
            return [];
        }

        try
        {
            var subjects = JsonSerializer.Deserialize<List<SubjectEntry>>(json);
            return subjects ?? [];
        }
        catch (Exception)
        {
            return [];
        }
    }

    public async Task ResetSubjectsAsync()
    {
        var emptyList = new List<SubjectStorage>();
        await SaveSubjects(emptyList);
    }

    public async Task UpdateSubjectAsync(SubjectEntry updatedSubjectEntry)
    {
        if (updatedSubjectEntry.IsCustomSubject)
        {
            await SaveCustomSubjectAsync(updatedSubjectEntry);
            return;
        }

        await SaveSubjectAsync(updatedSubjectEntry);
    }

    private async Task SaveSubjectAsync(SubjectEntry updatedSubjectEntry)
    {
        if (updatedSubjectEntry.IsCustomSubject)
        {
            return;
        }

        var updatedSubject = SubjectStorageMapper.ToStorage(updatedSubjectEntry);

        var subjects = await LoadSubjectsAsync();
        var index = subjects.FindIndex(s => s.SubjectId == updatedSubject.SubjectId && s.Attempt == updatedSubject.Attempt);
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

    public async Task RemoveSubjectAsync(SubjectEntryKey subjectEntryKey)
    {
        var subjects = await LoadSubjectsAsync();
        if (subjectEntryKey.Attempt == 1)
        {
            await RemoveAllOfSubjectAsync(subjectEntryKey.Id);
            return;
        }

        if (subjects.RemoveAll(s => s.SubjectId == subjectEntryKey.Id && s.Attempt == subjectEntryKey.Attempt) > 0)
        {
            await SaveSubjects(subjects);
            return;
        }

        var customSubjects = await LoadCustomSubjectsAsync();
        if (customSubjects.RemoveAll(s => s.Subject.Id == subjectEntryKey.Id && s.Attempt == subjectEntryKey.Attempt) > 0)
        {
            await SaveCustomSubjects(customSubjects);
        }
    }

    private async Task RemoveAllOfSubjectAsync(string id)
    {
        var subjects = await LoadSubjectsAsync();
        if (subjects.RemoveAll(s => s.SubjectId == id) > 0)
        {
            await SaveSubjects(subjects);
            return;
        }

        var customSubjects = await LoadCustomSubjectsAsync();
        if (customSubjects.RemoveAll(s => s.Subject.Id == id) > 0)
        {
            await SaveCustomSubjects(customSubjects);
        }
    }

    public async Task SaveLanguage(string language) => await js.InvokeVoidAsync("localStorage.setItem", UserLanguage, language);

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
        var state = new AppState
        {
            ProgramAbbreviation = await LoadProgramAbbreviation(),
            ProgramSpecialization = await LoadProgramSpecialization(),
            Subjects = await LoadSubjectsAsync(),
            CustomSubjects = await LoadCustomSubjectsAsync(),
            CreditOverride = await LoadCreditOverride(),
            StudyYears = await LoadStudyYears(),
            RecognizedYear = await LoadRecognizedYear()
        };
        return state;
    }

    public async Task ClearAppStateAsync()
    {
        await js.InvokeVoidAsync("localStorage.removeItem", ProgramAbbreviation);
        await js.InvokeVoidAsync("localStorage.removeItem", ProgramSpecialization);
        await js.InvokeVoidAsync("localStorage.removeItem", Subjects);
        await js.InvokeVoidAsync("localStorage.removeItem", CustomSubjects);
        await js.InvokeVoidAsync("localStorage.removeItem", CreditOverride);
        await js.InvokeVoidAsync("localStorage.removeItem", StudyYears);
        await js.InvokeVoidAsync("localStorage.removeItem", RecognizedYear);
    }
}