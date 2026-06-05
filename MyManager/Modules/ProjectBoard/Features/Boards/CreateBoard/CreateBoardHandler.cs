using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Modules.ProjectBoard.Domain.Entities;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

public interface ICreateBoardHandler
{
    Task<Result<CreateBoardResponse>> HandleCommand(CreateBoardCommand command,
        CancellationToken ct = default);
}

[RegisterScoped]
public class CreateBoardHandler(ProjectBoardDbContext db) : ICreateBoardHandler
{
    public async Task<Result<CreateBoardResponse>> HandleCommand(CreateBoardCommand command,
        CancellationToken ct = default)
    {
        var boardExists = await db.Boards.Where(b => b.Name == command.Name).AnyAsync(ct);
        if (boardExists)
            return ProjectBoardErrors.AlreadyExists(command.Name);

        var board = new Board
        {
            Name = command.Name,
            Description = command.Description.OrNull(),
            Slug = Slug.Slugify(command.Name),
        };

        await db.Boards.AddAsync(board, ct);
        await db.SaveChangesAsync(ct);

        return new CreateBoardResponse(board.Id, board.Name, board.Slug, board.Description, board.CreatedAt);
    }
}
