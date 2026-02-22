using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using ChallengeBlip.Models;

namespace WeatherGateway.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {   
                c.MapType<WeatherResponse>(() => new OpenApiSchema
                    {
                        Example = new OpenApiObject
                        {
                            ["location"] = new OpenApiObject
                            {
                                ["name"] = new OpenApiString("São Paulo")
                            },
                            ["current"] = new OpenApiObject
                            {
                                ["temp_c"] = new OpenApiDouble(23),
                                ["feelslike_c"] = new OpenApiDouble(24),
                                ["humidity"] = new OpenApiInteger(60),
                                ["cloud"] = new OpenApiInteger(40),
                                ["is_day"] = new OpenApiInteger(1),
                                ["last_updated"] = new OpenApiString("2025-01-01 12:00"),
                                ["condition"] = new OpenApiObject
                                {
                                    ["text"] = new OpenApiString("Partly cloudy"),
                                    ["icon"] = new OpenApiString("//cdn.weatherapi.com/weather/64x64/day/116.png"),
                                    ["code"] = new OpenApiInteger(116)
                                }
                            }
                        }
                    });

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clima API", Version = "v1" });
                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "Insira sua API key",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "ApiKeyScheme"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        new string[] { }
                    }
                });
            });
                
                        return services;
                    }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherAPIGateway");
                c.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}
