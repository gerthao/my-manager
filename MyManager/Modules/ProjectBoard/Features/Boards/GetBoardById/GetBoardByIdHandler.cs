using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;

public interface IGetBoardByIdHandler
{
    Task<Result<string>> HandleQuery(GetBoardByIdQuery query, CancellationToken ct = default);
}

[RegisterScoped]
public class GetBoardByIdHandler(ProjectBoardDbContext db) : IGetBoardByIdHandler
{
    public async Task<Result<string>> HandleQuery(GetBoardByIdQuery query,
        CancellationToken ct = default)
    {
        var slug = await db.Boards.Where(b => b.Id == query.Id)
            .Select(b => b.Slug)
            .FirstOrDefaultAsync(ct);

        if (slug == null)
            return ProjectBoardErrors.NotFound(query.Id);

        return slug;
    }
}
