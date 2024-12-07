using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Example02.Configuration;
using Example02.Contracts;
using Example02.Extensions;
using Microsoft.Extensions.Options;

namespace Example02.Producers;

public sealed class TopicProducer : BackgroundService, IAsyncDisposable
{
    private readonly ServiceBusClient _client;
    private readonly IOptions<Settings> _options;
    private readonly ILogger<TopicProducer> _logger;

    public TopicProducer(ServiceBusClient client, IOptions<Settings> options, ILogger<TopicProducer> logger)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var sender = _client.CreateSender(_options.Value.TopicName);

        while (!cancellationToken.IsCancellationRequested)
        {
            var message = new Message();
            var serviceBusMessage = new ServiceBusMessage(JsonSerializer.Serialize(message))
            {
                ApplicationProperties =
                {
                    [nameof(Message.Category)] = message.Category
                }
            };
            await sender.SendMessageAsync(serviceBusMessage, cancellationToken);
            _logger.LogPublishedMessage(message.Id, message.Category);
            await Task.Delay(TimeSpan.FromSeconds(_options.Value.ProducerDelayInSeconds), cancellationToken);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _client.DisposeAsync();
    }
}