using CleanArchitecture.Domain.Common.Errors;
using CleanArchitecture.Domain.Common.Models;
using ErrorOr;

namespace CleanArchitecture.Domain.ValueObjects;
public class DescriptionText : IValueObject {
    public string Value { get; }

    public const int MinLength = 0;
    public const int MaxLength = 6000;

    protected DescriptionText( string value ) {
        Value = value;
    }

    public static ErrorOr<DescriptionText> Create( string value ) {
        var description = new DescriptionText( value );
        var errors = Validate( description );

        if ( errors.Any() )
            return errors;

        return description;
    }

    public static List<Error> Validate( DescriptionText descriptionText ) {
        var errors = new List<Error>();
        if ( descriptionText is null ) {
            errors.Add( Errors.DescriptionText.InvalidDescription );
        }

        if ( descriptionText!.Value.Length is < MinLength or > MaxLength ) {
            return new List<Error>() { Errors.DescriptionText.InvalidDescription };
        }

        return errors;
    }

    public List<Error> Validate() {
        return Validate( this );
    }

    public override string ToString() {
        return Value;
    }
}
