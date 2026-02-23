using System.Text.Json.Serialization;

namespace RISOS.Dto;

public class FacultyDto
{
    [JsonPropertyName("zkratka_fakulty")] 
    public string Code { get; set; } = string.Empty;
    
    [JsonPropertyName("fakulta")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("programy")] 
    public List<ProgramDto> Programs { get; set; } = [];
}