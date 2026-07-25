using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Modules.ProjectBoard.Domain.Entities;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Columns.CreateColumn;

public interface ICreateColumnHandler
{
    Task<Result<CreateColumnResponse>> HandleCommand(CreateColumnCommand command, CancellationToken ct = default);
}

[RegisterScoped]
public sealed class CreateColumnHandler(ProjectBoardDbContext db) : ICreateColumnHandler
{
    public async Task<Result<CreateColumnResponse>> HandleCommand(CreateColumnCommand command,
        CancellationToken ct = default)
    {
        var board = await db.Boards.FirstOrDefaultAsync(b => b.Id == command.BoardId, ct);

        if (board == null)
            return BoardErrors.NotFound(command.BoardId);

        var column = new Column
        {
            BoardId = command.BoardId,
            Board = board,
            Name = command.Name,
        };

        await db.Columns.AddAsync(column, ct);
        await db.SaveChangesAsync(ct);

        return new CreateColumnResponse(
            column.Id,
            board.Id,
            board.Slug,
            column.Name,
            column.CreatedAt
        );
    }
}
