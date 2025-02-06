using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Tag.KandinskyGen.Managers;
using Tag.KandinskyGen.Managers.Dtos;
using Telegram.Bot;

namespace Tag.KandinskyGen.Consumer
{
    public class ConsumerGenerationRequest(
        ILogger<ConsumerGenerationRequest> logger,
        IGenerationRequestManager generationRequestManager,
        IKandinskyManager kandinskyManager,
        IMessageValidator messageValidator,
        ITelegramBotClient telegramBotClient)
    {
        private readonly ILogger<ConsumerGenerationRequest> _logger = logger;
        private readonly IGenerationRequestManager _generationRequestManager = generationRequestManager;
        private readonly IKandinskyManager _kandinskyManager = kandinskyManager;
        private readonly IMessageValidator _messageValidator = messageValidator;
        private readonly ITelegramBotClient _telegramBotClient = telegramBotClient;

        [Function(nameof(ConsumerGenerationRequest))]
        public async Task Run(
            [ServiceBusTrigger("generaterequests", "kandinskyconsumer", IsSessionsEnabled = true, Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            GenerationResponseDto generationResponse;

            var generationRequestViewModel = await _messageValidator.TryGetGenerationRequest(message, messageActions);

            try
            {
                generationResponse = await _kandinskyManager.EnqueueGeneration(
                    generationRequestViewModel.AlternativePrompt ?? generationRequestViewModel.ChatTitle,
                    generationRequestViewModel.ChatTgId);
            }
            catch (InvalidOperationException)
            {
                if (message.DeliveryCount == 10)
                {
                    await _telegramBotClient.SendTextMessageAsync(chatId: generationRequestViewModel.ChatTgId, text: "Модель в данный момент недоступна. Попробуйте позже.");
                }
                throw;
            }
            catch (Exception)
            {
                if (message.DeliveryCount == 10)
                {
                    await _telegramBotClient.SendTextMessageAsync(chatId: generationRequestViewModel.ChatTgId, text: "Произошла ошибка при обработке запроса. Попробуйте позже.");
                }
                throw;
            }

            var genActivityRequestDto = new GenerationActivityRequestDto
            {
                RequestId = generationRequestViewModel.RequestId,
                ChatTgId = generationResponse.ChatTgId,
                RequestType = generationRequestViewModel.RequestType,
                GenerationRequestedDateTime = generationRequestViewModel.GenerationRequestedDateTime,
                Uuid = generationResponse.Uuid,
                Prompt = generationResponse.Prompt,
                JsonPayload = generationResponse.JsonPayload
            };

            await _generationRequestManager.AddGenerationActivity(genActivityRequestDto);
        }
    }
}
