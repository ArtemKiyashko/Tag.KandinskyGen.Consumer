namespace Tag.KandinskyGen.Managers;

public class KandinskyOptions
{
    public uint PictureWidth { get; set; } = 1024;
    public uint PictureHeight { get; set; } = 1024;
    public string? PictureStyle { get; set; }
    public Uri? BaseAddress { get; set; } = new Uri("https://api-key.fusionbrain.ai");
    public string? XKey { get; set; }
    public string? XSecret { get; set; }
}
