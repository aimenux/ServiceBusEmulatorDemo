using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Example01.Configuration;
using Example01.Contracts;
using Example01.Extensions;
using Microsoft.Extensions.Options;

namespace Example01.Producers;

public class QueueProducer : BackgroundService
{
    private readonly ServiceBusClient _client;
    private readonly IOptions<Settings> _options;
    private readonly ILogger<QueueProducer> _logger;

    public QueueProducer(ServiceBusClient client, IOptions<Settings> options, ILogger<QueueProducer> logger)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var sender = _client.CreateSender(_options.Value.QueueName);

        while (!cancellationToken.IsCancellationRequested)
        {
            var message = new Message();
            var serviceBusMessage = new ServiceBusMessage(JsonSerializer.Serialize(message));
            await sender.SendMessageAsync(serviceBusMessage, cancellationToken);
            _logger.LogPublishedMessage(message.Id);
            await Task.Delay(TimeSpan.FromSeconds(_options.Value.ProducerDelayInSeconds), cancellationToken);
        }
    }
}