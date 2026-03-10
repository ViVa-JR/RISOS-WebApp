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
    
    [JsonPropertyName("doba_studia")]
    public string StudyDuration { get; set; } = string.Empty;
    
    [JsonPropertyName("typ_studia")]
    public string StudyType { get; set; } = string.Empty;
    
    [JsonPropertyName("kredity")]
    public string Credits { get; set; } = string.Empty;
    
}
