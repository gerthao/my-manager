using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;

public readonly record struct GetBoardByIdServices(
    IRequestHandler<GetBoardByIdQuery, Result<string>> Handler,
    ILogger<ProjectBoardModule> Logger
);
