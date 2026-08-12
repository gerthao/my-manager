using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Columns.GetColumnById;

using GetColumnByIdResult = Results<NotFound<ErrorDetails>, Ok<GetColumnByIdResponse>>;

public static class GetColumnByIdEndpoint
{
    [MapGet("/boards/{boardId:int}/columns/{columnId:int}")]
    [WithName("GetColumnById")]
    [WithTags("Columns")]
    [WithSummary("Get a column by its ID")]
    public static Task<GetColumnByIdResult> HandleEndpoint(
        [AsParameters] GetColumnByIdRequest request,
        [AsParameters] GetColumnByIdServices services,
        CancellationToken ct = default
    ) =>
        services.Handler.Handle(request).FoldAsync<GetColumnByIdResponse, GetColumnByIdResult>(
            error => error.Pipe(ErrorDetails.GlobalErrorOnly).Pipe(TypedResults.NotFound),
            response => response.Pipe(TypedResults.Ok)
        );
}
