using System.Text.Json.Serialization;

namespace Tag.KandinskyGen.Repositories.Entities;

public class KandinskyGeneratioRequestResultEntity
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("uuid")]
    public required string Uuid { get; set; }
}
