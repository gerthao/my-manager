namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

public static partial class UpdateBoardLogs
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Board {Id} - {Slug} has been updated")]
    public static partial void LogUpdated(ILogger<ProjectBoardModule> logger, int id, string slug);
}
