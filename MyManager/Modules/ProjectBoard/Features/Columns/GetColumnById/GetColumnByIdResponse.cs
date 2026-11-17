namespace MyManager.Modules.ProjectBoard.Features.Columns.GetColumnById;

public sealed record GetColumnByIdResponse(
    int ColumnId,
    int BoardId,
    string Name,
    DateTimeOffset ModifiedAt
);
