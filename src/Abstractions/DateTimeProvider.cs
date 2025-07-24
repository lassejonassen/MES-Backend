namespace Abstractions;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
