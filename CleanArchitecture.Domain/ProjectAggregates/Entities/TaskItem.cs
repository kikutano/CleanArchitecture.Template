using CleanArchitecture.Domain.Common.Models;
using CleanArchitecture.Domain.ValueObjects;
using ErrorOr;

namespace CleanArchitecture.Domain.ProjectAggregates.Entities;
public class TaskItem : BaseEntity {
    public LimitedText Title { get; private set; }
    public DescriptionText? Description { get; private set; }
    public TaskState State { get; private set; } = TaskState.ToDo;
    public virtual Guid ProjectId { get; private set; }

    protected TaskItem(
        Guid id,
        Guid projectId,
        LimitedText title,
        DescriptionText? description ) : base( id ) {
        Title = title;
        Description = description;
        ProjectId = projectId;
    }

    public static ErrorOr<TaskItem> Create(
        Guid projectId,
        LimitedText title,
        DescriptionText? description,
        Guid? id = null ) {

        var taskItem = new TaskItem(
            id ?? Guid.NewGuid(),
            projectId,
            title,
            description );

        return taskItem;
    }

    public void SetState( TaskState state ) {
        State = state;
    }

    public List<Error> UpdateTitle( LimitedText title ) {
        if ( title is null )
            return new List<Error>() { 
                Common.Errors.Errors.Generic.CanNotBeNull( "title" ) 
            };

        Title = title;
        return new List<Error>();
    }

    public List<Error> UpdateDescription( DescriptionText description ) {
        Description = description;
        return new List<Error>();
    }
}
