using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Tag.KandinskyGen.Managers;

namespace Tag.KandinskyGen.Consumer
{
    public class ConsumerGenerationRequest(ILogger<ConsumerGenerationRequest> logger, IGenerationRequestManager generationRequestManager)
    {
        private readonly ILogger<ConsumerGenerationRequest> _logger = logger;
        private readonly IGenerationRequestManager _generationRequestManager = generationRequestManager;

        [Function(nameof(ConsumerGenerationRequest))]
        public async Task Run(
            [ServiceBusTrigger("generaterequests", "kandinskyconsumer", IsSessionsEnabled = true, Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            var generationRequest = await MessageValidator.TryGetGenerationRequest(message, messageActions);


            await _generationRequestManager.AddGenerationActivity(generationRequest);
        }
    }
}
