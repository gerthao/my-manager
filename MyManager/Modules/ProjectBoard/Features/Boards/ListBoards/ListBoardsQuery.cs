using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Boards.ListBoards;

public sealed record ListBoardsQuery(
    [FromQuery(Name = "name")] string? Name = null
);
