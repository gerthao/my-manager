using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Boards.ListBoards;

public sealed record ListBoardsRequest(
    [FromQuery] string? Name
);
