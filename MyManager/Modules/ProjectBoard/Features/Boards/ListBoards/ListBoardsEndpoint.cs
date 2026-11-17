using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.ListBoards;

using ListBoardsResult = Ok<List<ListBoardsResponse>>;

public static class ListBoardsEndpoint
{
    [MapGet("/api/project-board/boards")]
    [WithName("ListBoards")]
    [WithSummary("List boards")]
    [WithTags("Boards")]
    public static async Task<ListBoardsResult> HandleEndpoint(
        [AsParameters] ListBoardsRequest request,
        [AsParameters] ListBoardsServices services,
        CancellationToken ct = default) =>
        TypedResults.Ok(await services.Handler.Handle(new ListBoardsQuery(request.Name), ct));
}
