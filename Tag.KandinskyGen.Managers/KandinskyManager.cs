using Microsoft.Extensions.Options;
using Tag.KandinskyGen.Managers.Dtos;
using Tag.KandinskyGen.Repositories;
using Tag.KandinskyGen.Repositories.Entities;

namespace Tag.KandinskyGen.Managers;

internal class KandinskyManager(IKandinskyRepository kandinskyRepository, IOptionsMonitor<KandinskyOptions> options) : IKandinskyManager
{
    private readonly IKandinskyRepository _kandinskyRepository = kandinskyRepository;
    private readonly KandinskyOptions _options = options.CurrentValue;

    public async Task<GenerationResponseDto> EnqueueGeneration(string prompt, long chatTgid)
    {
        var models = await _kandinskyRepository.GetModels();
        ArgumentNullException.ThrowIfNull(models);

        var latestTxt2ImageModel = models.Where(m => m.Type == "TEXT2IMAGE").OrderByDescending(m => m.Version).FirstOrDefault();
        ArgumentNullException.ThrowIfNull(latestTxt2ImageModel);

        var modelIsAvailable = await _kandinskyRepository.ModelIsActive(latestTxt2ImageModel.Id);

        if (!modelIsAvailable)
            throw new InvalidOperationException($"Model (id: {latestTxt2ImageModel.Id}) is not available this time");

        var entity = new KandinskyGenerationRequestEntity
        {
            ModelId = latestTxt2ImageModel.Id,
            Params = new KandinskyParamsEntity
            {
                NumImages = 1,
                Width = _options.PictureWidth,
                Height = _options.PictureHeight,
                GenerateParams = new KandinskyGenerateParamsEntity
                {
                    Query = prompt,
                    Style = _options.PictureStyle
                }
            }
        };

        var genResult = await _kandinskyRepository.EnqueueGeneration(entity);
        ArgumentNullException.ThrowIfNull(genResult);
        
        return new GenerationResponseDto{
            ChatTgId = chatTgid,
            Prompt = prompt,
            Uuid = genResult.Uuid
        };
    }
}
