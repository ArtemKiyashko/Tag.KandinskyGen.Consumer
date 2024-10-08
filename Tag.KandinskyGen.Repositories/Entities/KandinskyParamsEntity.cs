using System.Text.Json.Serialization;

namespace Tag.KandinskyGen.Repositories.Entities;

public class KandinskyParamsEntity
{
    [JsonPropertyName("type")]
    public KandinskyRequestTypes Type { get; set; } = KandinskyRequestTypes.GENERATE;

    [JsonPropertyName("numImages")]
    public uint NumImages { get; set; } = 1;

    [JsonPropertyName("width")]
    public uint Width { get; set; }

    [JsonPropertyName("height")]
    public uint Height { get; set; }

    [JsonPropertyName("generateParams")]
   public required KandinskyGenerateParamsEntity GenerateParams { get; set; }
}
