using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Columns.CreateColumn;

public sealed record CreateColumnRequest(
    [FromRoute] int BoardId,
    [FromBody] CreateColumnBody Body
);

public sealed record CreateColumnBody(
    string Name
);
