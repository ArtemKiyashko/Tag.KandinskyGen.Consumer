using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Tag.KandinskyGen.Managers.Dtos;

namespace Tag.KandinskyGen.Consumer;

public static class MessageValidator
{
    public static async Task<GenerationRequestDto> TryGetGenerationRequest(ServiceBusReceivedMessage message, ServiceBusMessageActions messageActions)
    {
        try
        {
            var generationRequest = await JsonSerializer.DeserializeAsync<GenerationRequestDto>(message.Body.ToStream())
                ?? throw new ArgumentException($"Cannot deserialize message body as {nameof(GenerationRequestDto)}", nameof(message));
            return generationRequest;
        }
        catch (Exception)
        {
            await messageActions.DeadLetterMessageAsync(message);
            throw;
        }
    }
}
