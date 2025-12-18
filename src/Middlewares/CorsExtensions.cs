using Microsoft.Extensions.DependencyInjection;

public static class CorsExtensions
{
    public static IServiceCollection AddFrontendCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("Frontend",
                policy => policy
                    .WithOrigins("https://challenge-blip-frontend-gateway.vercel.app")
                    .WithMethods("GET")
                    .AllowAnyHeader());
        });

        return services;
    }
}
