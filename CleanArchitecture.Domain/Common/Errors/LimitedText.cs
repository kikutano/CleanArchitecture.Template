using ErrorOr;

namespace CleanArchitecture.Domain.Common.Errors;

public static partial class Errors {
    public class LimitedText {
        public static Error InvalidLongText => Error.Validation(
            code: "LimitedText.InvalidLongText",
            description: $"The text must be at least {ValueObjects.LimitedText.MinLength} " +
            $"characters long and most {ValueObjects.LimitedText.MaxLength}" );
    }
}
