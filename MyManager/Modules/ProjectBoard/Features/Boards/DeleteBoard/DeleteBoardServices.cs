using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;

public readonly record struct DeleteBoardServices(
    IRequestHandler<DeleteBoardCommand, Option<DeleteBoardResponse>> Handler,
    ILogger<ProjectBoardModule> Logger
);
