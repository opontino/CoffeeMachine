namespace tests;

using CoffeeWebApi.Services;
using CoffeeWebApi.Models;
using FluentAssertions;

public class CoffeeMachineServiceTests
{
    [Fact]
    public void Should_Return_Success_On_First_Call()
    {
        var service = new CoffeeMachineService();

        var result = service.BrewCoffee();

        result.StatusCode.Should().Be(200);
        result.Body.Should().NotBeNull();
    }

    [Fact]
    public void Should_Return_503_On_Fifth_Call()
    {
        var service = new CoffeeMachineService();

        BrewResult result = null;

        for (int i = 0; i < 5; i++)
        {
            result = service.BrewCoffee();
        }

        result.StatusCode.Should().Be(503);
    }

    [Fact]
    public void Should_Return_200_After_Fifth_Call()
    {
        var service = new CoffeeMachineService();

        for (int i = 0; i < 5; i++)
        {
            service.BrewCoffee();
        }

        var result = service.BrewCoffee();

        result.StatusCode.Should().Be(200);
    }
}