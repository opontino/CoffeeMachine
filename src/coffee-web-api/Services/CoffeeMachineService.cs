namespace CoffeeWebApi.Services;
using CoffeeWebApi.Models;

public class CoffeeMachineService : ICoffeeMachineService
{
    private readonly IWeatherService _weatherService;
    private readonly ITimeProvider _timeProvider;
    private readonly object _lock = new object();

    private int _counter = 0;

    public CoffeeMachineService(
        IWeatherService weatherService,
        ITimeProvider timeProvider)
    {
        _weatherService = weatherService;
        _timeProvider = timeProvider;
    }

    public async Task<BrewResult> BrewCoffeeAsync()
    {
        var now = _timeProvider.UtcNow;

        if (now.Month == 4 && now.Day == 1)
        {
            return BrewResult.Teapot();
        }

        lock (_lock)
        {
            _counter++;
        }
           
        if (_counter % 5 == 0)
        {
            return BrewResult.OutOfCoffee();
        }

        var temp = await _weatherService.GetCurrentTemperatureAsync();

        var message =
            temp > 30
            ? "Your refreshing iced coffee is ready"
            : "Your piping hot coffee is ready";

        return BrewResult.Success(message);
    }
}