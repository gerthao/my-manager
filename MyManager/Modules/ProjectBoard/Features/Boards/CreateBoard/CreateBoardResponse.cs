namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

public sealed record CreateBoardResponse(
    int BoardId,
    string Name,
    string Slug,
    string? Description,
    DateTimeOffset CreatedAt);
