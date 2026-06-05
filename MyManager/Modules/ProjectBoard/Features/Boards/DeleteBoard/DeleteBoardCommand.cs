using MyManager.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;

[Isomorph]
public partial record DeleteBoardCommand(int Id);
