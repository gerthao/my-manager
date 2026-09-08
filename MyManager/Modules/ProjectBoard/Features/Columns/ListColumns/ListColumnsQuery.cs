using MyManager.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Columns.ListColumns;

[GenerateApply]
public sealed partial record ListColumnsQuery(int BoardId);
