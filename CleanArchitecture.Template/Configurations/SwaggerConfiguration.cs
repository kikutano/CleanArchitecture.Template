using Microsoft.OpenApi.Models;

namespace CleanArchitecture.WebApi.Configurations;

public static class SwaggerConfiguration {
    public static IServiceCollection AddSwaggerConfiguration(
        this IServiceCollection services, IConfiguration configuration ) {
        services.AddSwaggerGen( swaggerGenOptions => {
            swaggerGenOptions.SwaggerDoc( "v1", new OpenApiInfo {
                Title = configuration["Swagger:ApiName"],
                Version = configuration["Swagger:ApiVer"]
            } );
        } );

        return services;
    }
}
