using MyManager.Attributes;
using MyManager.Common;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

[GenerateApply]
public sealed partial record UpdateBoardCommand(int BoardId, Option<string> Name, Option<string> Description);
