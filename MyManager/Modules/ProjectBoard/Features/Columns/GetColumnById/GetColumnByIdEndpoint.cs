using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Modules.ProjectBoard.Domain.Errors;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Columns.GetColumnById;

using GetColumnByIdResult = Results<NotFound<ProblemDetails>, Ok<GetColumnByIdResponse>>;

public static class GetColumnByIdEndpoint
{
    [MapGet("/api/project-board/boards/{boardId:int}/columns/{columnId:int}")]
    [WithName("GetColumnById")]
    [WithTags("Columns")]
    [WithSummary("Get a column by its ID")]
    public static async Task<GetColumnByIdResult> HandleEndpoint(
        [AsParameters] GetColumnByIdRequest request,
        [AsParameters] GetColumnByIdServices services,
        CancellationToken ct = default
    ) =>
        await services.Handler.Handle(request, ct) switch
        {
            Left<ColumnNotFound>(var nf) => new ProblemDetails
            {
                Title = "Column not found",
                Detail = nf.Description,
                Instance = $"/api/project-board/boards/{request.BoardId}/columns/{request.ColumnId}",
                Extensions = new Dictionary<string, object?>
                {
                    ["traceId"] = Activity.Current?.Id,
                },
            }.Pipe(TypedResults.NotFound),
            Right<GetColumnByIdResponse> (var response) => response.Pipe(TypedResults.Ok),
        };
}
