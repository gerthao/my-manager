using Microsoft.EntityFrameworkCore;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard;

public sealed class ProjectBoardModule : IModule
{
    public const string Prefix = "/project-board";

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
    }

    public void RegisterServices(WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddDbContext<ProjectBoardDbContext>(options =>
            options.UseInMemoryDatabase("ProjectBoardDb")
        );
    }

    public void UseMiddleware(IApplicationBuilder app)
    {
    }
}
