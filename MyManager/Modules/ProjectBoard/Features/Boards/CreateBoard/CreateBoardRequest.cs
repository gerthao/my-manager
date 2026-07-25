using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

public sealed record CreateBoardRequest(
    [FromBody] CreateBoardRequestBody Body
);

public sealed record CreateBoardRequestBody(string Name, string? Description = null);
