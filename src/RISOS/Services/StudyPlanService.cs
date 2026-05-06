using RISOS.Pages.Home.Models;

namespace RISOS.Services;

public class StudyPlanService(LocalStorageService localStorageService)
{
    private List<SubjectEntry> _subjects = new();

    public IReadOnlyList<SubjectEntry> Subjects => _subjects.AsReadOnly();

    public event Action? OnPlanChanged;

    public void SetSubjects(IEnumerable<SubjectEntry> subjects)
    {
        _subjects = subjects.ToList();
        NotifyChanged();
    }

    public async Task AddSubjectAsync(SubjectEntry entry)
    {
        entry.IndexInZone = _subjects.Count(x => x.Semester == entry.Semester);
        _subjects.Add(entry);
        await localStorageService.UpdateSubjectAsync(entry);
        NotifyChanged();
    }

    public async Task RemoveSubjectAsync(SubjectEntry entry)
    {
        var removeAll = false;
        if (entry.IsSport)
        {
            _subjects.Remove(entry);
            if (entry.LatestAttempt)
            {
                var newLatest = _subjects.Where(x => x.Subject.Id == entry.Subject.Id).MaxBy(x => x.Attempt);
                if (newLatest != null)
                {
                    newLatest.LatestAttempt = true;
                    await localStorageService.UpdateSubjectAsync(newLatest);
                }
            }
        }
        else if (entry.Attempt != 1)
        {
            _subjects.Remove(entry);
            var prevAttempt = _subjects.FirstOrDefault(x => x.Subject.Id == entry.Subject.Id && x.Attempt == entry.Attempt - 1);
            if (prevAttempt != null)
            {
                prevAttempt.LatestAttempt = true;
                await localStorageService.UpdateSubjectAsync(prevAttempt);
            }
        }
        else
        {
            _subjects.RemoveAll(x => x.Subject.Id == entry.Subject.Id);
            removeAll = true;
        }

        await localStorageService.RemoveSubjectAsync(entry.Key, removeAll);
        await FixZoneIndexesAsync(entry.Semester);
        NotifyChanged();
    }

    public async Task DuplicateSportAsync(SubjectEntry entry)
    {
        var existingSports = _subjects.Where(x => x.Subject.Id == entry.Subject.Id).ToList();
        var maxAttempt = existingSports.Any() ? existingSports.Max(x => x.Attempt) : entry.Attempt;

        foreach (var s in existingSports.Where(x => x.LatestAttempt))
        {
            s.LatestAttempt = false;
            await localStorageService.UpdateSubjectAsync(s);
        }

        var newEntry = new SubjectEntry(entry.Subject)
        {
            Semester = SubjectEntry.Unassigned,
            Attempt = maxAttempt + 1,
            Selected = true,
            LatestAttempt = true,
            Completed = null
        };

        await AddSubjectAsync(newEntry);
    }

    public async Task RetakeSubjectAsync(SubjectEntry entry, int newSemester)
    {
        var latestEntry = _subjects.FirstOrDefault(x => x.Subject.Id == entry.Subject.Id && x.LatestAttempt);
        var latestAttempt = latestEntry?.Attempt ?? 0;

        if (latestAttempt > entry.Attempt)
        {
            return;
        }

        var newEntry = new SubjectEntry(entry.Subject)
        {
            Semester = newSemester.ToString(),
            IsCustomSubject = entry.IsCustomSubject,
            Completed = null,
            Attempt = latestAttempt + 1
        };

        if (latestEntry != null)
        {
            latestEntry.LatestAttempt = false;
            await localStorageService.UpdateSubjectAsync(latestEntry);
        }
        else
        {
            entry.LatestAttempt = false;
            await localStorageService.UpdateSubjectAsync(entry);
        }

        _subjects.Add(newEntry);
        await localStorageService.UpdateSubjectAsync(newEntry);
        NotifyChanged();
    }

    public async Task UpdateSubjectAsync(SubjectEntry entry)
    {
        await localStorageService.UpdateSubjectAsync(entry);
        NotifyChanged();
    }

    public async Task FixZoneIndexesAsync(string semester)
    {
        var items = _subjects.Where(x => x.Semester == semester).OrderBy(x => x.IndexInZone).ToList();
        for (var i = 0; i < items.Count; i++)
        {
            if (items[i].IndexInZone == i)
            {
                continue;
            }

            items[i].IndexInZone = i;
            await localStorageService.UpdateSubjectAsync(items[i]);
        }
    }

    private void NotifyChanged() => OnPlanChanged?.Invoke();
}
