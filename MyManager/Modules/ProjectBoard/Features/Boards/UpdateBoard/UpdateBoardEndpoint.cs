using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Modules.ProjectBoard.Domain.Errors;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

using UpdateBoardResult = Results<
    BadRequest<ValidationProblemDetails>,
    NotFound<ProblemDetails>,
    NoContent,
    Ok<UpdateBoardResponse>
>;

public static class UpdateBoardEndpoint
{
    [MapPatch("/api/project-board/boards/{boardId:int}")]
    [WithTags("Boards")]
    [WithName("UpdateBoard")]
    [WithSummary("Updates an existing board")]
    public static async Task<UpdateBoardResult> HandleEndpoint(
        [AsParameters] UpdateBoardRequest request,
        [AsParameters] UpdateBoardServices services,
        CancellationToken ct = default
    ) => services.Validator.Validate(request) switch
    {
        Invalid<ValidationError>(var errors) => errors.AsValidationProblem(
            title: "Board could not be updated",
            detail: $"Could not update board with id {request.BoardId}. Request is invalid.",
            instance: $"/api/project-board/boards/{request.BoardId}",
            extensions: new()
            {
                ["traceId"] = Activity.Current?.Id
            }
        ).Pipe(TypedResults.BadRequest),
        Valid<UpdateBoardCommand>(var command) => await services.Handler.Handle(command, ct) switch
        {
            Left<UpdateBoardError>(BoardNotFound error) => new ProblemDetails()
            {
                Title = "Board could not be updated",
                Detail = $"Could not update board with id {request.BoardId}. Board does not exist.",
                Instance = $"/api/project-board/boards/{request.BoardId}",
                Extensions =
                {
                    ["traceId"] = Activity.Current?.Id
                }
            }.Pipe(TypedResults.NotFound),
            Left<UpdateBoardError>(BoardUnchanged error) => TypedResults.NoContent(),
            Right<UpdateBoardResponse>(var updated) => updated
                .Tap(u => UpdateBoardLogs.LogUpdated(services.Logger, u.BoardId, u.Slug))
                .Pipe(TypedResults.Ok)
        },
    };
}
