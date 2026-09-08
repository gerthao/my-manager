using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Http;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Columns.DeleteColumn;

using DeleteColumnResult = Results<NotFound, Conflict, Ok<DeleteColumnResponse>>;

public static class DeleteColumnEndpoint
{
    [MapDelete("/project-board/boards/{boardId:int}/columns/{columnId:int}")]
    [WithTags("Columns")]
    [WithName("DeleteColumn")]
    [WithSummary("Delete a column from its board")]
    public static async Task<DeleteColumnResult> HandleEndpoint(
        [AsParameters] DeleteColumnRequest request,
        [AsParameters] DeleteColumnServices services,
        CancellationToken ct = default) =>
        await services.Handler.Handle(request, ct) switch
        {
            Left<DeleteColumnError>(DeleteColumnNotFound err) => TypedResults.NotFound(),
            Left<DeleteColumnError>(DeleteColumnContainsCards err) => TypedResults.Conflict(),
            Right<DeleteColumnResponse>(var response) => TypedResults.Ok(response),
        };
}

public sealed record DeleteColumnRequest(
    [FromRoute] int BoardId,
    [FromRoute] int ColumnId
);

public sealed record DeleteColumnResponse(
    int BoardId,
    int ColumnId,
    string Name,
    string? Description,
    DateTimeOffset ModifiedAt
);

public sealed record DeleteColumnNotFound;

public sealed record DeleteColumnContainsCards;

[Union]
public readonly record struct DeleteColumnError : IUnion
{
    public DeleteColumnError(DeleteColumnNotFound value) => Value = value;
    public DeleteColumnError(DeleteColumnContainsCards value) => Value = value;
    public object Value { get; }

    public static implicit operator DeleteColumnError(DeleteColumnNotFound value) => new(value);
    public static implicit operator DeleteColumnError(DeleteColumnContainsCards value) => new(value);
}

public readonly record struct DeleteColumnServices(
    IRequestHandler<DeleteColumnRequest, Either<DeleteColumnError, DeleteColumnResponse>> Handler,
    ILogger<ProjectBoardModule> Logger
);

[RegisterScoped]
public sealed class DeleteColumnHandler(ProjectBoardDbContext db)
    : IRequestHandler<DeleteColumnRequest, Either<DeleteColumnError, DeleteColumnResponse>>
{
    public async Task<Either<DeleteColumnError, DeleteColumnResponse>> Handle(DeleteColumnRequest request,
        CancellationToken ct = default)
    {
        var column =
            await db.Columns
                .FirstOrDefaultAsync(c => c.BoardId == request.BoardId && c.Id == request.ColumnId, ct);

        if (column is null)
            return new Left<DeleteColumnError>(new DeleteColumnNotFound());

        var cardsExist = await db.Cards.AnyAsync(c => c.ColumnId == request.ColumnId && c.BoardId == request.BoardId, ct);

        if (cardsExist) return new Left<DeleteColumnError>(new DeleteColumnContainsCards());

        db.Columns.Remove(column);

        await db.SaveChangesAsync(ct);

        return new DeleteColumnResponse(column.BoardId, column.Id, column.Name,
            column.Description, column.ModifiedAt);
    }
}
