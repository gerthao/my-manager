using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;

namespace MyManager.Modules.ProjectBoard.Features.Boards.ListBoards;

public interface IListBoardsHandler
{
    Task<Result<List<ListBoardsResponse>>> HandleQuery(CancellationToken ct = default);
}

[RegisterScoped]
public sealed class ListBoardsHandler(ProjectBoardDbContext db) : IListBoardsHandler
{
    public async Task<Result<List<ListBoardsResponse>>> HandleQuery(CancellationToken ct = default)
    {
        var list = await db.Boards
            .Select(b => new ListBoardsResponse(b.Id, b.Name, b.Slug, b.CreatedAt, b.ModifiedAt))
            .ToListAsync(ct);

        return list;
    }
}
