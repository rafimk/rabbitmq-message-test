using Message.Api.Messages;
using Message.Shared.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace Message.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    private readonly IMessagePublisher _messagePublisher;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(IMessagePublisher messagePublisher, ILogger<WeatherForecastController> logger)
    {
        _messagePublisher = messagePublisher;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        string messageId = Guid.NewGuid().ToString("N");
        var message = new UserCreated("Muhammed Rafi", "rafi.genius.cs@gmail.com", "0425");
        await _messagePublisher.PublishAsync("user", $"created", message, messageId);
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}