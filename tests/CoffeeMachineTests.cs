namespace tests;

using CoffeeWebApi.Models;
using CoffeeWebApi.Services;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;

public class CoffeeMachineServiceTests
{
    [Fact]
    public async Task Should_Return_Success_On_First_Call()
    {
        var weatherMock = new Mock<IWeatherService>();
        weatherMock.Setup(x => x.GetCurrentTemperatureAsync())
                   .ReturnsAsync(29);

        var timeMock = new Mock<ITimeProvider>();
        timeMock.Setup(x => x.UtcNow)
                .Returns(new DateTime(2026, 3, 10));

        var service = new CoffeeMachineService(
            weatherMock.Object,
            timeMock.Object);

        var result = await service.BrewCoffeeAsync();

        result.StatusCode.Should().Be(200);

    }

    [Fact]
    public async Task Should_Return_503_On_Fifth_Call()
    {
        var weatherMock = new Mock<IWeatherService>();
        weatherMock.Setup(x => x.GetCurrentTemperatureAsync())
                   .ReturnsAsync(29);

        var timeMock = new Mock<ITimeProvider>();
        timeMock.Setup(x => x.UtcNow)
                .Returns(new DateTime(2026, 3, 10));

        var service = new CoffeeMachineService(
            weatherMock.Object,
            timeMock.Object);

        var result = new BrewResult();

        for (int i = 0; i < 5; i++)
        {
            result = await service.BrewCoffeeAsync();
        }
            
        result.StatusCode.Should().Be(503);
    }

    [Fact]
    public async Task Should_Return_200_After_Fifth_Call()
    {
        var weatherMock = new Mock<IWeatherService>();
        weatherMock.Setup(x => x.GetCurrentTemperatureAsync())
                   .ReturnsAsync(29);

        var timeMock = new Mock<ITimeProvider>();
        timeMock.Setup(x => x.UtcNow)
                .Returns(new DateTime(2026, 3, 10));

        var service = new CoffeeMachineService(
            weatherMock.Object,
            timeMock.Object);

        var result = new BrewResult();

        for (int i = 0; i < 6; i++)
        {
            result = await service.BrewCoffeeAsync();
        }

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task BrewCoffee_ShouldReturn418_WhenDateIsAprilFirst()
    {
        var weatherMock = new Mock<IWeatherService>();
        weatherMock.Setup(x => x.GetCurrentTemperatureAsync())
                   .ReturnsAsync(25); // temperature irrelevant

        var timeMock = new Mock<ITimeProvider>();
        timeMock.Setup(x => x.UtcNow)
                .Returns(new DateTime(2026, 4, 1)); // April 1

        var service = new CoffeeMachineService(
            weatherMock.Object,
            timeMock.Object);

        var result = await service.BrewCoffeeAsync();

        result.StatusCode.Should().Be(418);
        result.Body.Should().BeNull();
    }

    [Fact]
    public async Task Should_Return_Iced_Coffee_When_Temp_Above_30()
    {
        var weatherMock = new Mock<IWeatherService>();
        weatherMock.Setup(x => x.GetCurrentTemperatureAsync())
                   .ReturnsAsync(35);

        var timeMock = new Mock<ITimeProvider>();
        timeMock.Setup(x => x.UtcNow)
                .Returns(new DateTime(2026, 3, 10));

        var service = new CoffeeMachineService(
            weatherMock.Object,
            timeMock.Object);

        var result = await service.BrewCoffeeAsync();

        var body = result.Body.ToString();

        body.Should().Contain("iced coffee");
    }
}