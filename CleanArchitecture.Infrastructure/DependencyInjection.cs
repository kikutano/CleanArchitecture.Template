using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;
public static class DependencyInjection {
    private const bool _useInMemoryDatabase = true;

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration ) {
        ConfigureRepositories( services );
        ConfigureDatabase( services, configuration );
        return services;
    }

    private static void ConfigureDatabase( IServiceCollection services, IConfiguration configuration ) {
        if ( _useInMemoryDatabase ) {
            ConfigureAsInMemoryDatabase( services );
        }
        else {
            ConfigureAsSqlServerDatabase( services, configuration );
        }
    }

    private static void ConfigureRepositories( IServiceCollection services ) {
        services.AddScoped<IProjectRepository, ProjectRepository>();
    }

    private static void ConfigureAsInMemoryDatabase( IServiceCollection services ) {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        services.AddDbContext<CleanArchitectureDbContext>( options => {
            options.UseInMemoryDatabase( "db" );
            options.UseInternalServiceProvider( serviceProvider );
        } );
    }

    private static void ConfigureAsSqlServerDatabase( 
        IServiceCollection services, IConfiguration configuration ) {
        services.AddDbContext<CleanArchitectureDbContext>(
            options => options.UseSqlServer(
                configuration.GetConnectionString( "DefaultConnection" ) )
            .UseLazyLoadingProxies() );
    }
}
