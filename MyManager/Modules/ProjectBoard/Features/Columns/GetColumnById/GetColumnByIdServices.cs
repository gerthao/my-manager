using MyManager.Common;
using MyManager.Http;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Columns.GetColumnById;

public readonly record struct GetColumnByIdServices(
    IRequestHandler<GetColumnByIdRequest, Either<ColumnNotFound, GetColumnByIdResponse>> Handler,
    ILogger<ProjectBoardModule> Logger
);
