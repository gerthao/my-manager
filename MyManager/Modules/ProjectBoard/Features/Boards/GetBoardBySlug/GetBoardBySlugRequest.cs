using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

public sealed record GetBoardBySlugRequest([FromRoute] int BoardId, [FromRoute] string Slug);
