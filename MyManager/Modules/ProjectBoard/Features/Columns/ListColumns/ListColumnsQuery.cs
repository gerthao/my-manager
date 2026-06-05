using Microsoft.AspNetCore.Mvc;
using MyManager.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Columns.ListColumns;

[Isomorph]
public partial record ListColumnsQuery(int BoardId);
