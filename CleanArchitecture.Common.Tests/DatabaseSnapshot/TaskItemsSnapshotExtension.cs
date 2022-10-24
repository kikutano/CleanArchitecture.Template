using CleanArchitecture.Domain.ProjectAggregates.Entities;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Common.Tests.DatabaseSnapshot;
public static class TaskItemsSnapshotExtension {
    public static DbSnapshotFactory AddTaskItem(
        this DbSnapshotFactory snapshot,
        out TaskItem taskItem,
        Guid projectId,
        string name = "task item",
        string description = "amazing description" ) {

        taskItem = snapshot.DbContext
            .TaskItems
            .Add(
                TaskItem.Create(
                    projectId,
                    LimitedText.Create( name ).Value,
                    DescriptionText.Create( description ).Value )
                .Value )
            .Entity;

        snapshot.DbContext.SaveChanges();
        return snapshot;
    }

    public static DbSnapshotFactory AddTaskItem(
       this DbSnapshotFactory snapshot,
       Guid projectId,
       string name = "task item",
       string description = "amazing description" ) {

        TaskItem taskItem = null;
        AddTaskItem( snapshot, out taskItem, projectId, name, description );
        
        return snapshot;
    }
}
