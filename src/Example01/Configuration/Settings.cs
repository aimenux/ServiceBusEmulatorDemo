namespace Example01.Configuration;

public class Settings
{
    public const string SectionName = "Settings";

    public string QueueName { get; init; } = default!;
    public int ConsumerDelayInSeconds { get; init; } = 1;
    public int ProducerDelayInSeconds { get; init; } = 5;
    public string ConnectionString { get; init; } = default!;
}