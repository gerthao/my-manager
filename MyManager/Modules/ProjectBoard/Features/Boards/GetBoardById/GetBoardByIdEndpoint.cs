using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;

using GetBoardByIdResult = Results<NotFound<ProblemDetails>, Ok<GetBoardByIdResponse>>;

public static class GetBoardByIdEndpoint
{
    [MapGet("/api/project-board/boards/{boardId:int}")]
    [WithName("GetBoardById")]
    [WithSummary("Get a board by ID")]
    [WithTags("Boards")]
    public static async Task<GetBoardByIdResult> HandleEndpoint(
        [AsParameters] GetBoardByIdRequest request,
        [AsParameters] GetBoardByIdServices services,
        CancellationToken ct = default) =>
    await services.Handler.Handle(new GetBoardByIdQuery(request.BoardId), ct) switch
    {
        None => new ProblemDetails()
        {
            Title = "Board not found",
            Detail = $"Could not find board with id {request.BoardId}",
            Instance = $"/api/project-board/boards/{request.BoardId}",
            Extensions = {
                ["traceId"] = Activity.Current?.Id
            }
        }.Pipe(TypedResults.NotFound),
        Some<GetBoardByIdResponse>(var response) => response.Pipe(TypedResults.Ok),
    };
}
