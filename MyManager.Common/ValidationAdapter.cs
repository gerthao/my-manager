using System.ComponentModel.DataAnnotations;

namespace MyManager.Common;

public static class ValidationAdapter
{
    public static Func<T, Validated<Error, T>> CreateValidator<T>(
        string fieldName,
        params ValidationAttribute[] attributes
    ) where T : notnull
    {
        return value => ValidateWithAttributes(value, fieldName, attributes);
    }

    public static Validated<Error, T> ValidateWithAttributes<T>(
        T value,
        string fieldName,
        params ValidationAttribute[] attributes
    ) where T : notnull
    {
        List<Error> errors = [];

        foreach (var attribute in attributes)
            if (!attribute.IsValid(value))
            {
                var message = attribute.FormatErrorMessage(fieldName);
                errors.Add(new Error(fieldName, message));
            }

        if (errors.Count > 0) return new NonEmptyList<Error>(errors[0], errors.Skip(1).ToArray());

        return new Valid<Error, T>(value);
    }
}
