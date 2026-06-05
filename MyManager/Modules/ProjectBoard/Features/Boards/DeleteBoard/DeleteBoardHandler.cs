using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;

public interface IDeleteBoardHandler
{
    Task<Result<DeleteBoardResponse>> HandleCommand(DeleteBoardCommand command,
        CancellationToken ct = default);
}

[RegisterScoped]
public sealed class DeleteBoardHandler(ProjectBoardDbContext db) : IDeleteBoardHandler
{
    public async Task<Result<DeleteBoardResponse>> HandleCommand(DeleteBoardCommand command,
        CancellationToken ct = default)
    {
        var board = await db.Boards.Where(b => b.Id == command.Id).FirstOrDefaultAsync(ct);

        if (board == null)
            return ProjectBoardErrors.NotFound(command.Id);

        db.Boards.Remove(board);

        await db.SaveChangesAsync(ct);

        return new DeleteBoardResponse(board.Id, board.Slug);
    }
}
