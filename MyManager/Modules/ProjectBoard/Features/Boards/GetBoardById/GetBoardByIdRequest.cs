using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;

public record GetBoardByIdRequest([FromRoute] int Id);
