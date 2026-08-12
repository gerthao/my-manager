using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Columns.GetColumnById;

public sealed record GetColumnByIdRequest(
    [FromRoute] int BoardId,
    [FromRoute] int ColumnId
);
