using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Http;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;

[RegisterScoped]
public sealed class GetBoardByIdHandler(ProjectBoardDbContext db) : IRequestHandler<GetBoardByIdQuery, Result<string>>
{
    public async Task<Result<string>> Handle(GetBoardByIdQuery query,
        CancellationToken ct = default)
    {
        var slug = await db.Boards.Where(b => b.Id == query.Id)
            .Select(b => b.Slug)
            .FirstOrDefaultAsync(ct);

        if (slug == null)
            return BoardErrors.NotFound(query.Id);

        return slug;
    }
}
