using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.ListBoards;

public readonly record struct ListBoardsServices(
    IRequestHandler<ListBoardsQuery, List<ListBoardsResponse>> Handler
);
