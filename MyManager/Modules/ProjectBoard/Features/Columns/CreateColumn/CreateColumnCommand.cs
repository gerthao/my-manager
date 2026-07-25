using MyManager.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Columns.CreateColumn;

[Isomorph]
public sealed partial record CreateColumnCommand(int BoardId, string Name);
