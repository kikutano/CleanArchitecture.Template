using ErrorOr;

namespace CleanArchitecture.Domain.ProjectAggregates.Errors;
public static partial class Errors {
    public class Project {
        public static Error NotFound( Guid id ) => Error.Validation(
            code: "Project.NotFound",
            description: $"The project with id {id} can not be found!" );
    }
}
