using ErrorOr;

namespace CleanArchitecture.Domain.Common.Errors;
public static partial class Errors {
    public class Generic {
        public static Error CanNotBeNull( string value ) => Error.Validation(
            code: "Generic.CanNotBeNull",
            description: $"The object {value} can not be null!" );
    }
}

