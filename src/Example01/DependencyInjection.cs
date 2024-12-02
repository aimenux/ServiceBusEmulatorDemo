using Azure.Messaging.ServiceBus;
using Example01.Configuration;
using Example01.Consumers;
using Example01.Producers;
using Microsoft.Extensions.Options;

namespace Example01;

public static class DependencyInjection
{
    public static HostApplicationBuilder AddServices(this HostApplicationBuilder builder)
    {
        builder.Services.Configure<Settings>(builder.Configuration.GetSection(Settings.SectionName));
        builder.Services.AddSingleton<IValidateOptions<Settings>, SettingsValidator>();
        builder.Services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<Settings>>();
            var connectionString = options.Value.ConnectionString;
            return new ServiceBusClient(connectionString);
        });
        builder.Services.AddHostedService<QueueConsumer>();
        builder.Services.AddHostedService<QueueProducer>();
        return builder;
    }
}