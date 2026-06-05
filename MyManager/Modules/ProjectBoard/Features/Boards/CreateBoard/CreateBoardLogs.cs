namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

public static partial class CreateBoardLogs
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Board {Id} - {Slug} has been created")]
    public static partial void LogCreated(ILogger<ProjectBoardModule> logger, int id, string slug);
}
