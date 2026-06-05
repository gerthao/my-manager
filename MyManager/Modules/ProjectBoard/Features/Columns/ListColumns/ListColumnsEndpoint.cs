using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Columns.ListColumns;

using ListColumnsResult = Results<BadRequest<ErrorDetails>, Ok<List<ListColumnsResponse>>>;

public abstract class ListColumnsEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/boards/{boardId:int}/columns", HandleEndpoint);
    }

    private static async Task<ListColumnsResult> HandleEndpoint(
        [AsParameters] ListColumnsRequest request,
        IListColumnsHandler handler,
        CancellationToken ct = default
    )
    {
        var query = new ListColumnsQuery(request.BoardId);
        var columnsResult = await handler.HandleQuery(query, ct);

        return columnsResult.Fold<ListColumnsResult>(
            error => error
                .Pipe(ErrorDetails.GlobalErrorOnly)
                .Pipe(TypedResults.BadRequest),
            columns =>
                columns.Pipe(TypedResults.Ok)
        );
    }
}
