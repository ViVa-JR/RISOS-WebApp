using System.Text.Json.Serialization;

namespace RISOS.Dto;

public class SubjectDto
{
    [JsonPropertyName("zkratka")]
    public string Code { get; set; } = string.Empty;
    
    [JsonPropertyName("nazev")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("kredity")]
    public string Credits { get; set; } = string.Empty;
    
    [JsonPropertyName("povinnost")]
    public string Obligation { get; set; } = string.Empty;
    
    [JsonPropertyName("zakonceni")]
    public string CompletionType { get; set; } = string.Empty;
    
    [JsonPropertyName("skupina")]
    public string? GroupId { get; set; } = null;
    
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
    
    [JsonPropertyName("semestr")]
    public List<string> Semesters { get; set; } = [];
    
    [JsonPropertyName("rocnik")]
    public string Year { get; set; } = string.Empty;
}