using System.Text.Json.Serialization;

namespace RISOS.Dto;

public class GitRepositoryDto
{
    [JsonPropertyName("workflow_runs")]
    public List<WorkflowRunDto> WorkflowList { get; set; } = [];
}

public class WorkflowRunDto
{
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
