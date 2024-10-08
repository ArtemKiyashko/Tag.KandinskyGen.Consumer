namespace Tag.KandinskyGen.Repositories.Entities;

public class KandinskyModelEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public required float Version { get; set; }
    public required string Type { get; set; }
}
