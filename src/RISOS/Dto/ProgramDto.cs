using System.Text.Json.Serialization;

namespace RISOS.Dto;

public class ProgramDto
{
    [JsonPropertyName("zkratka_programu")]
    public string Code { get; set; } = string.Empty;
    
    [JsonPropertyName("nazev_programu")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("specializace")] 
    public string? Specialization { get; set; } = null;
    
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}