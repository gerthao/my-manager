using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;

using GetBoardByIdResult = Results<NotFound<ErrorDetails>, RedirectToRouteHttpResult>;

public static class GetBoardByIdEndpoint
{
    [MapGet("/project-board/boards/{id:int}")]
    [WithName("GetBoardById")]
    [WithSummary("Gets an board by ID")]
    [WithTags("Boards")]
    public static async Task<GetBoardByIdResult> HandleEndpoint(
        [AsParameters] GetBoardByIdRequest request,
        [AsParameters] GetBoardByIdServices services,
        CancellationToken ct = default)
    {
        var query = new GetBoardByIdQuery(request.Id);
        var slug = await services.Handler.Handle(query, ct);

        return slug.Fold<GetBoardByIdResult>(
            error => error.Pipe(ErrorDetails.GlobalErrorOnly).Pipe(TypedResults.NotFound),
            success => TypedResults.RedirectToRoute(
                nameof(GetBoardBySlugEndpoint),
                new { id = request.Id, slug = success }
            )
        );
    }
}
