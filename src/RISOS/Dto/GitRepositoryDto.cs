using System.Text.Json.Serialization;

namespace RISOS.Dto;

public class GitRepositoryDto
{
    [JsonPropertyName("pushed_at")]
    public string PushedAt { get; set; } = string.Empty;
}