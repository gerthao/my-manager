using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;

using DeleteBoardResult = Results<BadRequest<ErrorDetails>, NoContent>;

public abstract class DeleteBoardEndpoint
{
    // public static void MapEndpoint(IEndpointRouteBuilder app) =>
    //     app.MapDelete("/boards/{id:int}", HandleEndpoint);

    [MapDelete("/project-board/boards/{id:int}")]
    public static async Task<DeleteBoardResult> HandleEndpoint(
        [AsParameters] DeleteBoardRequest request,
        IDeleteBoardHandler handler,
        ILogger<ProjectBoardModule> logger,
        CancellationToken ct = default
    )
    {
        var command = new DeleteBoardCommand(request.Id);
        var result = await handler.HandleCommand(command, ct);

        return result.Fold<DeleteBoardResult>(
            errors =>
                errors
                    .Pipe(ErrorDetails.GlobalErrorOnly)
                    .Pipe(TypedResults.BadRequest),
            success =>
                success
                    .Tap(s => DeleteBoardLogs.LogDeleted(logger, s.Id, s.Slug))
                    .Pipe(_ => TypedResults.NoContent())
        );
    }
}
