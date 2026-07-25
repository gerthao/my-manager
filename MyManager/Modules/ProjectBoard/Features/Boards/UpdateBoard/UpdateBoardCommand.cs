using MyManager.Attributes;
using MyManager.Common;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

[Isomorph]
public sealed partial record UpdateBoardCommand(int Id, string Name, Option<string> Description);
