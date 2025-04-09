using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Tag.KandinskyGen.Repositories.Entities;

namespace Tag.KandinskyGen.Repositories;

internal class KandinskyRepository(HttpClient httpClient) : IKandinskyRepository
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<KandinskyGeneratioRequestResultEntity?> EnqueueGeneration(KandinskyGenerationRequestEntity entity)
    {
        var paramsJson = JsonSerializer.Serialize(entity.Params);
        using var paramsContent = new StringContent(paramsJson, new MediaTypeHeaderValue("application/json"));

        using var PipelineId = new StringContent(entity.PipelineId.ToString());

        using var formData = new MultipartFormDataContent
        {
            { paramsContent, "params" },
            { PipelineId, "pipeline_id" }
        };

        var resultResponse = await _httpClient.PostAsync("key/api/v1/pipeline/run", formData);

        if (!resultResponse.IsSuccessStatusCode)
            throw new InvalidOperationException(resultResponse.ReasonPhrase);

        return await JsonSerializer.DeserializeAsync<KandinskyGeneratioRequestResultEntity>(await resultResponse.Content.ReadAsStreamAsync());
    }

    public async Task<IEnumerable<KandinskyPipelineEntity>?> GetPipelines()
    {
        var result = await _httpClient.GetFromJsonAsync<List<KandinskyPipelineEntity>>("key/api/v1/pipelines");
        return result;
    }

    public async Task<IList<KandinskyStyleEntity>?> GetStyles()
    {
        var result = await _httpClient.GetFromJsonAsync<List<KandinskyStyleEntity>>("https://cdn.fusionbrain.ai/static/styles/key");
        return result;
    }

    public async Task<bool> PipelineIsActive(Guid pipelineId)
    {
        var result = await _httpClient.GetFromJsonAsync<KandinskyPiplineStatusEntity>($"key/api/v1/pipeline/{pipelineId}/availability");
        return result is not null && result.Status == "ACTIVE";
    }
}
