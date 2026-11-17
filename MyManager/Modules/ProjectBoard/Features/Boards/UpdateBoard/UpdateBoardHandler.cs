using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Http;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

[Union]
public readonly record struct UpdateBoardCommandResult: IUnion {
    public object Value { get; }

    public UpdateBoardCommandResult(BoardNotFound value) => Value = value;
    public UpdateBoardCommandResult(BoardUnchanged value) => Value = value;
    public UpdateBoardCommandResult(UpdateBoardResponse value) => Value = value;
}

[RegisterScoped]
public sealed class UpdateBoardHandler(ProjectBoardDbContext db)
    : IRequestHandler<UpdateBoardCommand, Either<UpdateBoardError, UpdateBoardResponse>>
{
    public async Task<Either<UpdateBoardError, UpdateBoardResponse>> Handle(UpdateBoardCommand command,
        CancellationToken ct = default)
    {
        if (command.Name is None && command.Description is None)
            return new UpdateBoardError(new BoardUnchanged());

        var board = await db.Boards.Where(b => b.BoardId == command.BoardId).FirstOrDefaultAsync(ct);

        if (board is null)
            return new UpdateBoardError(new BoardNotFound());

        if (command.Name is Some<string>(var n))
        {
            board.Name = n;
            board.Slug = Slug.Slugify(n);
        }

        if (command.Description is Some<string>(var d))
        {
            board.Description = d;
        }

        await db.SaveChangesAsync(ct);

        return new UpdateBoardResponse(board.BoardId, board.Name, board.Slug, board.ModifiedAt);
    }
}
