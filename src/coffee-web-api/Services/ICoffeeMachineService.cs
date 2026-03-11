namespace CoffeeWebApi.Services;
using CoffeeWebApi.Models;

public interface ICoffeeMachineService
{
    BrewResult BrewCoffee();
}