using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.ListBoards;

using ListBoardsResult = Results<BadRequest<ErrorDetails>, Ok<List<ListBoardsResponse>>>;

public static class ListBoardsEndpoint
{
    [MapGet("/project-board/boards")]
    [WithName("ListBoards")]
    [WithSummary("List boards")]
    [WithTags("Boards")]
    public static async Task<ListBoardsResult> HandleEndpoint(
        [AsParameters] ListBoardsQuery query,
        [AsParameters] ListBoardServices services,
        CancellationToken ct = default)
    {
        var result = await services.Handler.Handle(query, ct);

        return result.Fold<ListBoardsResult>(
            error => error.Pipe(ErrorDetails.GlobalErrorOnly).Pipe(TypedResults.BadRequest),
            boards => TypedResults.Ok(boards)
        );
    }
}
