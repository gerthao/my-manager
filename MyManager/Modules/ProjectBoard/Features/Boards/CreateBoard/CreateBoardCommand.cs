using MyManager.Attributes;
using MyManager.Common;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

[Isomorph]
public partial record CreateBoardCommand(string Name, Option<string> Description);
