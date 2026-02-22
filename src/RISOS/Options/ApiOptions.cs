namespace RISOS.Options;

public record ApiOptions()
{
    public const string Section = "Api";
    public string BaseUrl { get; set; } = string.Empty;
}