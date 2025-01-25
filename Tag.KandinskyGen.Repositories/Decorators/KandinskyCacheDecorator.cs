
using LazyCache;
using Tag.KandinskyGen.Repositories.Entities;

namespace Tag.KandinskyGen.Repositories.Decorators;

internal class KandinskyCacheDecorator(IKandinskyRepository repository, IAppCache appCache) : IKandinskyRepository
{
    private readonly IAppCache _appCache = appCache;
    private readonly IKandinskyRepository _repository = repository;
    public Task<KandinskyGeneratioRequestResultEntity?> EnqueueGeneration(KandinskyGenerationRequestEntity entity) => _repository.EnqueueGeneration(entity);

    public Task<IEnumerable<KandinskyModelEntity>?> GetModels() => _appCache.GetOrAdd("models", _repository.GetModels);

    public Task<IList<KandinskyStyleEntity>?> GetStyles() => _appCache.GetOrAdd("styles", _repository.GetStyles);

    public Task<bool> ModelIsActive(int modelId) => _appCache.GetOrAdd($"modelisactive_{modelId}", () => _repository.ModelIsActive(modelId));
}
