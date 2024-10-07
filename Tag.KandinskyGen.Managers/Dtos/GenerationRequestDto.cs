namespace Tag.KandinskyGen.Managers.Dtos;

public class GenerationRequestDto
{
    public required Guid RequestId { get; set; }
    public required GenerateRequestTypes RequestType { get; set; }
    public required long ChatTgId { get; set; }
    public required string ChatTitle { get; set; }
    public string? AlternativePrompt { get; set; }
    public required DateTimeOffset GenerationRequestedDateTime { get; set; }
}
