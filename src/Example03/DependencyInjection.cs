using Azure.Messaging.ServiceBus;
using Example03.Configuration;
using Example03.Consumers;
using Example03.Producers;
using Microsoft.Extensions.Options;

namespace Example03;

public static class DependencyInjection
{
    public static void AddServices(this HostApplicationBuilder builder)
    {
        builder.Services.Configure<Settings>(builder.Configuration.GetSection(Settings.SectionName));
        builder.Services.AddSingleton<IValidateOptions<Settings>, SettingsValidator>();
        builder.Services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<Settings>>();
            var connectionString = options.Value.ConnectionString;
            return new ServiceBusClient(connectionString);
        });
        builder.Services.AddHostedService<LowTopicConsumer>();
        builder.Services.AddHostedService<HighTopicConsumer>();
        builder.Services.AddHostedService<TopicProducer>();
    }
}