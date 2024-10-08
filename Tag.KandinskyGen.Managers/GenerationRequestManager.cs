using Tag.KandinskyGen.Managers.Dtos;
using Tag.KandinskyGen.Repositories;
using Tag.KandinskyGen.Repositories.Entities;
using Tag.KandinskyGen.Repositories.Enums;

namespace Tag.KandinskyGen.Managers;

internal class GenerationRequestManager(IGenerationRequestRepository generationRequestRepository) : IGenerationRequestManager
{
    private readonly IGenerationRequestRepository _generationRequestRepository = generationRequestRepository;

    public async Task AddGenerationActivity(GenerationActivityRequestDto generationActivityRequest)
    {
        var entity = new GenerationActivityEntity
        {
            PartitionKey = generationActivityRequest.GenerationRequestedDateTime.UtcDateTime.ToString("dd-MM-yyyy"),
            RowKey = generationActivityRequest.RequestId.ToString(),
            ChatTgId = generationActivityRequest.ChatTgId,
            GenerationRequestedDateTime = generationActivityRequest.GenerationRequestedDateTime,
            GenerationStatus = GenerationStatuses.InProgress,
            StartedDateTime = DateTimeOffset.UtcNow,
            Prompt = generationActivityRequest.Prompt,
            Uuid = generationActivityRequest.Uuid
        };

        await _generationRequestRepository.InsertGenerationActivity(entity);
    }
}
