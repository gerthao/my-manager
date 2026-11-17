namespace MyManager.Modules.ProjectBoard.Features.Boards.ListBoards;

public sealed record ListBoardsResponse(
    int BoardId,
    string Name,
    string Slug,
    DateTimeOffset CreatedAt,
    DateTimeOffset ModifiedAt
);
