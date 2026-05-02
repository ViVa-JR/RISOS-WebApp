using Microsoft.JSInterop;
using RISOS.Enums;
using RISOS.Pages.Home.Models;

namespace RISOS.Services;

public class AnalyticsService(IJSRuntime jsRuntime)
{
    public ValueTask TrackEventAsync(string eventName, Dictionary<string, object?>? parameters = null)
    {
        return jsRuntime.InvokeVoidAsync("risosAnalytics.trackEvent", eventName, parameters ?? new Dictionary<string, object?>());
    }

    public ValueTask TrackStudyProgramSelectedAsync(StudyProgram? program)
    {
        return TrackEventAsync("study_program_select", new Dictionary<string, object?>
        {
            ["program_abbreviation"] = program?.Abbreviation ?? "none",
            ["program_title"] = program?.Title ?? "none",
            ["specialization"] = program?.Specialization ?? "none",
            ["study_duration_years"] = program?.StudyDuration,
            ["required_credits"] = program?.Credits
        });
    }

    public ValueTask TrackAddButtonClickAsync()
    {
        return TrackEventAsync("add_button_click", new Dictionary<string, object?>
        {
            ["location"] = "unassigned_subjects",
            ["button_name"] = "add"
        });
    }

    public ValueTask TrackThemeChangedAsync(ThemeType oldTheme, ThemeType newTheme)
    {
        return TrackEventAsync("theme_change", new Dictionary<string, object?>
        {
            ["theme_from"] = oldTheme.ToString(),
            ["theme_to"] = newTheme.ToString()
        });
    }

    public ValueTask TrackLanguageChangedAsync(AppLanguage oldLanguage, AppLanguage newLanguage)
    {
        return TrackEventAsync("language_change", new Dictionary<string, object?>
        {
            ["language_from"] = oldLanguage.ToString(),
            ["language_to"] = newLanguage.ToString()
        });
    }

    public ValueTask TrackSubjectClickAsync(SubjectEntry subjectEntry)
    {
        var subjectType = subjectEntry.Subject.Type.ToString().ToLowerInvariant();

        return TrackEventAsync("subject_click", new Dictionary<string, object?>
        {
            ["subject_type"] = subjectType,
            ["subject_code"] = subjectEntry.Subject.ShortName,
            ["subject_name"] = subjectEntry.Subject.Name,
            ["action"] = subjectEntry.Selected ? "select" : "deselect",
            ["is_custom_subject"] = subjectEntry.IsCustomSubject,
            ["sport_name"] = subjectEntry.IsSport ? subjectEntry.Subject.Name : null
        });
    }

    public ValueTask TrackStudyYearsChangedAsync(string action, int previousYears, int newYears)
    {
        return TrackEventAsync("study_year_change", new Dictionary<string, object?>
        {
            ["action"] = action,
            ["years_from"] = previousYears,
            ["years_to"] = newYears
        });
    }

    public ValueTask TrackRecognizedYearToggleAsync(bool isEnabled)
    {
        return TrackEventAsync("recognized_year_toggle", new Dictionary<string, object?>
        {
            ["is_enabled"] = isEnabled
        });
    }
}

