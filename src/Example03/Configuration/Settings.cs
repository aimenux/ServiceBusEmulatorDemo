namespace Example03.Configuration;

public class Settings
{
    public const string SectionName = "Settings";

    public string TopicName { get; init; } = default!;
    public string LowSubscriptionName { get; init; } = default!;
    public string HighSubscriptionName { get; init; } = default!;
    public int ConsumerDelayInSeconds { get; init; } = 1;
    public int ProducerDelayInSeconds { get; init; } = 5;
    public string ConnectionString { get; init; } = default!;
}