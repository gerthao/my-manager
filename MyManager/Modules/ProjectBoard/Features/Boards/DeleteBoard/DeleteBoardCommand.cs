using MyManager.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.DeleteBoard;

[GenerateApply]
public sealed partial record DeleteBoardCommand(int Id);
