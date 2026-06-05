namespace MyManager.Http;

public interface IEndpointHandler<in TRequest, TResponse>
{
    static abstract Task<TResponse> Handle(TRequest request, CancellationToken ct = default);
}

public interface IEndpointHandler<TResponse>
{
    static abstract Task<TResponse> Handle(CancellationToken ct = default);
}

public interface Ep
{
    public interface Req<TRequest>
    {
        public interface Res<TResponse> : IEndpointHandler<TRequest, TResponse>;
    }

    public interface Res<TResponse> : IEndpointHandler<TResponse>;
}
