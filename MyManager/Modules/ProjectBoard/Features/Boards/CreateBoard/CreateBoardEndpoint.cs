using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Common.Extensions;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

using CreateBoardResult = Results<BadRequest<ErrorDetails>, Conflict<ErrorDetails>, Created<CreateBoardResponse>>;

public static class CreateBoardEndpoint
{
    [MapPost( "/project-board/boards")]
    public static Task<CreateBoardResult> HandleEndpoint(
        [AsParameters] CreateBoardRequest request,
        [AsParameters] CreateBoardServices services,
        CancellationToken ct = default
    ) =>
        services.validator.Validate(request).FoldAsync(
            errors => errors
                .Pipe(ErrorDetails.FieldErrorOnly)
                .Pipe(TypedResults.BadRequest),
            async command => (await services.handler.Handle(command, ct)).Fold<CreateBoardResult>(
                error => error
                    .Pipe(ErrorDetails.GlobalErrorOnly)
                    .Pipe(TypedResults.Conflict),
                response =>
                    response
                        .Tap(r => CreateBoardLogs.LogCreated(services.logger, r.Id, r.Slug))
                        .Pipe(r => TypedResults.Created($"/boards/{r.Slug}", r))
            )
        );
}
