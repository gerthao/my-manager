using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

public readonly record struct CreateBoardServices(
    IValidator<CreateBoardRequest, CreateBoardCommand> Validator,
    IRequestHandler<CreateBoardCommand, Result<CreateBoardResponse>> Handler,
    ILogger<ProjectBoardModule> Logger
);
