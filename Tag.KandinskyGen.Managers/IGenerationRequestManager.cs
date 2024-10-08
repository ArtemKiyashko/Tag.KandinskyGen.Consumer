using Tag.KandinskyGen.Managers.Dtos;

namespace Tag.KandinskyGen.Managers;

public interface IGenerationRequestManager
{
    Task AddGenerationActivity(GenerationActivityRequestDto generationActivity);
}
