using Tag.KandinskyGen.Repositories.Entities;

namespace Tag.KandinskyGen.Repositories.Decorators;

internal class KandinskyFakeDataDecorator(IKandinskyRepository repository) : IKandinskyRepository
{
    private readonly IKandinskyRepository _repository = repository;

    public Task<KandinskyGeneratioRequestResultEntity?> EnqueueGeneration(KandinskyGenerationRequestEntity entity) => _repository.EnqueueGeneration(entity);

    public Task<IEnumerable<KandinskyModelEntity>?> GetModels()
    {
        IEnumerable<KandinskyModelEntity> models =
        [
            new() {
                Id = 4,
                Name = "Kandinsky",
                Version = 3.1f,
                Type = "TEXT2IMAGE"
            }
        ];

        return Task.FromResult(models);
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

    public Task<bool> ModelIsActive(int modelId) => Task.FromResult(true);
}
