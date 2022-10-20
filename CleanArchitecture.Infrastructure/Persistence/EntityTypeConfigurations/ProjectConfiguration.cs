using CleanArchitecture.Domain.ProjectAggregates;
using CleanArchitecture.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.EntityTypeConfigurations;
internal class ProjectConfiguration : IEntityTypeConfiguration<Project> {
    public void Configure( EntityTypeBuilder<Project> builder ) {
        builder.HasKey( x => x.Id );

        builder.Property( p => p.Name )
            .HasConversion(
                p => p.Value,
                p => LimitedText.Create( p ).Value );
    }
}
