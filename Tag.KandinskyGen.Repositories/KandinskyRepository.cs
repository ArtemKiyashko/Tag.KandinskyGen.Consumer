using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Tag.KandinskyGen.Repositories.Entities;

namespace Tag.KandinskyGen.Repositories;

internal class KandinskyRepository(HttpClient httpClient) : IKandinskyRepository
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<KandinskyGeneratioRequestResultEntity?> EnqueueGeneration(KandinskyGenerationRequestEntity entity)
    {
        var paramsJson = JsonSerializer.Serialize(entity.Params);
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(paramsJson));
        using var fileHttpContent = new StreamContent(memoryStream);

        using var stringContent = new StringContent(entity.ModelId.ToString());

        using var formData = new MultipartFormDataContent
        {
            { fileHttpContent, "params" },
            { stringContent, "model_id" }
        };

        var resultResponse = await _httpClient.PostAsync("key/api/v1/text2image/run", formData);

        return await JsonSerializer.DeserializeAsync<KandinskyGeneratioRequestResultEntity>(await resultResponse.Content.ReadAsStreamAsync());
    }

    public async Task<IEnumerable<KandinskyModelEntity>?> GetModels()
    {
        var result = await _httpClient.GetFromJsonAsync<List<KandinskyModelEntity>>("key/api/v1/models");
        return result;
    }

    public async Task<bool> ModelIsActive(int modelId)
    {
        var result = await _httpClient.GetFromJsonAsync<KandinskyModelStatusEntity>($"key/api/v1/text2image/availability?model_id={modelId}");
        return result is not null && result.Status == "ACTIVE";
    }
}
