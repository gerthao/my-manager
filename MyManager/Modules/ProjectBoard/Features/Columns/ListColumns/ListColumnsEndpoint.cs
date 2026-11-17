using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Columns.ListColumns;

using ListColumnsResult = Ok<List<ListColumnsResponse>>;

public static class ListColumnsEndpoint
{
    [MapGet("/api/project-board/boards/{boardId:int}/columns")]
    [WithName("ListColumns")]
    [WithTags("Columns")]
    [WithSummary("List columns for a board")]
    public static async Task<ListColumnsResult> HandleEndpoint(
        [AsParameters] ListColumnsRequest request,
        IRequestHandler<ListColumnsQuery, List<ListColumnsResponse>> handler,
        CancellationToken ct = default
    )
    {
        var query = new ListColumnsQuery(request.BoardId);
        var columnsResult = await handler.Handle(query, ct);

        return TypedResults.Ok(columnsResult);
    }
}
