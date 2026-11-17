using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

public readonly record struct UpdateBoardServices(
    IValidator<UpdateBoardRequest, UpdateBoardCommand> Validator,
    IRequestHandler<UpdateBoardCommand, Either<UpdateBoardError, UpdateBoardResponse>> Handler,
    ILogger<ProjectBoardModule> Logger
);
