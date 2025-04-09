using Tag.KandinskyGen.Repositories.Entities;

namespace Tag.KandinskyGen.Repositories;

internal interface IKandinskyRepository
{
    Task<KandinskyGeneratioRequestResultEntity?> EnqueueGeneration(KandinskyGenerationRequestEntity entity);
    Task<IEnumerable<KandinskyPipelineEntity>?> GetPipelines();
    Task<bool> PipelineIsActive(Guid pipelineId);
    Task<IList<KandinskyStyleEntity>?> GetStyles();
}
