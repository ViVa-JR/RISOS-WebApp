using RISOS.Pages.Home.Models;
using static MudBlazor.CategoryTypes;

namespace RISOS.Mappers
{
    public class SubjectStorageMapper
    {
        public static SubjectStorage ToStorage(SubjectEntry subjectEntry)
        {
            var result = new SubjectStorage();
            result.SubjectId=subjectEntry.Subject.Id;
            result.Completed=subjectEntry.Completed;
            result.semester=subjectEntry.Semester;
            return result;
        }

        public static ICollection<SubjectStorage> ToStorageList(List<SubjectEntry> subjectEntries)
        {
            if (subjectEntries == null)
            {
                return [];
            }

            return subjectEntries.Select(entry => ToStorage(entry)).ToList();
        }

        public static SubjectEntry ToSubjectEntry(SubjectStorage storage, SubjectEntry subjectEntry)
        {
            subjectEntry.Selected = true;
            subjectEntry.Completed = storage.Completed;
            subjectEntry.Semester = storage.semester;
            return subjectEntry;
        }


    }
}
