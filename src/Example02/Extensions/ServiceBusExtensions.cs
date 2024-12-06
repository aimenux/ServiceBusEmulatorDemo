using Azure.Messaging.ServiceBus;

namespace Example02.Extensions;

public static class ServiceBusExtensions
{
    public static async Task<T?> ReceiveMessageAsync<T>(this ServiceBusReceiver receiver, CancellationToken cancellationToken) where T : class
    {
        var serviceBusMessage = await receiver.ReceiveMessageAsync(cancellationToken: cancellationToken);
        var message = serviceBusMessage?.Body?.ToObjectFromJson<T>();
        return message;
    }
}