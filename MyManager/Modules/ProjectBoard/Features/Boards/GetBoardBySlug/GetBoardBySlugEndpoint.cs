using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

using GetBoardBySlugResult = Results<NotFound<ProblemDetails>, Ok<GetBoardBySlugResponse>>;

public static class GetBoardBySlugEndpoint
{
    [MapGet("/api/project-board/boards/{boardId:int}-{slug}")]
    [WithName("GetBoardBySlug")]
    [WithSummary("Gets a board by slug and ID")]
    [WithTags("Boards")]
    public static async Task<GetBoardBySlugResult> HandleEndpoint(
        [AsParameters] GetBoardBySlugRequest request,
        [AsParameters] GetBoardBySlugServices services,
        CancellationToken ct = default
    ) => await services.Handler.Handle(new GetBoardBySlugQuery(request.BoardId, request.Slug), ct) switch
    {
        None => new ProblemDetails
        {
            Title = "Board not found",
            Detail = $"Could not find board with slug {request.Slug}",
            Instance = $"/api/project-board/boards/{request.Slug}-{request.BoardId}",
            Extensions = {
                    ["traceId"] = Activity.Current?.Id,
                },
        }.Pipe(TypedResults.NotFound),
        Some<GetBoardBySlugResponse>(var r) => r.Pipe(TypedResults.Ok)
    };
}
