using Microsoft.AspNetCore.Http.HttpResults;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

using UpdateBoardEndpointResult = Results<BadRequest<ErrorDetails>, Ok<UpdateBoardResponse>>;

public abstract class UpdateBoardEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/boards/{id}", HandleEndpoint)
            .WithName(nameof(UpdateBoardEndpoint));

    public static Task<UpdateBoardEndpointResult> HandleEndpoint(
        [AsParameters] UpdateBoardRequest request,
        IValidator<UpdateBoardRequest, UpdateBoardCommand> validator,
        IUpdateBoardHandler handler,
        ILogger<ProjectBoardModule> logger,
        CancellationToken ct = default
    ) => validator.Validate(request).FoldAsync(
        errors => ErrorDetails.FieldErrorOnly(errors).Pipe(TypedResults.BadRequest),
        async command => (await handler.HandleCommand(command, ct)).Fold<UpdateBoardEndpointResult>(
            error => error
                .Pipe(ErrorDetails.GlobalErrorOnly).Pipe(TypedResults.BadRequest),
            updated => updated
                .Tap(u => UpdateBoardLogs.LogUpdated(logger, u.Id, u.Slug))
                .Pipe(TypedResults.Ok)
        )
    );
}
