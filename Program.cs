using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ChallengeBlip.Services;
using WeatherGateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Builds
builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddFrontendCors();


var app = builder.Build();

// Use app
app.UseSwaggerDocumentation();
app.UseCors("Frontend");
app.UseMiddleware<ApiHeaderMiddleware>();

// Mapear Controllers
app.MapControllers();

app.Run();
