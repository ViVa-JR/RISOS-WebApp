using RISOS.Enums;

namespace RISOS.Extensions;

public static class CompletionTypeExtensions
{
    public static string GetDescription(this CompletionType typ) => typ switch
    {
        CompletionType.None => "No final classification",
        CompletionType.Cr => "Course-unit credit",
        CompletionType.GCr => "Graded course-unit credit",
        CompletionType.CrEx => "Course-unit credit and examination",
        CompletionType.Ex => "Examination",
        CompletionType.Col => "Colloquium",
        CompletionType.Kp => "Final semester project",
        CompletionType.Rec => "Recognized",
        CompletionType.Szz => "Final state examination",
        CompletionType.RCr => "Recognized course-unit credit",
        CompletionType.RgCr => "Recognized graded course-unit credit",
        CompletionType.RCrEx => "Recognized course-unit credit and examination",
        CompletionType.REx => "Recognized examination",
        CompletionType.Hs => "Evaluation of study",
        CompletionType.Hds => "Evaluation of doctoral study",
        CompletionType.UpZa => "Recognized course-unit credit only",
        CompletionType.RVol => "Recognized year of study",
        CompletionType.CrFsp => "Course-unit credit and final semester project",
        CompletionType.DrEx => "Examination",
        CompletionType.RecCr => "Recognized examination",
        CompletionType.ExDd => "Except DD",
        CompletionType.SpEx => "Specialization examination",
        CompletionType.SpCrEx => "Specialization credit and examination",
        CompletionType.RCol => "Recognized colloquium",
        _ => "unknown"
    };
}