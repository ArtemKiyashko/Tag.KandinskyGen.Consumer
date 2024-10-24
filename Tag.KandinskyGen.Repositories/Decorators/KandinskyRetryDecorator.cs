using Polly;
using Tag.KandinskyGen.Repositories.Entities;

namespace Tag.KandinskyGen.Repositories.Decorators;

internal class KandinskyRetryDecorator(IKandinskyRepository repository) : IKandinskyRepository
{
    private readonly IKandinskyRepository _repository = repository;

    public Task<KandinskyGeneratioRequestResultEntity?> EnqueueGeneration(KandinskyGenerationRequestEntity entity)
        => Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(async () => await _repository.EnqueueGeneration(entity));

    public Task<IEnumerable<KandinskyModelEntity>?> GetModels() 
        => Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(_repository.GetModels);

    public Task<bool> ModelIsActive(int modelId)
        => Policy
            .HandleResult(false)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(async () => await _repository.ModelIsActive(modelId));
}
