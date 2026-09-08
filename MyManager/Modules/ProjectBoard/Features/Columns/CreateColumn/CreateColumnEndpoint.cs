using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Columns.CreateColumn;

using CreateColumnResult = Results<BadRequest<ErrorDetails>, Conflict<ErrorDetails>, Created<CreateColumnResponse>>;

public static class CreateColumnEndpoint
{
    [MapPost("/project-board/boards/{boardId:int}/columns")]
    [WithName("CreateColumn")]
    [WithTags("Columns")]
    [WithSummary("Creates a new column for a project board.")]
    public static Task<CreateColumnResult> HandleEndpoint(
        [AsParameters] CreateColumnRequest request,
        IValidator<CreateColumnRequest, CreateColumnCommand> validator,
        IRequestHandler<CreateColumnCommand, Result<CreateColumnResponse>> handler,
        ILogger<ProjectBoardModule> logger,
        CancellationToken ct = default
    ) => validator.Validate(request).FoldAsync(
        errors => errors
            .Pipe(ErrorDetails.FieldErrorOnly)
            .Pipe(TypedResults.BadRequest),
        async command => (await handler.Handle(command, ct))
            .Pipe(result =>
                result.Fold<CreateColumnResult>(
                    error => error
                        .Pipe(ErrorDetails.GlobalErrorOnly)
                        .Pipe(TypedResults.Conflict),
                    response => response
                        .Pipe(r => TypedResults.Created($"/project-board/boards/{r.BoardId}/columns/{r.Id}", r))
                )
            )
    );
}
