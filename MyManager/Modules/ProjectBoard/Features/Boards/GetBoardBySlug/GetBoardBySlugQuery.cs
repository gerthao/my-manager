using MyManager.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

[GenerateApply]
public sealed partial record GetBoardBySlugQuery(int Id, string Slug);
