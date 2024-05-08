using api_inges_dev.Context;
using api_inges_dev.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace api_inges_dev.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly ConexionSQLServer contex;

    public WeatherForecastController(ConexionSQLServer contex)
    {
        this.contex = contex;
    }

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    // private readonly ILogger<WeatherForecastController> _logger;

    // public WeatherForecastController(ILogger<WeatherForecastController> logger)
    // {
    //     _logger = logger;
    // }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<Technologies> Get()
    {
        return contex.technologies.ToList();
    }
}
