using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Columns.CreateColumn;

using CreateColumnResult = Results<BadRequest<ErrorDetails>, Conflict<ErrorDetails>, Created<CreateColumnResponse>>;

public sealed class CreateColumnEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/boards/{boardId:int}/columns", HandleEndpoint);
    }

    private static Task<CreateColumnResult> HandleEndpoint(
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
            .Pipe(r =>
                r.Fold<CreateColumnResult>(
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
