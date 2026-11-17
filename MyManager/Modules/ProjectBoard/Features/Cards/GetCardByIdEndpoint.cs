using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Cards;

using GetCardByIdResult = Results<NotFound<ProblemDetails>, Ok<GetCardByIdResponse>>;

public static class GetCardByIdEndpoint
{
    [MapGet("/api/project-board/boards/{boardId:int}/cards/{cardId:int}")]
    [WithTags("Cards")]
    [WithName("GetCard")]
    [WithSummary("Get a card from a board by ID")]
    public static async Task<GetCardByIdResult> HandleEndpoint(
        [AsParameters] GetCardByIdRequest request,
        [AsParameters] GetCardByIdServices services,
        CancellationToken ct = default) =>
        await services.Handler.Handle(request, ct) switch
        {
            None => new ProblemDetails
            {
                Title = "Card not found",
                Detail = "Could not find card.",
                Status = StatusCodes.Status404NotFound
            }.Pipe(TypedResults.NotFound),
            Some<GetCardByIdResponse>(var response) => TypedResults.Ok(response),
        };
}

public sealed record GetCardByIdRequest(
    int BoardId,
    int CardId
);

public sealed record GetCardByIdResponse(
    string Name,
    string? Description,
    DateTimeOffset CreatedAt,
    DateTimeOffset ModifiedAt
);

[RegisterScoped]
public sealed class GetCardByIdHandler(ProjectBoardDbContext db)
    : IRequestHandler<GetCardByIdRequest, Option<GetCardByIdResponse>>
{
    public async Task<Option<GetCardByIdResponse>> Handle(GetCardByIdRequest request,
        CancellationToken ct = default)
    {
        var card = await db.Cards
            .Where(c => c.CardId == request.CardId && c.BoardId == request.BoardId)
            .Select(c => new GetCardByIdResponse(c.Name, c.Description, c.CreatedAt, c.ModifiedAt))
            .FirstOrDefaultAsync(ct);

        return Option<GetCardByIdResponse>.Of(card);
    }
}

public readonly record struct GetCardByIdServices(
    [FromServices] IRequestHandler<GetCardByIdRequest, Option<GetCardByIdResponse>> Handler,
    [FromServices] ILogger<ProjectBoardModule> Logger
);
