using Polly;
using Tag.KandinskyGen.Repositories.Entities;

namespace Tag.KandinskyGen.Repositories.Decorators;

internal class KandinskyRetryDecorator(IKandinskyRepository repository) : IKandinskyRepository
{
    private readonly IKandinskyRepository _repository = repository;

    public Task<KandinskyGeneratioRequestResultEntity?> EnqueueGeneration(KandinskyGenerationRequestEntity entity)
        => Policy
            .Handle<HttpRequestException>()
            .Or<InvalidOperationException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(async () => await _repository.EnqueueGeneration(entity));

    public Task<IEnumerable<KandinskyPipelineEntity>?> GetPipelines() 
        => Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(_repository.GetPipelines);

    public Task<IList<KandinskyStyleEntity>?> GetStyles()
        => Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(_repository.GetStyles);

    public Task<bool> PipelineIsActive(Guid pipelineId)
        => Policy
            .HandleResult(false)
            .Or<HttpRequestException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(async () => await _repository.PipelineIsActive(pipelineId));
}
