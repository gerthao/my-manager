using Microsoft.AspNetCore.Mvc;
using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;

public readonly record struct GetBoardByIdServices(
    [FromServices] IRequestHandler<GetBoardByIdQuery, Option<GetBoardByIdResponse>> Handler,
    [FromServices] ILogger<ProjectBoardModule> Logger
);
