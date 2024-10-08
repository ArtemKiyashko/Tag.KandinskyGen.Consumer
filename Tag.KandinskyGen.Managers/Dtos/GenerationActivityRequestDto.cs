namespace Tag.KandinskyGen.Managers.Dtos;

public class GenerationActivityRequestDto
{
    public required Guid RequestId { get; set; }
    public required GenerateRequestTypes RequestType { get; set; }
    public required long ChatTgId { get; set; }
    public required DateTimeOffset GenerationRequestedDateTime { get; set; }
    public required string Uuid { get; set; }
    public required string Prompt { get; set; }
}
