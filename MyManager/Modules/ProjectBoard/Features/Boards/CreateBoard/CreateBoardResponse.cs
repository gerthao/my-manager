namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

public sealed record CreateBoardResponse(
    int Id,
    string Name,
    string Slug,
    string? Description,
    DateTimeOffset CreatedAt);
