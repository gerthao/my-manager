using Microsoft.AspNetCore.Mvc;
using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

public readonly record struct CreateBoardServices(
    [FromServices] IValidator<CreateBoardRequest, CreateBoardCommand> Validator,
    [FromServices] IRequestHandler<CreateBoardCommand, Either<BoardAlreadyExists, CreateBoardResponse>> Handler,
    [FromServices] ILogger<ProjectBoardModule> Logger
);
