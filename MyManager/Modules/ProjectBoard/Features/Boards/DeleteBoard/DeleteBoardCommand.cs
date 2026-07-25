using MyManager.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;

[Isomorph]
public sealed partial record DeleteBoardCommand(int Id);
