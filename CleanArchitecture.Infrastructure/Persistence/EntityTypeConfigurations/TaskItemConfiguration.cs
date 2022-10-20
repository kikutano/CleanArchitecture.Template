using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Domain.ProjectAggregates.Entities;

namespace CleanArchitecture.Infrastructure.Persistence.EntityTypeConfigurations;
internal class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem> {
    public void Configure( EntityTypeBuilder<TaskItem> builder ) {
        builder.HasKey( x => x.Id );

        builder.Property( p => p.Title )
            .HasConversion(
                p => p.Value,
                p => LimitedText.Create( p ).Value );
        builder.Property( p => p.Description )
            .HasConversion(
                p => p.Value,
                p => DescriptionText.Create( p ).Value );
    }
}