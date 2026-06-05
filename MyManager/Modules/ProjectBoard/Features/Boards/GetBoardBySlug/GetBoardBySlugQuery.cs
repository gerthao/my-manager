using MyManager.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

[Isomorph]
public partial record GetBoardBySlugQuery(int Id, string Slug);
