using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;

public sealed record GetBoardByIdRequest([FromRoute] int BoardId);
