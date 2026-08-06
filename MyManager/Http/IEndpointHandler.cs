namespace MyManager.Http;

public interface IServices;

public interface IEndpointHandler<in TRequest, in TServices, TResponse> where TServices : IServices
{
    static abstract Task<TResponse> HandleEndpoint(TRequest request, TServices services, CancellationToken ct = default);
}

public interface IEndpointHandler<in TServices, TResponse> where TServices : IServices
{
    static abstract Task<TResponse> HandleEndpoint(TServices services, CancellationToken ct = default);
}


public interface IEndpointHandler<TResponse>
{
    static abstract Task<TResponse> HandleEndpoint(CancellationToken ct = default);
}

public interface IEndpointHandler
{
    static abstract Task HandleEndpoint(CancellationToken ct = default);
}
