using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

public interface IUpdateBoardHandler
{
    Task<Result<UpdateBoardResponse>> HandleCommand(UpdateBoardCommand command,
        CancellationToken ct = default);
}

[RegisterScoped]
public sealed class UpdateBoardHandler(ProjectBoardDbContext db) : IUpdateBoardHandler
{
    public async Task<Result<UpdateBoardResponse>> HandleCommand(UpdateBoardCommand command,
        CancellationToken ct = default)
    {
        var board = await db.Boards.Where(b => b.Id == command.Id).FirstOrDefaultAsync(ct);

        if (board == null)
            return ProjectBoardErrors.NotFound(command.Id);

        board.Name = command.Name;
        board.Description = command.Description.OrNull();
        board.Slug = Slug.Slugify(command.Name);

        await db.SaveChangesAsync(ct);

        return new UpdateBoardResponse(board.Id, board.Name, board.Slug, board.ModifiedAt);
    }
}
