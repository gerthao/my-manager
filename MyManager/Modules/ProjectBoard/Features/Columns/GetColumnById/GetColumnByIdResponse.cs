namespace MyManager.Modules.ProjectBoard.Features.Columns.GetColumnById;

public sealed record GetColumnByIdResponse(
    int Id,
    int BoardId,
    string Name,
    string? Description,
    DateTimeOffset ModifiedAt
);
