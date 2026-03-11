using CoffeeWebApi.Features;
using CoffeeWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSingleton<ICoffeeMachineService, CoffeeMachineService>();

var app = builder.Build();

app.MapCoffeeMachine();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();

