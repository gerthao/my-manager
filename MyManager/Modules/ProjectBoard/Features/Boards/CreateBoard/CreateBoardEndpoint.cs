using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

using CreateBoardResult = Results<BadRequest<ErrorDetails>, Conflict<ErrorDetails>, Created<CreateBoardResponse>>;

public static class CreateBoardEndpoint
{
    [MapPost( "/project-board/boards")]
    [WithName("CreateBoard")]
    [WithDescription("Create board")]
    [WithTags("Boards")]
    public static Task<CreateBoardResult> HandleEndpoint(
        [AsParameters] CreateBoardRequest request,
        [AsParameters] CreateBoardServices services,
        CancellationToken ct = default
    ) =>
        services.Validator.Validate(request).FoldAsync(
            errors => errors
                .Pipe(ErrorDetails.FieldErrorOnly)
                .Pipe(TypedResults.BadRequest),
            async command => (await services.Handler.Handle(command, ct)).Fold<CreateBoardResult>(
                error => error
                    .Pipe(ErrorDetails.GlobalErrorOnly)
                    .Pipe(TypedResults.Conflict),
                response =>
                    response
                        .Tap(r => CreateBoardLogs.LogCreated(services.Logger, r.Id, r.Slug))
                        .Pipe(r => TypedResults.Created($"/boards/{r.Slug}", r))
            )
        );
}
