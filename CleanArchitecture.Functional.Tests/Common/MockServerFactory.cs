using CleanArchitecture.Common.Tests.DatabaseSnapshot;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CleanArchitecture.Functional.Tests.Common;
internal static class MockServerFactory {
    public static DbSnapshotFactory DbSnapshoter { get; private set; }

    public static WebApplicationFactory<Program> GetFakeServer() {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder( builder => {
                builder.ConfigureServices( services => {
                    var dbContext = services.SingleOrDefault(
                        d => d.ServiceType == typeof( DbContextOptions<CleanArchitectureDbContext> ) );

                    if ( dbContext != null )
                        services.Remove( dbContext );

                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    var dbName = Guid.NewGuid().ToString().Replace( "-", "" );

                    services.AddDbContext<CleanArchitectureDbContext>( options => {
                        options.UseInMemoryDatabase( dbName );
                        options.UseInternalServiceProvider( serviceProvider );
                    } );
                    var sp = services.BuildServiceProvider();

                    DbSnapshoter = new DbSnapshotFactory( 
                        sp.GetService<CleanArchitectureDbContext>()! );

                    using ( var scope = sp.CreateScope() ) {
                        using ( var appContext = scope.ServiceProvider
                            .GetRequiredService<CleanArchitectureDbContext>() ) {
                            appContext.Database.EnsureCreated();
                        }
                    }
                } );
            } );

        application.CreateClient();
        return application;
    }
}
