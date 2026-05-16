using RISOS.Pages.Home.Models;

namespace RISOS.Mappers;

public static class SubjectStorageMapper
{
    public static SubjectStorageEntry ToStorage(SubjectEntry subjectEntry)
    {
        return new SubjectStorageEntry
        {
            SubjectId = subjectEntry.Subject.Id,
            Completed = subjectEntry.Completed,
            Grade = subjectEntry.Grade,
            Semester = subjectEntry.Semester,
            Attempt = subjectEntry.Attempt,
            LatestAttempt = subjectEntry.LatestAttempt,
            IndexInZone = subjectEntry.IndexInZone
        };
    }

    public static ICollection<SubjectStorageEntry> ToStorageList(List<SubjectEntry> subjectEntries)
        => subjectEntries.Select(ToStorage).ToList();


    public static SubjectEntry AplyStorageData(SubjectStorageEntry storage, SubjectEntry subjectEntry)
    {
        if (storage.Attempt != 1)
        {
            return new SubjectEntry(subjectEntry.Subject)
            {
                Completed = storage.Completed,
                Grade = storage.Grade,
                Semester = storage.Semester,
                Attempt = storage.Attempt,
                LatestAttempt = storage.LatestAttempt,
                IndexInZone = storage.IndexInZone,
                Selected = true
            };
        }

        subjectEntry.Selected = true;
        subjectEntry.Completed = storage.Completed;
        subjectEntry.Grade = storage.Grade;
        subjectEntry.Semester = storage.Semester;
        subjectEntry.Attempt = storage.Attempt;
        subjectEntry.LatestAttempt = storage.LatestAttempt;
        subjectEntry.IndexInZone = storage.IndexInZone;
        return subjectEntry;
    }
}
