using Azure.Messaging.ServiceBus;
using Example02.Configuration;
using Example02.Contracts;
using Example02.Extensions;
using Microsoft.Extensions.Options;

namespace Example02.Consumers;

public class TopicConsumer : BackgroundService
{
    private readonly ServiceBusClient _client;
    private readonly IOptions<Settings> _options;
    private readonly ILogger<TopicConsumer> _logger;

    public TopicConsumer(ServiceBusClient client, IOptions<Settings> options, ILogger<TopicConsumer> logger)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var receiver = _client.CreateReceiver(_options.Value.TopicName, _options.Value.SubscriptionName);

        while (!cancellationToken.IsCancellationRequested)
        {
            var message = await receiver.ReceiveMessageAsync<Message>(cancellationToken);
            if (message is not null)
            {
                _logger.LogConsumedMessage(message.Id);
            }

            await Task.Delay(TimeSpan.FromSeconds(_options.Value.ConsumerDelayInSeconds), cancellationToken);
        }
    }
}