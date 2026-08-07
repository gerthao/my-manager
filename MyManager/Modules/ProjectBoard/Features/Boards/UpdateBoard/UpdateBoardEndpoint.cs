using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

using UpdateBoardEndpointResult = Results<BadRequest<ErrorDetails>, Ok<UpdateBoardResponse>>;

public abstract class UpdateBoardEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/boards/{id}", HandleEndpoint)
            .WithName(nameof(UpdateBoardEndpoint))
        ;

    [MapPut("/project-board/boards/{id}", "UpdateBoard")]
    public static Task<UpdateBoardEndpointResult> HandleEndpoint(
        [AsParameters] UpdateBoardRequest request,
        [AsParameters] UpdateBoardServices services,
        CancellationToken ct = default
    ) => services.Validator.Validate(request).FoldAsync(
        errors => ErrorDetails.FieldErrorOnly(errors).Pipe(TypedResults.BadRequest),
        async command => (await services.Handler.Handle(command, ct)).Fold<UpdateBoardEndpointResult>(
            error => error
                .Pipe(ErrorDetails.GlobalErrorOnly).Pipe(TypedResults.BadRequest),
            updated => updated
                .Tap(u => UpdateBoardLogs.LogUpdated(services.Logger, u.Id, u.Slug))
                .Pipe(TypedResults.Ok)
        )
    );
}
