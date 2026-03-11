namespace CoffeeWebApi.Services;
using CoffeeWebApi.Models;

public class CoffeeMachineService : ICoffeeMachineService
{
    private int _counter = 0;
    private readonly Lock _lock = new();

    public BrewResult BrewCoffee()
    {
        if (DateTime.UtcNow.Month == 4 && DateTime.UtcNow.Day == 1)
        {
            return BrewResult.Teapot();
        }

        lock (_lock)
        {
            _counter++;

            if (_counter % 5 == 0)
            {
                return BrewResult.OutOfCoffee();
            }
        }

        return BrewResult.Success();
    }
}