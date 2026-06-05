using MyManager.Attributes;
using MyManager.Common;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

[Isomorph]
public partial record UpdateBoardCommand(int Id, string Name, Option<string> Description);
