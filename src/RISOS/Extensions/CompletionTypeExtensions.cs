using RISOS.Enums;

namespace RISOS.Extensions;

public static class CompletionTypeExtensions
{
    public static string GetDescription(this CompletionType typ) => typ switch
    {
        CompletionType.None => "no final classification",
        CompletionType.Cr => "course-unit credit",
        CompletionType.GCr => "graded course-unit credit",
        CompletionType.CrEx => "course-unit credit and examination",
        CompletionType.Ex => "examination",
        CompletionType.Col => "colloquium",
        CompletionType.Kp => "final semester project",
        CompletionType.Rec => "recognized",
        CompletionType.Szz => "final state examination",
        CompletionType.RCr => "recognized course-unit credit",
        CompletionType.RgCr => "recognized graded course-unit credit",
        CompletionType.RCrEx => "recognized course-unit credit and examination",
        CompletionType.REx => "recognized examination",
        CompletionType.Hs => "evaluation of study",
        CompletionType.Hds => "evaluation of doctoral study",
        CompletionType.UpZa => "recognized course-unit credit only",
        CompletionType.RVol => "recognized year of study",
        CompletionType.CrFsp => "course-unit credit and final semester project",
        CompletionType.DrEx => "examination",
        CompletionType.RecCr => "recognized examination",
        CompletionType.ExDd => "except DD",
        CompletionType.SpEx => "specialization examination",
        CompletionType.SpCrEx => "specialization credit and examination",
        CompletionType.RCol => "recognized colloquium",
        _ => "unknown"
    };
}