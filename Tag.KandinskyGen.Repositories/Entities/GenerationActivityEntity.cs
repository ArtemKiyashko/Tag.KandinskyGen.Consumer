using Tag.KandinskyGen.Repositories.Enums;

namespace Tag.KandinskyGen.Repositories.Entities;

public class GenerationActivityEntity : BaseEntity
{
    public required long ChatTgId { get; set; }
    public GenerationStatuses GenerationStatus { get; set; }
    public DateTimeOffset StartedDateTime { get; set; }
    public DateTimeOffset FinishedDateTime { get; set; }
    public string? ResultContainer { get; set; }
    public string? ResultPath { get; set; }
}
