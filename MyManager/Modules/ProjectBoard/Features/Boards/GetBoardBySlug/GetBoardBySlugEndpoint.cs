using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

using GetBoardBySlugResult = Results<NotFound<ErrorDetails>, Ok<GetBoardBySlugResponse>>;

public abstract class GetBoardBySlugEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app
            .MapGet("/boards/{slug}-{id:int}", HandleEndpoint)
            .WithName(nameof(GetBoardBySlugEndpoint));

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
