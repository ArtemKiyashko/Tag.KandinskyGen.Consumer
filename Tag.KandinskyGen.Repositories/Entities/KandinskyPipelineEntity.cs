using System;

namespace Tag.KandinskyGen.Repositories.Entities;

public class KandinskyPipelineEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? NameEn { get; set; }
    public string? Description { get; set; }
    public string? DescriptionEn { get; set; }
    public string[] Tags { get; set; } = [];
    public required float Version { get; set; }
    public required string Status { get; set; }
    public required string Type { get; set; }
    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? LastModified { get; set; }
}
