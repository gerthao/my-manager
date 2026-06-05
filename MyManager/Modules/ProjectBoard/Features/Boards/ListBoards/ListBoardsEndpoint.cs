using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.ListBoards;

using ListBoardsResult = Results<BadRequest<ErrorDetails>, Ok<List<ListBoardsResponse>>>;

public abstract class ListBoardsEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
        => app.MapGet("/boards", HandleEndpoint);

    public static async Task<ListBoardsResult> HandleEndpoint(
        [FromServices] IListBoardsHandler handler,
        CancellationToken ct = default)
    {
        var result = await handler.HandleQuery(ct);

        return result.Fold<ListBoardsResult>(
            error => error.Pipe(ErrorDetails.GlobalErrorOnly).Pipe(TypedResults.BadRequest),
            boards => TypedResults.Ok(boards)
        );
    }
}
