using Microsoft.Extensions.Options;

namespace Example03.Configuration;

public sealed class SettingsValidator : IValidateOptions<Settings>
{
    public ValidateOptionsResult Validate(string? name, Settings? options)
    {
        if (options is null)
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings)} is required.");
        }

        if (options.ConsumerDelayInSeconds <= 0)
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.ConsumerDelayInSeconds)} must be greater than 0.");
        }

        if (options.ProducerDelayInSeconds <= 0)
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.ProducerDelayInSeconds)} must be greater than 0.");
        }

        if (string.IsNullOrWhiteSpace(options.TopicName))
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.TopicName)} is required.");
        }

        if (string.IsNullOrWhiteSpace(options.LowSubscriptionName))
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.LowSubscriptionName)} is required.");
        }

        if (string.IsNullOrWhiteSpace(options.HighSubscriptionName))
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.HighSubscriptionName)} is required.");
        }

        if (string.IsNullOrWhiteSpace(options.ConnectionString))
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.ConnectionString)} is required.");
        }

        return ValidateOptionsResult.Success;
    }
}