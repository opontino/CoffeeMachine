namespace CoffeeWebApi.Services;
using CoffeeWebApi.Models;

public interface ICoffeeMachineService
{
    Task<BrewResult> BrewCoffeeAsync();
}