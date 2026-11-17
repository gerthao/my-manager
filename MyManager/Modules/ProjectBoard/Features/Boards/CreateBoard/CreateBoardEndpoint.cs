using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Routing.Attributes;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

using CreateBoardResult =
    Results<BadRequest<ValidationProblemDetails>, Conflict<ProblemDetails>, Created<CreateBoardResponse>>;

public static class CreateBoardEndpoint
{
    [MapPost("/api/project-board/boards")]
    [WithName("CreateBoard")]
    [WithSummary("Creates a new board")]
    [WithTags("Boards")]
    public static async Task<CreateBoardResult> HandleEndpoint(
        [AsParameters] CreateBoardRequest request,
        [AsParameters] CreateBoardServices services,
        CancellationToken ct = default
    ) =>
        services.Validator.Validate(request) switch
        {
            Invalid<ValidationError>(var errors) => errors
                .AsValidationProblem(
                    title: "Board could not be created",
                    instance: "/api/project-board/boards"
                ).Pipe(TypedResults.BadRequest),
            Valid<CreateBoardCommand>(var command) => await services.Handler.Handle(command, ct) switch
            {
                Left<BoardAlreadyExists>(var error) => new ProblemDetails
                {
                    Title = "Board could not be created",
                    Detail = error.Description,
                    Instance = "/api/project-board/boards",
                }.Pipe(TypedResults.Conflict),
                Right<CreateBoardResponse>(var response) => response
                    .Tap(r => CreateBoardLogs.LogCreated(services.Logger, r.BoardId, r.Slug))
                    .Pipe(r => TypedResults.Created($"/api/project-board/boards/{r.BoardId}-{r.Slug}", r)),
            },
        };
}
