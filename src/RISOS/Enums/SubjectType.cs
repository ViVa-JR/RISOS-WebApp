using System.ComponentModel;

namespace RISOS.Enums;

public enum SubjectType
{
    Compulsory,
    [Description("Compulsory-Elective")]
    CompulsoryElective,
    Elective
}