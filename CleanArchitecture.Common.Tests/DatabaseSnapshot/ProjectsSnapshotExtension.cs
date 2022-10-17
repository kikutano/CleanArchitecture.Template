using CleanArchitecture.Domain.Aggregates;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Common.Tests.DatabaseSnapshot;
public static class ProjectsSnapshotExtension {
    public static DbSnapshotFactory AddProject(
        this DbSnapshotFactory snapshot,
        out Project project,
        string name = "new project" ) {

        project = snapshot.DbContext
            .Add(
                Project.Create(
                    LimitedText.Create( name ).Value )
                .Value )
            .Entity;

        snapshot.DbContext.SaveChanges();
        return snapshot;
    }
}
