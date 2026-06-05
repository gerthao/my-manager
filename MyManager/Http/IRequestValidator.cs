using MyManager.Common;

namespace MyManager.Http;

public interface IRequestValidator<T> where T : notnull
{
    public static abstract Validated<ValidationError, T> Validate(HttpRequest request);
}
