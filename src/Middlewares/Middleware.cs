using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System;
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
            if (!context.Request.Headers.TryGetValue("Authorization", out var X_API_KEY))
            {
                throw new UnauthorizedAccessException();
            }

            if (X_API_KEY != _apiKey)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("API key inválida!");
                return;
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