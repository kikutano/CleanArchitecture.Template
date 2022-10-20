using CleanArchitecture.Domain.Common.Models;
using CleanArchitecture.Domain.ProjectAggregates.Entities;
using CleanArchitecture.Domain.ValueObjects;
using ErrorOr;

namespace CleanArchitecture.Domain.ProjectAggregates;
public class Project : BaseEntity, IAggregateRoot {
    public LimitedText Name { get; private set; }

    public virtual IEnumerable<TaskItem> TaskItems
        => _taskItems.AsReadOnly();
    private readonly List<TaskItem> _taskItems = new();

    protected Project( LimitedText name, Guid id ) : base( id ) {
        Name = name;
    }

    public static ErrorOr<Project> Create( LimitedText name, Guid? id = null ) {
        var project = new Project(
            name,
            id ?? Guid.NewGuid() );

        var errors = Validate( project );

        if ( errors.Any() )
            return errors;

        return project;
    }

    public List<Error> AddTask( TaskItem taskItem ) {
        _taskItems.Add( taskItem );
        return new List<Error>();
    }

    public TaskItem? GetTask( Guid taskId ) {
        return _taskItems.FirstOrDefault( x => x.Id == taskId );
    }

    private static List<Error> Validate( Project project ) {
        var errors = new List<Error>();

        if ( project.Name is null )
            errors.Add( Common.Errors.Errors.Generic.CanNotBeNull( "project.Name" ) );

        return errors;
    }
}
