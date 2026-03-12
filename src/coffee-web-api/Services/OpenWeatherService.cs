using CoffeeWebApi.Models;

namespace CoffeeWebApi.Services;

public class OpenWeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public OpenWeatherService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<double> GetCurrentTemperatureAsync()
    {
        var apiKey = _config["Weather:ApiKey"];
        var city = _config["Weather:City"];

        var url =
            $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        var response = await _httpClient.GetFromJsonAsync<WeatherResponse>(url);

        return response.Main.Temp;
    }

}
