namespace MyManager.Http;

public interface IModule
{
    void RegisterServices(WebApplicationBuilder builder);
    void UseMiddleware(IApplicationBuilder app);
    void MapEndpoints(IEndpointRouteBuilder app);
}
