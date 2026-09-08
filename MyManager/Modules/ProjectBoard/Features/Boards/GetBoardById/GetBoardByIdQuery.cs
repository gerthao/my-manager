using MyManager.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;

[GenerateApply]
public sealed partial record GetBoardByIdQuery(int Id);
