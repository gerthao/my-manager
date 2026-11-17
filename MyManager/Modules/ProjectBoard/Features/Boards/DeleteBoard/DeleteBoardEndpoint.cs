using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;

using DeleteBoardResult = Results<NotFound<ProblemDetails>, NoContent>;

public static class DeleteBoardEndpoint
{
    [MapDelete("/api/project-board/boards/{boardId:int}")]
    [WithName("DeleteBoard")]
    [WithSummary("Deletes an existing board")]
    [WithTags("Boards")]
    public static async Task<DeleteBoardResult> HandleEndpoint(
        [AsParameters] DeleteBoardRequest request,
        [AsParameters] DeleteBoardServices services,
        CancellationToken ct = default
    ) =>
    await services.Handler.Handle(new DeleteBoardCommand(request.BoardId), ct) switch
    {
        None => new ProblemDetails()
        {
            Type = "Board cannot be deleted",
            Detail = $"No board to be deleted.",
        }.Pipe(TypedResults.NotFound),
        Some<DeleteBoardResponse>(var cmd) => TypedResults.NoContent()
    };
}
