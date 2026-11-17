using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Columns.CreateColumn;

public readonly record struct CreateColumnServices(
    IValidator<CreateColumnRequest, CreateColumnCommand> Validator,
    IRequestHandler<CreateColumnCommand, Either<CreateColumnError, CreateColumnResponse>> Handler,
    ILogger<ProjectBoardModule> Logger
);
