namespace CoffeeWebApi.Services;


public interface IWeatherService
{
    Task<double> GetCurrentTemperatureAsync();
}
