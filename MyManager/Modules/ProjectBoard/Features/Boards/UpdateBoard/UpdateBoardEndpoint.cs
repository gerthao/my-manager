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

    public readonly record struct Services(
        IValidator<UpdateBoardRequest, UpdateBoardCommand> validator,
        IRequestHandler<UpdateBoardCommand, Result<UpdateBoardResponse>> handler,
        ILogger<ProjectBoardModule> logger
    );

    public static Task<UpdateBoardEndpointResult> HandleEndpoint(
        [AsParameters] UpdateBoardRequest request,
        [AsParameters] Services services,
        CancellationToken ct = default
    ) => services.validator.Validate(request).FoldAsync(
        errors => ErrorDetails.FieldErrorOnly(errors).Pipe(TypedResults.BadRequest),
        async command => (await services.handler.Handle(command, ct)).Fold<UpdateBoardEndpointResult>(
            error => error
                .Pipe(ErrorDetails.GlobalErrorOnly).Pipe(TypedResults.BadRequest),
            updated => updated
                .Tap(u => UpdateBoardLogs.LogUpdated(services.logger, u.Id, u.Slug))
                .Pipe(TypedResults.Ok)
        )
    );
}
