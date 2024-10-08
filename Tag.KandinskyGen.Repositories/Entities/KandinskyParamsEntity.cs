using System.Text.Json.Serialization;

namespace Tag.KandinskyGen.Repositories.Entities;

public class KandinskyParamsEntity
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "GENERATE";

    [JsonPropertyName("numImages")]
    public required uint NumImages { get; set; } = 1;

    [JsonPropertyName("width")]
    public required uint Width { get; set; }

    [JsonPropertyName("height")]
    public required uint Height { get; set; }

    [JsonPropertyName("generateParams")]
   public required KandinskyGenerateParamsEntity GenerateParams { get; set; }
}
