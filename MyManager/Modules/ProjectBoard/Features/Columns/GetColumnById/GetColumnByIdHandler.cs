using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Http;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Columns.GetColumnById;

[RegisterScoped]
public sealed class GetColumnByIdHandler(ProjectBoardDbContext db)
    : IRequestHandler<GetColumnByIdRequest, Either<ColumnNotFound, GetColumnByIdResponse>>
{
    public async Task<Either<ColumnNotFound, GetColumnByIdResponse>> Handle(GetColumnByIdRequest request,
        CancellationToken ct = default)
    {
        var column =
            await db.Columns.FirstOrDefaultAsync(c => c.ColumnId == request.ColumnId && c.BoardId == request.BoardId, ct);

        if (column is null) return new ColumnNotFound(request.ColumnId, request.BoardId, $"Unable to find column with id {request.ColumnId} for board id {request.BoardId}");

        return new GetColumnByIdResponse(column.ColumnId, column.BoardId, column.Name, column.ModifiedAt);
    }
}
