using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

[RegisterScoped]
public sealed class GetBoardBySlugHandler(ProjectBoardDbContext db)
    : IRequestHandler<GetBoardBySlugQuery, Option<GetBoardBySlugResponse>>
{
    public async Task<Option<GetBoardBySlugResponse>> Handle(GetBoardBySlugQuery query,
        CancellationToken ct = default)
    {
        return await db.Boards.Where(b => b.BoardId == query.BoardId && b.Slug == query.Slug)
            .Select(b => new GetBoardBySlugResponse(b.BoardId, b.Name, b.Slug, b.Description, b.CreatedAt, b.ModifiedAt))
            .FirstOrDefaultAsync(ct);
    }
}
