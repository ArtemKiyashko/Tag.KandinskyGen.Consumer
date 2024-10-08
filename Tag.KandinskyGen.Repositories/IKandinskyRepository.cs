using Tag.KandinskyGen.Repositories.Entities;

namespace Tag.KandinskyGen.Repositories;

internal interface IKandinskyRepository
{
    Task<KandinskyGeneratioRequestResultEntity?> EnqueueGeneration(KandinskyGenerationRequestEntity entity);
    Task<IEnumerable<KandinskyModelEntity>?> GetModels();
    Task<bool> ModelIsActive(int modelId);
}
