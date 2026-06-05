namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

public sealed record GetBoardBySlugResponse(
    int Id,
    string Name,
    string Slug,
    string? Description,
    DateTimeOffset CreatedAt,
    DateTimeOffset ModifiedAt
);
