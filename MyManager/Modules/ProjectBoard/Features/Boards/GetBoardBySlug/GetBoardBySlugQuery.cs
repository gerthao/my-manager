using MyManager.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

[Isomorph]
public sealed partial record GetBoardBySlugQuery(int Id, string Slug);
