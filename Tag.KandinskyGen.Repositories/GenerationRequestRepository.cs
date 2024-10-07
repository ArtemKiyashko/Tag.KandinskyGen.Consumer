using Azure.Data.Tables;
using Tag.KandinskyGen.Repositories.Entities;

namespace Tag.KandinskyGen.Repositories;

internal class GenerationRequestRepository(TableClient tableClient) : IGenerationRequestRepository
{
    private readonly TableClient _tableClient = tableClient;

    public Task InsertGenerationActivity(GenerationActivityEntity entity) => _tableClient.AddEntityAsync(entity);
}
