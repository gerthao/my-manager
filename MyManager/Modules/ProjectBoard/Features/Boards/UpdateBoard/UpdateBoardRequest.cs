using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

public sealed record UpdateBoardRequest([FromRoute] int BoardId, [FromBody] UpdateBoardRequestBody Body);

public sealed record UpdateBoardRequestBody(string? Name = null, string? Description = null);
