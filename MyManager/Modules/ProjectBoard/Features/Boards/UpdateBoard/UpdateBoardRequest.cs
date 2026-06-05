using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

public record UpdateBoardRequest([FromRoute] int Id, [FromBody] UpdateBoardRequestBody Body);

public record UpdateBoardRequestBody(string Name, string? Description = null);
