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
        builder.AddSettings();
        builder.AddServiceBus();
    }
    
    private static void AddSettings(this HostApplicationBuilder builder)
    {
        builder.Services.Configure<Settings>(builder.Configuration.GetSection(Settings.SectionName));
        builder.Services.AddSingleton<IValidateOptions<Settings>, SettingsValidator>();
    }

    private static void AddServiceBus(this HostApplicationBuilder builder)
    {
        builder.Services.AddHostedService<LowTopicConsumer>();
        builder.Services.AddHostedService<HighTopicConsumer>();
        builder.Services.AddHostedService<TopicProducer>();

        builder.Services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<Settings>>().Value;
            var client = new ServiceBusClient(settings.ConnectionString);
            return client;
        });
    }
}