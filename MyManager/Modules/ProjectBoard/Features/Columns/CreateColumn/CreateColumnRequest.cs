using Microsoft.AspNetCore.Mvc;

namespace MyManager.Modules.ProjectBoard.Features.Columns.CreateColumn;

public record CreateColumnRequest(
    [FromRoute] int BoardId,
    [FromBody] CreateColumnBody Body
);

public record CreateColumnBody(
    string Name
);
