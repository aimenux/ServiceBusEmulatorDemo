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
        builder.Services.AddHostedService<TopicConsumer>();
        builder.Services.AddHostedService<TopicProducer>();

        builder.Services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<Settings>>().Value;
            var client = new ServiceBusClient(settings.ConnectionString);
            return client;
        });
    }
}