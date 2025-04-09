using Tag.KandinskyGen.Repositories.Entities;

namespace Tag.KandinskyGen.Repositories.Decorators;

internal class KandinskyFakeDataDecorator(IKandinskyRepository repository) : IKandinskyRepository
{
    private readonly IKandinskyRepository _repository = repository;

    public Task<KandinskyGeneratioRequestResultEntity?> EnqueueGeneration(KandinskyGenerationRequestEntity entity) => _repository.EnqueueGeneration(entity);

    public Task<IEnumerable<KandinskyPipelineEntity>?> GetPipelines()
    {
        IEnumerable<KandinskyPipelineEntity> pipelines =
        [
            new() {
                Id = new Guid("a17740da-e8a0-4816-876a-74326c5c4cef"),
                Name = "Kandinsky",
                Version = 3.1f,
                Type = "TEXT2IMAGE",
                Status = "ACTIVE"
            }
        ];

        return Task.FromResult(pipelines);
    }

    public Task<IList<KandinskyStyleEntity>?> GetStyles()
    {
        IList<KandinskyStyleEntity> styles =
        [
            new() {
                Name = "DEFAULT",
                Title = "Свой стиль",
                TitleEn = "No style",
                Image = "https://cdn.fusionbrain.ai/static/download/img-style-personal.png"
            },
            new() {
                Name = "KANDINSKY",
                Title = "Кандинский",
                TitleEn = "Kandinsky",
                Image = "https://cdn.fusionbrain.ai/static/download/img-style-kandinsky.png"
            },
            new() {
                Name = "UHD",
                Title = "Детальное фото",
                TitleEn = "Detailed photo",
                Image = "https://cdn.fusionbrain.ai/static/download/img-style-detail-photo.png"
            }
            ,
            new() {
                Name = "ANIME",
                Title = "Аниме",
                TitleEn = "Anime",
                Image = "https://cdn.fusionbrain.ai/static/download/anime_new.jpg"
            }
        ];

        return Task.FromResult(styles);
    }

    public Task<bool> PipelineIsActive(Guid pipelineId) => Task.FromResult(true);
}
