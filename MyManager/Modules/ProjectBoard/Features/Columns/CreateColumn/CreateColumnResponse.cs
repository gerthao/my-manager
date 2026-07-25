namespace MyManager.Modules.ProjectBoard.Features.Columns.CreateColumn;

public sealed record CreateColumnResponse(int Id, int BoardId, string BoardSlug, string Name, DateTimeOffset CreatedAt);
