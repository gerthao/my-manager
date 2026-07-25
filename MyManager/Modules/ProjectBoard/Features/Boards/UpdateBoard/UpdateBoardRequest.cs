using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

public sealed record UpdateBoardRequest([FromRoute] int Id, [FromBody] UpdateBoardRequestBody Body);

public sealed record UpdateBoardRequestBody(string Name, string? Description = null);
