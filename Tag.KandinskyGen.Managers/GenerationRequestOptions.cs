namespace Tag.KandinskyGen.Managers;

public class GenerationRequestOptions
{
    public Uri? TablesServiceUri { get; set; }
    public string? TablesConnectionString { get; set; }
    public string GenerationActivityTable { get; set; } = "taggenerationactivities";
}
