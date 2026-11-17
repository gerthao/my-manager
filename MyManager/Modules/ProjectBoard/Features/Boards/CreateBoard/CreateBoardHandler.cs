using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;
using MyManager.Modules.ProjectBoard.Domain.Entities;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

[RegisterScoped]
public sealed class CreateBoardHandler(ProjectBoardDbContext db)
    : IRequestHandler<CreateBoardCommand, Either<BoardAlreadyExists, CreateBoardResponse>>
{
    public async Task<Either<BoardAlreadyExists, CreateBoardResponse>> Handle(CreateBoardCommand command,
        CancellationToken ct = default)
    {
        var boardExists = await db.Boards.Where(b => b.Name == command.Name).AnyAsync(ct);

        if (boardExists)
            return new BoardAlreadyExists($"Board {command.Name} already exists!");

        var board = new Board
        {
            Name = command.Name,
            Description = command.Description.OrNull(),
            Slug = Slug.Slugify(command.Name),
        };

        await db.Boards.AddAsync(board, ct);
        await db.SaveChangesAsync(ct);

        return new CreateBoardResponse(board.BoardId, board.Name, board.Slug, board.Description, board.CreatedAt);
    }
}
