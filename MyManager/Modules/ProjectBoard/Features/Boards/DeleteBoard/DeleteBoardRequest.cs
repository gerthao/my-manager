using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;

public sealed record DeleteBoardRequest([FromRoute] int Id);
