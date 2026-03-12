namespace CoffeeWebApi.Models;

public class BrewResult
{
    public int StatusCode { get; init; }
    public object? Body { get; init; }

    public static BrewResult Success(string message)
    {
        return new BrewResult
        {
            StatusCode = 200,
            Body = new
            {
                message,
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

