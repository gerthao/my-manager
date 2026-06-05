using System.Collections;

namespace MyManager.Common;

public sealed record ErrorDetails(
    IEnumerable<ValidationError>? FieldErrors = null,
    Error? GlobalError = null
)
{
    public static readonly ErrorDetails Empty = new();
    public static ErrorDetails GlobalErrorOnly(Error error) => new() { GlobalError = error };
    public static ErrorDetails FieldErrorOnly(IEnumerable<ValidationError> errors) => new() { FieldErrors = errors };
    public static ErrorDetails FieldErrorOnly(params ValidationError[] errors) => new() { FieldErrors = errors };
}
