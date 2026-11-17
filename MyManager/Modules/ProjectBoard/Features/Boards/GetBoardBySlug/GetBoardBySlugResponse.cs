namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

public sealed record GetBoardBySlugResponse(
    int BoardId,
    string Name,
    string Slug,
    string? Description,
    DateTimeOffset CreatedAt,
    DateTimeOffset ModifiedAt
);
