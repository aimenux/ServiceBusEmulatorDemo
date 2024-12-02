using Azure.Messaging.ServiceBus;
using Example02.Configuration;
using Example02.Consumers;
using Example02.Producers;
using Microsoft.Extensions.Options;

namespace Example02;

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
        builder.Services.AddHostedService<TopicConsumer>();
        builder.Services.AddHostedService<TopicProducer>();
    }
}