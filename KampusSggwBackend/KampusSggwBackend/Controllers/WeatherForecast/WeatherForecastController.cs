namespace KampusSggwBackend.Controllers.WeatherForecast;

using Microsoft.AspNetCore.Mvc;
using KampusSggwBackend.Controllers.WeatherForecast.Results;
using Microsoft.AspNetCore.Authorization;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet("GetWeatherForecast")]
    public async Task<ActionResult<List<WeatherForecast>>> Get()
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            })
            .ToList();

        return Ok(forecast);
    }
}
