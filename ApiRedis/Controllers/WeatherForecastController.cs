using ApiRedis.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiRedis.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ICacheService cache) : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    private const string CacheKey = "weather-forecast";

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        // Tenta buscar do Redis primeiro
        var cached = await cache.GetAsync<WeatherForecast[]>(CacheKey);
        if (cached is not null)
            return cached;

        // Se não tem cache, gera e salva por 30 segundos
        var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();

        await cache.SetAsync(CacheKey, forecast, TimeSpan.FromSeconds(30));

        return forecast;
    }

    [HttpDelete("cache", Name = "ClearWeatherCache")]
    public async Task<IActionResult> ClearCache()
    {
        await cache.RemoveAsync(CacheKey);
        return Ok(new { message = "Cache limpo!" });
    }
}
