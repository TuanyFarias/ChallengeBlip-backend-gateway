using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ChallengeBlip.Controllers;
using ChallengeBlip.Services;
using ChallengeBlip.Models;
using Microsoft.AspNetCore.Http;

public class WeatherControllerTests
{
    public class FakeWeatherService : WeatherService
    {
        public FakeWeatherService() : base(new HttpClient(), new ConfigurationBuilder().Build()) { }

        public new Task<string> GetWeatherAsync(string city, DateTime date, string header, string lang)
        {
            return Task.FromResult("{\"location\":{\"name\":\"São Paulo\"},\"current\":{\"temp_c\":25}}");
        }

        public new WeatherResponse JsonFormatted(string result)
        {
            return new WeatherResponse
            {
                location = new Location { name = "São Paulo" },
                Current = new Current { TempC = 25 }
            };
        }
    }

    [Fact]
    public async Task GetWeather_ReturnsOk_WithFakeService()
    {
        var controller = new WeatherController(new FakeWeatherService());

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
        controller.HttpContext.Items["WeatherApiKey"] = "fakekey";

        var result = await controller.Get("São Paulo");

        var okResult = Assert.IsType<OkObjectResult>(result);
        var weather = Assert.IsType<WeatherResponse>(okResult.Value);

        Assert.Equal("São Paulo", weather!.location!.name);
        Assert.Equal(25, weather!.Current!.TempC);
    }
}