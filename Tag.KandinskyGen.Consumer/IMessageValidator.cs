using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Tag.KandinskyGen.Consumer.ViewModels;

namespace Tag.KandinskyGen.Consumer;

public interface IMessageValidator
{
    Task<GenerationRequestViewModel> TryGetGenerationRequest(ServiceBusReceivedMessage message, ServiceBusMessageActions messageActions);
}
