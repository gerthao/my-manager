using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;

public record DeleteBoardRequest([FromRoute] int Id);
