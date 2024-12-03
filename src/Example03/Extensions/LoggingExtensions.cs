namespace Example03.Extensions;

public static partial class LoggingExtensions
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Message ({MessageId}:{Category}) consumed.")]
    public static partial void LogConsumedMessage(this ILogger logger, Guid messageId, string category);

    [LoggerMessage(Level = LogLevel.Information, Message = "Message ({MessageId}:{Category}) published.")]
    public static partial void LogPublishedMessage(this ILogger logger, Guid messageId, string category);
}