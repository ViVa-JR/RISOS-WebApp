using RISOS.Pages.Home.Models;

namespace RISOS.Mappers
{
    public class SubjectStorageMapper
    {
        public static SubjectStorage ToStorage(SubjectEntry subjectEntry)
        {
            var result = new SubjectStorage
            {
                SubjectId = subjectEntry.Subject.Id,
                Completed = subjectEntry.Completed,
                Semester = subjectEntry.Semester,
                Attempt = subjectEntry.Attempt,
                LatestAttempt = subjectEntry.LatestAttempt,
                IndexInZone = subjectEntry.IndexInZone,
            };
            return result;
        }

        public static ICollection<SubjectStorage> ToStorageList(List<SubjectEntry> subjectEntries)
            => subjectEntries.Select(ToStorage).ToList();


        public static SubjectEntry ToSubjectEntry(SubjectStorage storage, SubjectEntry subjectEntry)
        {
            if (storage.Attempt != 1)
            {
                return new SubjectEntry(subjectEntry.Subject)
                {
                    Completed = storage.Completed,
                    Semester = storage.Semester,
                    Attempt = storage.Attempt,
                    LatestAttempt = storage.LatestAttempt,
                    IndexInZone = storage.IndexInZone,
                    Selected = true
                };
            }

            subjectEntry.Selected = true;
            subjectEntry.Completed = storage.Completed;
            subjectEntry.Semester = storage.Semester;
            subjectEntry.Attempt = storage.Attempt;
            subjectEntry.LatestAttempt = storage.LatestAttempt;
            subjectEntry.IndexInZone = storage.IndexInZone;
            return subjectEntry;
        }
    }
}