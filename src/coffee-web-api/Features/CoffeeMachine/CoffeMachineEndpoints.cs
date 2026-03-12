namespace CoffeeWebApi.Features;
using CoffeeWebApi.Models;
using CoffeeWebApi.Services;

public static class CoffeeMachineEndpoints
{
    public static void MapCoffeeMachine(this IEndpointRouteBuilder app)
    {
        app.MapGet("/brew-coffee", async (ICoffeeMachineService service) =>
        {
            var result = await service.BrewCoffeeAsync();

            if (result.Body == null)
                return Results.StatusCode(result.StatusCode);

            return Results.Json(result.Body, statusCode: result.StatusCode);
        });
    }
}