using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Http;
using MyManager.Modules.ProjectBoard.Domain.Errors;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Cards;

using ListCardsResult = Ok<List<ListCardsResponse>>;

public static class ListCardsEndpoint
{
    [MapGet("/project-board/{boardId:int}/cards")]
    [WithName("ListCards")]
    [WithSummary("List cards of a board")]
    public async static Task<ListCardsResult> HandleEndpoint(
        [AsParameters] ListCardsRequest request,
        [AsParameters] ListCardsServices services,
        CancellationToken ct = default
    )
    {
        var query = new ListCardsQuery(request.BoardId);
        var listCardsResult = await services.Handler.Handle(query, ct);

        return TypedResults.Ok(listCardsResult);
    }
}

public sealed record ListCardsRequest(
    [FromRoute(Name = "boardId")] int BoardId
);

public sealed record ListCardsQuery(
    int BoardId
);

public sealed record ListCardsResponse(
    int Id,
    int BoardId,
    string Name,
    string? Description,
    DateTimeOffset CreatedAt,
    DateTimeOffset ModifiedAt
);

public readonly record struct ListCardsServices(
    [FromServices] IRequestHandler<ListCardsQuery, List<ListCardsResponse>> Handler,
    [FromServices] ILogger<ProjectBoardModule> Logger
);

[RegisterScoped]
public sealed class ListCardsHandler(ProjectBoardDbContext db) : IRequestHandler<ListCardsQuery, List<ListCardsResponse>>
{
    public async Task<List<ListCardsResponse>> Handle(ListCardsQuery query, CancellationToken ct = default)
    {
        var cards = await db.Cards
            .Where(c => c.BoardId == query.BoardId)
            .Select(c => new ListCardsResponse(
                Id: c.Id,
                BoardId: c.BoardId,
                Name: c.Name,
                Description: c.Description,
                CreatedAt: c.CreatedAt,
                ModifiedAt: c.ModifiedAt
            ))
            .ToListAsync(ct);

        return cards;
    }
}
