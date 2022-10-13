using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;
public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration ) {
        ConfigureRepositories( services );
        ConfigureDatabase( services, configuration );
        return services;
    }

    private static void ConfigureDatabase( IServiceCollection services, IConfiguration configuration ) {
        services.AddDbContext<CleanArchitectureDbContext>(
            options => options.UseSqlServer(
                configuration.GetConnectionString( "DefaultConnection" ) )
            .UseLazyLoadingProxies() );
    }

    private static void ConfigureRepositories( IServiceCollection services ) {
        services.AddScoped<IProjectRepository, ProjectRepository>();
    }
}
