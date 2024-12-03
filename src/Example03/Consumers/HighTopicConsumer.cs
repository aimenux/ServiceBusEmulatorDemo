using Azure.Messaging.ServiceBus;
using Example03.Configuration;
using Example03.Contracts;
using Example03.Extensions;
using Microsoft.Extensions.Options;

namespace Example03.Consumers;

public class HighTopicConsumer : BackgroundService
{
    private readonly ServiceBusClient _client;
    private readonly IOptions<Settings> _options;
    private readonly ILogger<HighTopicConsumer> _logger;

    public HighTopicConsumer(ServiceBusClient client, IOptions<Settings> options, ILogger<HighTopicConsumer> logger)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var receiver = _client.CreateReceiver(_options.Value.TopicName, _options.Value.HighSubscriptionName);

        while (!cancellationToken.IsCancellationRequested)
        {
            var message = await receiver.ReceiveMessageAsync<Message>(cancellationToken);
            if (message is not null)
            {
                _logger.LogConsumedMessage(message.Id, message.Category);
            }

            await Task.Delay(TimeSpan.FromSeconds(_options.Value.ConsumerDelayInSeconds), cancellationToken);
        }
    }
}