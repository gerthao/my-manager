using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;
using MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;

using GetBoardByIdResult = Results<NotFound<ErrorDetails>, RedirectToRouteHttpResult>;

public abstract class GetBoardByIdEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/boards/{id:int}", HandleEndpoint)
            .WithName(nameof(GetBoardByIdEndpoint));

    public static async Task<GetBoardByIdResult> HandleEndpoint(
        [AsParameters] GetBoardByIdRequest request,
        IGetBoardByIdHandler handler,
        CancellationToken ct = default)
    {
        var query = new GetBoardByIdQuery(request.Id);
        var slug = await handler.HandleQuery(query, ct);

        return slug.Fold<GetBoardByIdResult>(
            error => error.Pipe(ErrorDetails.GlobalErrorOnly).Pipe(TypedResults.NotFound),
            success => TypedResults.RedirectToRoute(
                nameof(GetBoardBySlugEndpoint),
                new { id = request.Id, slug = success }
            )
        );
    }
}
