using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

using GetBoardBySlugResult = Results<NotFound<ErrorDetails>, Ok<GetBoardBySlugResponse>>;

public static class GetBoardBySlugEndpoint
{
    [MapGet("/project-board/boards/{slug}-{id:int}")]
    [WithName("GetBoardBySlug")]
    [WithDescription("Get board by slug")]
    [WithTags("Boards")]
    public static async Task<GetBoardBySlugResult> HandleEndpoint(
        [AsParameters] GetBoardBySlugRequest request,
        IGetBoardBySlugHandler handler,
        CancellationToken ct = default
    )
    {
        var query = new GetBoardBySlugQuery(request.Id, request.Slug);
        var result = await handler.HandleQuery(query, ct);

        return result.Fold<GetBoardBySlugResult>(
            error => error.Pipe(ErrorDetails.GlobalErrorOnly).Pipe(TypedResults.NotFound),
            success => success.Pipe(TypedResults.Ok)
        );
    }
}
