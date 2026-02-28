namespace RISOS.Models;

public record Credits(int Registered = 0, int Completed = 0)
{
    public static Credits operator +(Credits a, Credits b) => 
        new(a.Registered + b.Registered, a.Completed + b.Completed);
}