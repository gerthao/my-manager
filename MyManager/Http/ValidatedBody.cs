using MyManager.Common;

namespace MyManager.Http;

public sealed class ValidatedBody<TE, TA>
    where TE: notnull
    where TA: notnull
{
    public Validated<TE, TA> Value { get; }

    private ValidatedBody(Validated<TE, TA> value)
    {
        Value = value;
    }

    public static async ValueTask<ValidatedBody<TE, TA>> BindAsync(HttpContext context)
    {
        var binder = context.RequestServices.GetRequiredService<IRequestBinder<TE, TA>>();
        var value  = await binder.BindAsync(context.Request);
        return new ValidatedBody<TE, TA>(value);
    }
}
