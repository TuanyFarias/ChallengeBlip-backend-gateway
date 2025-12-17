using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ChallengeBlip.Controllers;
using ChallengeBlip.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar HttpClient e Controllers
builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddControllers();

var app = builder.Build();

// Middleware
app.UseMiddleware<ApiHeaderMiddleware>();

// Mapear Controllers
app.MapControllers();

app.Run();
