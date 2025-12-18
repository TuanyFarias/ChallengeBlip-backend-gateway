using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Xunit;
using Moq;

public class ApiHeaderMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_Returns403_WhenApiKeyNotConfigured()
    {
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(c => c["WeatherApi:ApiKey"]).Returns("");

        var middleware = new ApiHeaderMiddleware((innerHttpContext) =>
        {
            return Task.CompletedTask;
        }, configurationMock.Object);

        var context = new DefaultHttpContext();
        await middleware.InvokeAsync(context);

        Assert.Equal(403, context.Response.StatusCode);
    }

    [Fact]
    public async Task InvokeAsync_AddsWeatherApiKeyToContext()
    {
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(c => c["WeatherApi:ApiKey"]).Returns("mykey");

        var middleware = new ApiHeaderMiddleware((innerHttpContext) =>
        {
            Assert.Equal("mykey", innerHttpContext.Items["WeatherApiKey"]);
            return Task.CompletedTask;
        }, configurationMock.Object);

        var context = new DefaultHttpContext();
        await middleware.InvokeAsync(context);
    }
}
