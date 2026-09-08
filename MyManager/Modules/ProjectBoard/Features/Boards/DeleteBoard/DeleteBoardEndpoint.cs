using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;

using DeleteBoardResult = Results<BadRequest<ErrorDetails>, NoContent>;

public static class DeleteBoardEndpoint
{
    [MapDelete("/project-board/boards/{id:int}")]
    [WithName("DeleteBoard")]
    [WithSummary("Deletes an existing board")]
    [WithTags("Boards")]
    public static async Task<DeleteBoardResult> HandleEndpoint(
        [AsParameters] DeleteBoardRequest request,
        [AsParameters] DeleteBoardServices services,
        CancellationToken ct = default
    )
    {
        var command = new DeleteBoardCommand(request.Id);
        var result = await services.Handler.HandleCommand(command, ct);

        return result.Fold<DeleteBoardResult>(
            errors => errors
                .Pipe(ErrorDetails.GlobalErrorOnly)
                .Pipe(TypedResults.BadRequest),
            success => success
                .Tap(s => DeleteBoardLogs.LogDeleted(services.Logger, s.Id, s.Slug))
                .Pipe(_ => TypedResults.NoContent())
        );
    }
}
