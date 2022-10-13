using CleanArchitecture.Domain.Aggregates;
using CleanArchitecture.Infrastructure.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence;
public class CleanArchitectureDbContext : DbContext {
    public CleanArchitectureDbContext( DbContextOptions<CleanArchitectureDbContext> options )
        : base( options ) {
    }

    public DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating( ModelBuilder modelBuilder ) {
        base.OnModelCreating( modelBuilder );

        modelBuilder.ApplyConfiguration( new ProjectConfiguration() );
    }
}
