using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.GetBoardBySlug;

public readonly record struct GetBoardBySlugServices(
    IRequestHandler<GetBoardBySlugQuery, Option<GetBoardBySlugResponse>> Handler,
    ILogger<ProjectBoardModule> Logger
);
