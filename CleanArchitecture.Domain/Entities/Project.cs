using CleanArchitecture.Domain.Common.Models;
using ErrorOr;

namespace CleanArchitecture.Domain.Entities;
public class Project : IAggregateRoot {
    public string Name { get; private set; }

    protected Project( string name ) {
        Name = name;
    }

    public static ErrorOr<Project> Create( string name ) {
        return new Project( name );
    }
}
