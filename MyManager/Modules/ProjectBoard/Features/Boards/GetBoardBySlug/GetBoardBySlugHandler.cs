using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Http;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

[RegisterScoped]
public sealed class GetBoardBySlugHandler(ProjectBoardDbContext db)
    : IRequestHandler<GetBoardBySlugQuery, Result<GetBoardBySlugResponse>>
{
    public async Task<Result<GetBoardBySlugResponse>> Handle(GetBoardBySlugQuery query,
        CancellationToken ct = default)
    {
        var board = await db.Boards.Where(b => b.Id == query.Id)
            .Select(b => new GetBoardBySlugResponse(b.Id, b.Name, b.Slug, b.Description, b.CreatedAt, b.ModifiedAt))
            .FirstOrDefaultAsync(ct);

        if (board == null)
            return BoardErrors.NotFound(query.Slug);

        return board;
    }
}
