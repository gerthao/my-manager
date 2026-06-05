namespace MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;

public static partial class DeleteBoardLogs
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Board {Id} - {Slug} has been deleted")]
    public static partial void LogDeleted(ILogger<ProjectBoardModule> logger, int id, string slug);
}
