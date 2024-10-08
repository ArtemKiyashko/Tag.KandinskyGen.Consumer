namespace Tag.KandinskyGen.Managers.Dtos;

public class GenerationResponseDto
{
    public required long ChatTgId { get; set; }
    public required string Prompt { get; set; }
    public required string Uuid { get; set; }
}
