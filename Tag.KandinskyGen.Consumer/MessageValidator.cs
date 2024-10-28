using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Tag.KandinskyGen.Consumer.ViewModels;
using Telegram.Bot;

namespace Tag.KandinskyGen.Consumer;

public class MessageValidator(ITelegramBotClient telegramBotClient) : IMessageValidator
{
    private readonly ITelegramBotClient _telegramBotClient = telegramBotClient;

    public async Task<GenerationRequestViewModel> TryGetGenerationRequest(ServiceBusReceivedMessage message, ServiceBusMessageActions messageActions)
    {
        try
        {
            var generationRequest = await JsonSerializer.DeserializeAsync<GenerationRequestViewModel>(message.Body.ToStream())
                ?? throw new ArgumentException($"Cannot deserialize message body as {nameof(GenerationRequestViewModel)}", nameof(message));
            
            var prompt = generationRequest.AlternativePrompt ?? generationRequest.ChatTitle;
            if (prompt.Length > 1000)
            {
                await _telegramBotClient.SendTextMessageAsync(chatId: generationRequest.ChatTgId, text: "Этот запрос слишком длинный. Максимальная длина запроса - 1000 символов.");
                throw new ArgumentException("Prompt cannot be more than 1000 chars");
            }

            return generationRequest;
        }
        catch (Exception)
        {
            await messageActions.DeadLetterMessageAsync(message);
            throw;
        }
    }
}
