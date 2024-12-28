using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Tag.KandinskyGen.Managers;
using Tag.KandinskyGen.Managers.Dtos;

namespace Tag.KandinskyGen.Consumer
{
    public class ConsumerGenerationRequest(
        ILogger<ConsumerGenerationRequest> logger, 
        IGenerationRequestManager generationRequestManager, 
        IKandinskyManager kandinskyManager,
        IMessageValidator messageValidator)
    {
        private readonly ILogger<ConsumerGenerationRequest> _logger = logger;
        private readonly IGenerationRequestManager _generationRequestManager = generationRequestManager;
        private readonly IKandinskyManager _kandinskyManager = kandinskyManager;
        private readonly IMessageValidator _messageValidator = messageValidator;

        [Function(nameof(ConsumerGenerationRequest))]
        public async Task Run(
            [ServiceBusTrigger("generaterequests", "kandinskyconsumer", IsSessionsEnabled = true, Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            var generationRequestViewModel = await _messageValidator.TryGetGenerationRequest(message, messageActions);
            
            var generationResponse = await _kandinskyManager.EnqueueGeneration(
                generationRequestViewModel.AlternativePrompt ?? generationRequestViewModel.ChatTitle, 
                generationRequestViewModel.ChatTgId);

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
