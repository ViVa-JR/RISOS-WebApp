namespace RISOS.Common.Models;

public record Credits(
    int Registered = 0,
    int Completed = 0,
    bool SportLimitReached = false,
    bool HasDuplicateSports = false)
{
    public static Credits operator +(Credits a, Credits b) =>
        new(
            a.Registered + b.Registered,
            a.Completed + b.Completed,
            a.SportLimitReached || b.SportLimitReached,
            a.HasDuplicateSports || b.HasDuplicateSports
        );
}
