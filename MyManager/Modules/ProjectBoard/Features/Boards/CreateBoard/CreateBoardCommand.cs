using MyManager.Attributes;
using MyManager.Common;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

[Isomorph]
public sealed partial record CreateBoardCommand(string Name, Option<string> Description);
