using CleanArchitecture.Domain.Common.Errors;
using CleanArchitecture.Domain.Common.Models;
using ErrorOr;

namespace CleanArchitecture.Domain.ValueObjects;
public class LimitedText : IValueObject {
    public string Value { get; }

    public const int MinLength = 3;
    public const int MaxLength = 50;

    protected LimitedText( string value ) {
        Value = value;
    }

    public static ErrorOr<LimitedText> Create( string value ) {
        var plainText = new LimitedText( value );
        var errors = Validate( plainText );

        if ( errors.Any() )
            return errors;

        return plainText;
    }

    public static List<Error> Validate( LimitedText limitedText ) {
        var errors = new List<Error>();
        if ( limitedText is null ) {
            errors.Add( Errors.LimitedText.InvalidLongText );
        }

        if ( limitedText!.Value.Length is < MinLength or > MaxLength ) {
            return new List<Error>() { Errors.LimitedText.InvalidLongText };
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
