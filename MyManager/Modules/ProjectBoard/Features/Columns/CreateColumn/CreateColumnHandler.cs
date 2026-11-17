using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Http;
using MyManager.Modules.ProjectBoard.Domain.Entities;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Columns.CreateColumn;

[RegisterScoped]
public sealed class CreateColumnHandler(ProjectBoardDbContext db)
    : IRequestHandler<CreateColumnCommand, Either<CreateColumnError, CreateColumnResponse>>
{
    public async Task<Either<CreateColumnError, CreateColumnResponse>> Handle(CreateColumnCommand command,
        CancellationToken ct = default)
    {
        var board = await db.Boards.FirstOrDefaultAsync(b => b.BoardId == command.BoardId, ct);

        if (board == null)
            return new CreateColumnError(new BoardNotFound());

        var columnExists = await db.Columns.AnyAsync(c => c.BoardId == command.BoardId && c.Name == command.Name, ct);

        if (columnExists)
            return new CreateColumnError(new ColumnAlreadyExists());

        var column = new Column
        {
            BoardId = command.BoardId,
            Board = board,
            Name = command.Name,
        };

        await db.Columns.AddAsync(column, ct);
        await db.SaveChangesAsync(ct);

        return new CreateColumnResponse(
            column.ColumnId,
            board.BoardId,
            board.Slug,
            column.Name,
            column.CreatedAt
        );
    }
}
