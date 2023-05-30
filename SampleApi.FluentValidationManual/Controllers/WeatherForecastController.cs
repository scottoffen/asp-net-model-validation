using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using SampleApi.FluentValidationManual.Model;

namespace SampleApi.FluentValidationManual.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IValidator<SampleRequest> _validator;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        IValidator<SampleRequest> validator
    )
    {
        _logger = logger;
        _validator = validator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost]
    public IActionResult Post(SampleRequest model)
    {
        var validationResult = _validator.Validate(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(this.ModelState);
            return BadRequest(this.ModelState);
        }

        return Ok(model);
    }
}
