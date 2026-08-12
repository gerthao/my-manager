using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Common;
using MyManager.Common.Extensions;
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
        CreateColumnHandler handler,
        ILogger<ProjectBoardModule> logger,
        CancellationToken ct = default
    ) => validator.Validate(request).FoldAsync(
        errors => errors
            .Pipe(ErrorDetails.FieldErrorOnly)
            .Pipe(TypedResults.BadRequest),
        async command => (await handler.HandleCommand(command, ct))
            .Pipe(result =>
                result.Fold<CreateColumnResult>(
                    error => error
                        .Pipe(ErrorDetails.GlobalErrorOnly)
                        .Pipe(TypedResults.Conflict),
                    response => response
                        .Pipe(r =>
                            TypedResults.Created($"/boards/{r.BoardId}/columns/{r.Id}", r)
                        )
                )
            )
    );
}
