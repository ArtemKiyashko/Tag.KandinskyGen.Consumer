using System.Text.Json.Serialization;

namespace Tag.KandinskyGen.Repositories.Entities;

public class KandinskyGenerationRequestEntity
{
    [JsonPropertyName("model_id")]
    public int ModelId { get; set; }

    [JsonPropertyName("params")]
    public required KandinskyParamsEntity Params { get; set; }
}
