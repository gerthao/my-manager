using MyManager.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Columns.CreateColumn;

[Isomorph]
public sealed class CreateColumnCommand(int BoardId, string Name);
