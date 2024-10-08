using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Tag.KandinskyGen.Consumer.ViewModels;

namespace Tag.KandinskyGen.Consumer;

public static class MessageValidator
{
    public static async Task<GenerationRequestViewModel> TryGetGenerationRequest(ServiceBusReceivedMessage message, ServiceBusMessageActions messageActions)
    {
        try
        {
            var generationRequest = await JsonSerializer.DeserializeAsync<GenerationRequestViewModel>(message.Body.ToStream())
                ?? throw new ArgumentException($"Cannot deserialize message body as {nameof(GenerationRequestViewModel)}", nameof(message));
            return generationRequest;
        }
        catch (Exception)
        {
            await messageActions.DeadLetterMessageAsync(message);
            throw;
        }
    }
}
