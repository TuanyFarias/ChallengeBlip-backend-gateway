
using System.Net;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using ChallengeBlip.Services;


public class WeatherServiceTests
{
    private WeatherService CreateService(string responseContent, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(responseContent)
            });

        var client = new HttpClient(handlerMock.Object);
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(c => c["WeatherApi:BaseUrl"]).Returns("http://fakeapi.com");

        return new WeatherService(client, configurationMock.Object);
    }

    [Fact]
    public async Task GetWeatherAsync_ReturnsContent_WhenSuccessful()
    {
        var service = CreateService("{\"location\":{\"name\":\"São Paulo\"},\"current\":{\"temp_c\":25}}");

        var result = await service.GetWeatherAsync("São Paulo", DateTime.UtcNow, "fakekey", "pt");

        Assert.Contains("São Paulo", result);
    }

    [Fact]
    public async Task GetWeatherAsync_ThrowsInvalidOperation_WhenNotFound()
    {
        var service = CreateService("", HttpStatusCode.NotFound);

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await service.GetWeatherAsync("CidadeInexistente", DateTime.UtcNow, "fakekey", "pt")
        );
    }

    [Fact]
    public void JsonFormatted_ReturnsWeatherResponse_WhenValidJson()
    {
        var service = CreateService("");

        var json = "{\"location\":{\"name\":\"São Paulo\"},\"current\":{\"temp_c\":25}}";
        var weather = service.JsonFormatted(json);

        Assert.Equal("São Paulo", weather.location.name);
        Assert.Equal(25, weather.Current.TempC);
    }
}
