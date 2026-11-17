using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Modules.ProjectBoard.Domain.Errors;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Columns.CreateColumn;

using CreateColumnResult =
    Results<BadRequest<ValidationProblemDetails>, NotFound<ProblemDetails>, Conflict<ProblemDetails>,
        Ok<CreateColumnResponse>>;

public static class CreateColumnEndpoint
{
    [MapPost("/api/project-board/boards/{boardId:int}/columns")]
    [WithName("CreateColumn")]
    [WithTags("Columns")]
    [WithSummary("Creates a new column for a project board.")]
    public static async Task<CreateColumnResult> HandleEndpoint(
        [AsParameters] CreateColumnRequest request,
        [AsParameters] CreateColumnServices services,
        CancellationToken ct = default
    ) => services.Validator.Validate(request) switch
    {
        Invalid<ValidationError>(var errors) => errors.AsValidationProblem(
            title: "Column could not be created",
            detail: "Unable to create column for board.  Request is invalid.",
            instance: $"/api/project-board/boards/{request.BoardId}/columns",
            extensions: new() { ["traceId"] = Activity.Current?.Id }
        ).Pipe(TypedResults.BadRequest),
        Valid<CreateColumnCommand>(var command) => await services.Handler.Handle(command, ct) switch
        {
            Left<CreateColumnError> (BoardNotFound error) => new ProblemDetails
            {
                Title = "Column could not be created",
                Detail = "Unable to create column for board.  Board does not exist.",
                Instance = $"/api/project-board/boards/{request.BoardId}/columns",
                Extensions = new Dictionary<string, object?> { ["traceId"] = Activity.Current?.Id },
            }.Pipe(TypedResults.NotFound),
            Left<CreateColumnError> (ColumnAlreadyExists error) => new ProblemDetails
            {
                Title = "Column could not be created",
                Detail = "Unable to create column for board.  A column with the same name already exists.",
                Instance = $"/api/project-board/boards/{request.BoardId}/columns",
                Extensions = new Dictionary<string, object?> { ["traceId"] = Activity.Current?.Id },
            }.Pipe(TypedResults.Conflict),
            Right<CreateColumnResponse> (var response) => response.Pipe(TypedResults.Ok),
        },
    };
}
