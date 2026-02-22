using Microsoft.AspNetCore.Mvc;
using ChallengeBlip.Services;
using Microsoft.AspNetCore.Http;
using ChallengeBlip.Models;
namespace ChallengeBlip.Controllers;

[ApiController]
[Route("api/weather")]

public class WeatherController : ControllerBase
{
    private readonly WeatherService _service;

    public WeatherController(WeatherService service)
    {
        _service = service;
    }


    [HttpGet]
    [ProducesResponseType(typeof(WeatherResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> Get(
        [FromQuery] string city,
        string lang = "pt"
        )
    {
        try
        {
            var header = HttpContext.Items["WeatherApiKey"]?.ToString() ?? "";

            var result = await _service.GetWeatherAsync(city, DateTime.UtcNow, header, lang);
            var weather = _service.JsonFormatted(result);

            return StatusCode(200, weather);
        }

        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }

        catch
        {
            return StatusCode(500, "Erro no servidor");
        }
    }
}