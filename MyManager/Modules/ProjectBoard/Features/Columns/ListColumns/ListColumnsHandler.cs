using Microsoft.EntityFrameworkCore;
using MyManager.Common;

namespace MyManager.Modules.ProjectBoard.Features.Columns.ListColumns;

public interface IListColumnsHandler
{
    Task<Result<List<ListColumnsResponse>>> HandleQuery(ListColumnsQuery query, CancellationToken ct = default);
}

public sealed class ListColumnsHandler(ProjectBoardDbContext db)
{
    public async Task<Result<List<ListColumnsResponse>>> HandleQuery(ListColumnsQuery query, CancellationToken ct = default)
    {
        var columns = await db.Columns
            .Where(c => c.BoardId == query.BoardId)
            .Select(c => new ListColumnsResponse(c.Id, c.Name, c.Description))
            .ToListAsync(ct);

        return columns;
    }
}
