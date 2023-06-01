using Microsoft.AspNetCore.Mvc;
using SampleApi.CustomValidation.Models;
using Wsr.ModelValidation;

namespace SampleApi.CustomValidation.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private IModelValidatorService _modelValidationService;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        IModelValidatorService modelValidationService
    )
    {
        _logger = logger;
        _modelValidationService = modelValidationService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get([FromQuery] SampleReadRequest request)
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
    public IActionResult Post(SampleCreateRequest request)
    {
        return Ok(request);
    }

    [HttpPost]
    [Route("/{id}")]
    public IActionResult PostMore([FromBody]SampleCreateRequest request, [FromQuery]SampleReadRequest id)
    {
        return Ok(request);
    }

    [HttpPut]
    public IActionResult Put(SampleUpdateRequest request)
    {
        _modelValidationService.ValidateAndThrow(request);
        return Ok(request);
    }
}
