namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

public sealed record UpdateBoardResponse(int Id, string Name, string Slug, DateTimeOffset ModifiedAt);
