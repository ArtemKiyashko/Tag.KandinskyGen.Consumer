using System.Text.Json.Serialization;

namespace Tag.KandinskyGen.Repositories.Entities;

public class KandinskyGenerationRequestEntity
{
    [JsonPropertyName("pipeline_id")]
    public required Guid PipelineId { get; set; }

    [JsonPropertyName("params")]
    public required KandinskyParamsEntity Params { get; set; }
}
