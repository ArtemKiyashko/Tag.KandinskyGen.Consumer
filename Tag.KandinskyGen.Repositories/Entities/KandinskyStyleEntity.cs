using System.Text.Json.Serialization;

namespace Tag.KandinskyGen.Repositories.Entities;

public class KandinskyStyleEntity
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("titleEn")]
    public string? TitleEn { get; set; }
    [JsonPropertyName("image")]
    public string? Image { get; set; }
}
