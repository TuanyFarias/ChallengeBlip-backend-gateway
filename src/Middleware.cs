using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;

public class ApiHeaderMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _apiKey;

    public ApiHeaderMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _apiKey = configuration["WeatherApi:ApiKey"] ?? "";
    } 

   public async Task InvokeAsync(HttpContext context)
     {
        try
        {
            if (context.Request.Method != "GET" || string.IsNullOrEmpty(_apiKey))
            {
                throw new UnauthorizedAccessException();
            }

            context.Items["WeatherApiKey"] = _apiKey;

            await _next(context);
        }

        catch (Exception)
        {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Acesso Negado!");
            }
        }

}