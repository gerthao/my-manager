using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

using CreateBoardResult = Results<BadRequest<ErrorDetails>, Conflict<ErrorDetails>, Created<CreateBoardResponse>>;

public abstract class CreateBoardEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app.MapPost("/boards", HandleEndpoint);

    public static Task<CreateBoardResult> HandleEndpoint(
        [AsParameters] CreateBoardRequest request,
        IValidator<CreateBoardRequest, CreateBoardCommand> validator,
        ICreateBoardHandler handler,
        ILogger<ProjectBoardModule> logger,
        CancellationToken ct = default
    ) =>
        validator.Validate(request).FoldAsync(
            errors => errors
                .Pipe(ErrorDetails.FieldErrorOnly)
                .Pipe(TypedResults.BadRequest),
            async command => (await handler.HandleCommand(command, ct)).Fold<CreateBoardResult>(
                error => error
                    .Pipe(ErrorDetails.GlobalErrorOnly)
                    .Pipe(TypedResults.Conflict),
                response =>
                    response
                        .Tap(r => CreateBoardLogs.LogCreated(logger, r.Id, r.Slug))
                        .Pipe(r => TypedResults.Created($"/boards/{r.Slug}", r))
            )
        );
}
