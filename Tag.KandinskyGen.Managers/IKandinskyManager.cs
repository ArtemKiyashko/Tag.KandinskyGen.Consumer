using Tag.KandinskyGen.Managers.Dtos;

namespace Tag.KandinskyGen.Managers;

public interface IKandinskyManager
{
    Task<GenerationResponseDto> EnqueueGeneration(string prompt, long chatTgid);
}
