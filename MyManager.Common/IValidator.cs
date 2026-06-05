namespace MyManager.Common;

public interface IValidator<in TSource, TResult>
    where TSource : notnull
    where TResult : notnull
{
    Validated<ValidationError, TResult> Validate(TSource request);
}
