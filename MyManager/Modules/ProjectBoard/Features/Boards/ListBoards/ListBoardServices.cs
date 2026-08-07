using MyManager.Common;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.ListBoards;

public readonly record struct ListBoardServices(
    IRequestHandler<ListBoardsQuery, Result<List<ListBoardsResponse>>> Handler
);
