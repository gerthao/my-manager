using Microsoft.EntityFrameworkCore;
using MyManager.Http;
using MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;
using MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;
using MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;
using MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;
using MyManager.Modules.ProjectBoard.Features.Boards.ListBoards;
using MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

namespace MyManager.Modules.ProjectBoard;

public sealed class ProjectBoardModule : IModule
{
    public const string Prefix = "/project-board";

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(Prefix);

        ListBoardsEndpoint.MapEndpoint(group);
        CreateBoardEndpoint.MapEndpoint(group);
        GetBoardBySlugEndpoint.MapEndpoint(group);
        GetBoardByIdEndpoint.MapEndpoint(group);
        UpdateBoardEndpoint.MapEndpoint(group);
        DeleteBoardEndpoint.MapEndpoint(group);
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
