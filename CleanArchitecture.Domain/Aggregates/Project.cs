using CleanArchitecture.Domain.Common.Errors;
using CleanArchitecture.Domain.Common.Models;
using CleanArchitecture.Domain.ValueObjects;
using ErrorOr;

namespace CleanArchitecture.Domain.Aggregates;
public class Project : BaseEntity, IAggregateRoot {
    public LimitedText Name { get; private set; }

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

    private static List<Error> Validate( Project project ) {
        var errors = new List<Error>();

        if ( project.Name is null )
            errors.Add( Errors.Generic.CanNotBeNull( "project.Name" ) );

        return errors;
    }
}
