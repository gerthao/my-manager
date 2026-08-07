using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

public readonly record struct UpdateBoardServices(
    IValidator<UpdateBoardRequest, UpdateBoardCommand> Validator,
    IRequestHandler<UpdateBoardCommand, Result<UpdateBoardResponse>> Handler,
    ILogger<ProjectBoardModule> Logger
);
