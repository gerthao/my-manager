using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

public interface IGetBoardBySlugHandler
{
    Task<Result<GetBoardBySlugResponse>> HandleQuery(GetBoardBySlugQuery query,
        CancellationToken ct = default);
}

[RegisterScoped]
public class GetBoardBySlugHandler(ProjectBoardDbContext db) : IGetBoardBySlugHandler
{
    public async Task<Result<GetBoardBySlugResponse>> HandleQuery(GetBoardBySlugQuery query,
        CancellationToken ct = default)
    {
        var board = await db.Boards.Where(b => b.Id == query.Id)
            .Select(b => new GetBoardBySlugResponse(b.Id, b.Name, b.Slug, b.Description, b.CreatedAt, b.ModifiedAt))
            .FirstOrDefaultAsync(ct);

        if (board == null)
            return ProjectBoardErrors.NotFound(query.Slug);

        return board;
    }
}
