using Tag.KandinskyGen.Managers.Dtos;

namespace Tag.KandinskyGen.Consumer.ViewModels;

public class GenerationRequestViewModel
{
    public required Guid RequestId { get; set; }
    public required GenerateRequestTypes RequestType { get; set; }
    public required long ChatTgId { get; set; }
    public required string ChatTitle { get; set; }
    public string? AlternativePrompt { get; set; }
    public required DateTimeOffset GenerationRequestedDateTime { get; set; }
}
