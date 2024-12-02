using Microsoft.Extensions.Options;

namespace Example01.Configuration;

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

        if (string.IsNullOrWhiteSpace(options.QueueName))
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.QueueName)} is required.");
        }

        if (string.IsNullOrWhiteSpace(options.ConnectionString))
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.ConnectionString)} is required.");
        }

        return ValidateOptionsResult.Success;
    }
}