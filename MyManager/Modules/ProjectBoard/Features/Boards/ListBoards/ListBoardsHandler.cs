using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.ListBoards;

public interface IListBoardsHandler
{
    Task<Result<List<ListBoardsResponse>>> HandleQuery(CancellationToken ct = default);
}

[RegisterScoped]
public sealed class ListBoardsHandler(ProjectBoardDbContext db)
    : IRequestHandler<ListBoardsQuery, Result<List<ListBoardsResponse>>>
{
    public async Task<Result<List<ListBoardsResponse>>> Handle(ListBoardsQuery query, CancellationToken ct = default)
    {
        var boards = db.Boards
            .Select(b => new ListBoardsResponse(b.Id, b.Name, b.Slug, b.CreatedAt, b.ModifiedAt));

        if (query.Name is not null)
            boards = boards.Where(b => b.Name.Contains(query.Name, StringComparison.InvariantCultureIgnoreCase));

        return await boards.ToListAsync(ct);
    }
}
