using Microsoft.SemanticKernel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKernel();

builder.Services.AddOpenAIChatCompletion("gpt-3.5-turbo", Environment.GetEnvironmentVariable("OPEN_AI_KEY")!);


var app = builder.Build();

// Configure the HTTP request pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", async (Kernel kernel) =>
{
    int temp = Random.Shared.Next(-20, 55);
    return new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now),
            temp,
            await kernel.InvokePromptAsync<string>($"Short description of the weather at {temp} celsius")
        );
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
