namespace MyManager.Http;

public interface IRequestHandler<in TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken ct = default);
}

public interface IRequestHandler<in TRequest>
{
    Task Handle(TRequest request, CancellationToken ct = default);
}
