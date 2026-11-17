using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;

[RegisterScoped]
public sealed class GetBoardByIdHandler(ProjectBoardDbContext db) : IRequestHandler<GetBoardByIdQuery, Option<GetBoardByIdResponse>>
{
    public async Task<Option<GetBoardByIdResponse>> Handle(GetBoardByIdQuery query,
        CancellationToken ct = default)
    {
        var slug = await db.Boards.Where(b => b.BoardId == query.BoardId)
            .Select(b => new GetBoardByIdResponse(b.BoardId, b.Name, b.Slug, b.Description, b.CreatedAt, b.ModifiedAt))
            .FirstOrDefaultAsync(ct);

        return slug;
    }
}
