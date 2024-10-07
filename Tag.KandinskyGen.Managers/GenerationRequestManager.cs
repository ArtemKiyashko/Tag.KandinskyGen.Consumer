using Tag.KandinskyGen.Managers.Dtos;
using Tag.KandinskyGen.Repositories;
using Tag.KandinskyGen.Repositories.Entities;
using Tag.KandinskyGen.Repositories.Enums;

namespace Tag.KandinskyGen.Managers;

internal class GenerationRequestManager(IGenerationRequestRepository generationRequestRepository) : IGenerationRequestManager
{
    private readonly IGenerationRequestRepository _generationRequestRepository = generationRequestRepository;

    public async Task AddGenerationActivity(GenerationRequestDto generationRequest)
    {
        var entity = new GenerationActivityEntity
        {
            PartitionKey = generationRequest.GenerationRequestedDateTime.UtcDateTime.ToString("dd-MM-yyyy"),
            RowKey = generationRequest.RequestId.ToString(),
            ChatTgId = generationRequest.ChatTgId,
            GenerationRequestedDateTime = generationRequest.GenerationRequestedDateTime,
            GenerationStatus = GenerationStatuses.InProgress,
            StartedDateTime = DateTimeOffset.UtcNow
        };

        await _generationRequestRepository.InsertGenerationActivity(entity);
    }
}
