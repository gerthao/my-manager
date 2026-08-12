using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Columns.GetColumnById;

public readonly record struct GetColumnByIdServices(
    IRequestHandler<GetColumnByIdRequest, Result<GetColumnByIdResponse>> Handler,
    ILogger<ProjectBoardModule> Logger
);
