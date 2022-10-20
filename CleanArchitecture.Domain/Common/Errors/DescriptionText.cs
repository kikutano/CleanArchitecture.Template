using ErrorOr;

namespace CleanArchitecture.Domain.Common.Errors;
public static partial class Errors {
    public class DescriptionText {
        public static Error InvalidDescription => Error.Validation(
            code: "DescriptionText.InvalidDescription",
            description: $"The text must be at least {ValueObjects.DescriptionText.MinLength} " +
            $"characters long and most {ValueObjects.DescriptionText.MaxLength}" );
    }
}
