using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace Example01.Extensions;

public static class ServiceBusExtensions
{
    public static async Task<T?> ReceiveMessageAsync<T>(this ServiceBusReceiver receiver, CancellationToken cancellationToken) where T : class
    {
        var serviceBusMessage = await receiver.ReceiveMessageAsync(cancellationToken: cancellationToken);
        var messageBody = serviceBusMessage?.Body?.ToString();
        return string.IsNullOrEmpty(messageBody)
            ? default
            : JsonSerializer.Deserialize<T>(messageBody);
    }
}