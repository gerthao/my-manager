using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Http;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Columns.GetColumnById;

[RegisterScoped]
public sealed class GetColumnByIdHandler(ProjectBoardDbContext db)
    : IRequestHandler<GetColumnByIdRequest, Result<GetColumnByIdResponse>>
{
    public async Task<Result<GetColumnByIdResponse>> Handle(GetColumnByIdRequest request,
        CancellationToken ct = default)
    {
        var column =
            await db.Columns.FirstOrDefaultAsync(c => c.Id == request.ColumnId && c.BoardId == request.BoardId, ct);

        if (column is null) return ColumnErrors.NotFound();

        return new GetColumnByIdResponse(column.Id, column.BoardId, column.Name, column.Description, column.ModifiedAt);
    }
}
