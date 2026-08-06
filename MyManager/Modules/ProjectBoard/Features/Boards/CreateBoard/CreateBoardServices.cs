using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

public readonly record struct CreateBoardServices(
    IValidator<CreateBoardRequest, CreateBoardCommand> validator,
    IRequestHandler<CreateBoardCommand, Result<CreateBoardResponse>> handler,
    ILogger<ProjectBoardModule> logger
);
