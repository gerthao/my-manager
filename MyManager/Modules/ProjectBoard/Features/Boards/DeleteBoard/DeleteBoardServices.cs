namespace MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;

public readonly record struct DeleteBoardServices(
    IDeleteBoardHandler Handler,
    ILogger<ProjectBoardModule> Logger
);
