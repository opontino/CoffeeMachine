namespace CoffeeWebApi.Models;

public class BrewResult
{
    public int StatusCode { get; init; }
    public object? Body { get; init; }

    public static BrewResult Success()
    {
        return new BrewResult
        {
            StatusCode = StatusCodes.Status200OK,
            Body = new
            {
                message = "Your piping hot coffee is ready",
                prepared = DateTimeOffset.Now.ToString("o")
            }
        };
    }

    public static BrewResult OutOfCoffee()
    {
        return new BrewResult
        {
            StatusCode = StatusCodes.Status503ServiceUnavailable
        };
    }

    public static BrewResult Teapot()
    {
        return new BrewResult
        {
            StatusCode = StatusCodes.Status418ImATeapot
        };
    }
}