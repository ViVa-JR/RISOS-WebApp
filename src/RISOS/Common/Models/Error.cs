namespace RISOS.Common.Models;

public record Error(string Message, Exception? Exception = null)
{
    public static Error None => new("None");
    public static Error NullValue => new("Value is null");
    public static Error ParseError => new("Failed to parse response");
}
