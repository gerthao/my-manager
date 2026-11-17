namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;

public sealed record GetBoardByIdResponse(
    int BoardId,
    string Name,
    string Slug,
    string? Description,
    DateTimeOffset CreatedAt,
    DateTimeOffset ModifiedAt
);
