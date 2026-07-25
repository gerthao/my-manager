using MyManager.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardById;

[Isomorph]
public sealed partial record GetBoardByIdQuery(int Id);
