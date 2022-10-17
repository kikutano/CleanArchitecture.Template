using CleanArchitecture.Infrastructure.Persistence;

namespace CleanArchitecture.Common.Tests.DatabaseSnapshot;
public class DbSnapshotFactory {
    public CleanArchitectureDbContext DbContext { get; private set; }

    public DbSnapshotFactory( CleanArchitectureDbContext context ) {
        DbContext = context;
    }
}
