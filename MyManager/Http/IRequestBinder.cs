using MyManager.Common;

namespace MyManager.Http;

public interface IRequestBinder<TE, TA>
where TE: notnull
where TA: notnull
{
    Task<Validated<TE, TA>> BindAsync(HttpRequest request);
}
