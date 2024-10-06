using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Tag.KandinskyGen.Consumer
{
    public class ConsumerGenerationRequest
    {
        private readonly ILogger<ConsumerGenerationRequest> _logger;

        public ConsumerGenerationRequest(ILogger<ConsumerGenerationRequest> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ConsumerGenerationRequest))]
        public async Task Run(
            [ServiceBusTrigger("generaterequests", "kandinskyconsumer", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

             // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
