using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Columns.ListColumns;


[RegisterScoped]
public sealed class ListColumnsHandler(ProjectBoardDbContext db): IRequestHandler<ListColumnsQuery, Result<List<ListColumnsResponse>>>
{
    public async Task<Result<List<ListColumnsResponse>>> Handle(ListColumnsQuery query,
        CancellationToken ct = default)
    {
        var columns = await db.Columns
            .Where(c => c.BoardId == query.BoardId)
            .Select(c => new ListColumnsResponse(c.Id, c.Name, c.Description))
            .ToListAsync(ct);

        return columns;
    }
}
