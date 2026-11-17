namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

public sealed record UpdateBoardResponse(int BoardId, string Name, string Slug, DateTimeOffset ModifiedAt);
