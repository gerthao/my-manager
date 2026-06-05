using MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;
using MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;
using MyManager.Modules.ProjectBoard.Features.Boards.ListBoards;
using MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;
using MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;
using MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

namespace MyManager.Modules.ProjectBoard.Features.Boards;

public static class BoardEndpoints
{
    public static IEndpointRouteBuilder MapBoardEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/boards")
            .WithTags("Boards");

        group.MapGet("", ListBoardsEndpoint.HandleEndpoint)
            .WithName(nameof(ListBoardsEndpoint))
            .WithDisplayName("List boards");

        group.MapPost("", CreateBoardEndpoint.HandleEndpoint)
            .WithName(nameof(CreateBoardEndpoint))
            .WithDisplayName("Create a board");

        group.MapGet("/{id:int}", GetBoardByIdEndpoint.HandleEndpoint)
            .WithName(nameof(GetBoardByIdEndpoint))
            .WithDisplayName("Get a board by id");

        group.MapGet("/{slug}-{id:int}", GetBoardBySlugEndpoint.HandleEndpoint)
            .WithName(nameof(GetBoardBySlugEndpoint))
            .WithDisplayName("Get a board by slug");

        group.MapPut("/{id}", UpdateBoardEndpoint.HandleEndpoint)
            .WithName(nameof(UpdateBoardEndpoint))
            .WithDisplayName("Update a board");

        group.MapDelete("/{id}", DeleteBoardEndpoint.HandleEndpoint)
            .WithName(nameof(DeleteBoardEndpoint))
            .WithDisplayName("Delete a board");

        return endpoints;
    }
}
