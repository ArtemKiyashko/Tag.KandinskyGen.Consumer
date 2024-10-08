using System.Text.Json.Serialization;

namespace Tag.KandinskyGen.Repositories.Entities;

public class KandinskyGenerateParamsEntity
{
    [JsonPropertyName("query")]
    public required string Query { get; set; }
}
