namespace CoffeeWebApi.Services;

public interface ITimeProvider
{
    DateTime UtcNow { get; }
}