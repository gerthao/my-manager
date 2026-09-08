using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

using CreateBoardResult =
    Results<BadRequest<ProblemDetails>, Conflict<ProblemDetails>, Created<CreateBoardResponse>>;

public static class CreateBoardEndpoint
{
    [MapPost("/project-board/boards")]
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
                .AsProblem(
                    title: "Board could not be created",
                    instance: "/project-board/boards"
                ).Pipe(TypedResults.BadRequest),
            Valid<CreateBoardCommand>(var command) => await services.Handler.Handle(command, ct) switch
            {
                Left<BoardAlreadyExists> (var error) => new ProblemDetails
                {
                    Title = "Board could not be created.",
                    Detail = error.Description,
                    Instance = "/project-board/boards",
                }.Pipe(TypedResults.Conflict),
                Right<CreateBoardResponse> (var response) => response
                    .Tap(r => CreateBoardLogs.LogCreated(services.Logger, r.Id, r.Slug))
                    .Pipe(r => TypedResults.Created($"/project-board/boards/{r.Slug}", r)),
            },
        };
}
