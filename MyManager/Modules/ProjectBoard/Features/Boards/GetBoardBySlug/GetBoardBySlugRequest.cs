using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

public sealed record GetBoardBySlugRequest([FromRoute] int Id, [FromRoute] string Slug);
