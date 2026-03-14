namespace RISOS.Pages.Home.Models
{
    public class SubjectStorage
    {
        public string SubjectId { get; set; } = "";
        public bool? Completed { get; set; }
        public string semester { get; set; } = SubjectEntry.Unassigned;
    }
}
