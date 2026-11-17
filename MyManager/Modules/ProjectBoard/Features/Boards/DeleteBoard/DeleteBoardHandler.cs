using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;

[RegisterScoped]
public sealed class DeleteBoardHandler(ProjectBoardDbContext db) : IRequestHandler<DeleteBoardCommand, Option<DeleteBoardResponse>>
{
    public async Task<Option<DeleteBoardResponse>> Handle(DeleteBoardCommand command,
        CancellationToken ct = default)
    {
        var board = await db.Boards.Where(b => b.BoardId == command.BoardId).FirstOrDefaultAsync(ct);

        if (board is null)
            return new None();

        db.Boards.Remove(board);

        await db.SaveChangesAsync(ct);

        return new DeleteBoardResponse(board.BoardId, board.Slug);
    }
}
