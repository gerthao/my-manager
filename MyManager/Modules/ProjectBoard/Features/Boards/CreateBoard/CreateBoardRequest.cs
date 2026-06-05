using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

public record CreateBoardRequest(
    [FromBody] CreateBoardRequestBody Body
);

public record CreateBoardRequestBody(string Name, string? Description = null);
