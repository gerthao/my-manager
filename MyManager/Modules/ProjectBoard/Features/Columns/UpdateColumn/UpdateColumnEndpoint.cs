using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;
using MyManager.Modules.ProjectBoard.Domain.Errors;
using MyManager.Routing.Attributes;

namespace MyManager.Modules.ProjectBoard.Features.Columns.UpdateColumn;

using UpdateColumnResult = Results<
    BadRequest<ValidationProblemDetails>,
    NotFound<ProblemDetails>,
    NoContent,
    Ok<UpdateColumnResponse>
>;

public static class UpdateColumnEndpoint
{
    [MapPatch("/api/project-board/boards/{boardId:int}/columns/{columnId:int}")]
    [WithTags("Columns")]
    [WithName("UpdateColumn")]
    [WithSummary("Updates an existing column for a board")]
    public static async Task<UpdateColumnResult> HandleEndpoint(
        [AsParameters] UpdateColumnRequest request,
        [AsParameters] UpdateColumnServices services,
        CancellationToken ct = default) =>
        services.Validator.Validate(request) switch
        {
            Invalid<ValidationError>(var errors) => errors
                .AsValidationProblem(
                    title: "Column could not be updated",
                    detail:
                    $"Could not update column with id {request.ColumnId} with board id {request.BoardId}. Request is invalid.",
                    instance: $"/api/project-board/boards/{request.BoardId}/columns/{request.ColumnId}",
                    extensions: new() { ["traceId"] = Activity.Current?.Id })
                .Pipe(TypedResults.BadRequest),
            Valid<UpdateColumnCommand>(var command) => await services.Handler.Handle(command, ct) switch
            {
                Left<UpdateColumnError>(ColumnUnchanged unchanged) => TypedResults.NoContent(),
                Left<UpdateColumnError>(ColumnNotFound notFound) => new ProblemDetails()
                {
                    Title = "Column could not be updated",
                    Detail =
                        $"Could not update column with id {request.ColumnId} with board id {request.BoardId}. Column does not exist.",
                    Instance = $"/api/project-board/boards/{request.BoardId}/columns/{request.ColumnId}",
                    Extensions = { ["traceId"] = Activity.Current?.Id },
                }.Pipe(TypedResults.NotFound),
                Right<UpdateColumnResponse>(var response) => response.Pipe(TypedResults.Ok),
            },
        };
}

public sealed record UpdateColumnRequest(
    [FromRoute] int BoardId,
    [FromRoute] int ColumnId,
    [FromBody] UpdateColumnRequestBody Body
);

public sealed record UpdateColumnRequestBody(
    string? Name
);

public sealed record UpdateColumnCommand(
    int BoardId,
    int ColumnId,
    Option<string> Name
);

public sealed record UpdateColumnResponse(
    int BoardId,
    int ColumnId,
    string Name,
    DateTimeOffset ModifiedAt
);

public readonly record struct UpdateColumnServices(
    IValidator<UpdateColumnRequest, UpdateColumnCommand> Validator,
    IRequestHandler<UpdateColumnCommand, Either<UpdateColumnError, UpdateColumnResponse>> Handler,
    ILogger<ProjectBoardModule> Logger
);

[Union]
public sealed record UpdateColumnError : IUnion
{
    public object Value { get; }

    public UpdateColumnError(ColumnNotFound value) => Value = value;
    public UpdateColumnError(ColumnUnchanged value) => Value = value;
}

[RegisterScoped]
public sealed class UpdateColumnHandler(ProjectBoardDbContext db)
    : IRequestHandler<UpdateColumnCommand, Either<UpdateColumnError, UpdateColumnResponse>>
{
    public async Task<Either<UpdateColumnError, UpdateColumnResponse>> Handle(UpdateColumnCommand command,
        CancellationToken ct = default)
    {
        if (command.Name is None)
            return new UpdateColumnError(new ColumnUnchanged());

        var column =
            await db.Columns.FirstOrDefaultAsync(c => c.BoardId == command.BoardId && c.ColumnId == command.ColumnId,
                ct);

        if (column is null)
            return new UpdateColumnError(new ColumnNotFound());

        if (command.Name is Some<string>(var n))
        {
            column.Name = n;
        }

        await db.SaveChangesAsync(ct);

        return new UpdateColumnResponse(column.BoardId, column.ColumnId, column.Name, column.ModifiedAt);
    }
}

[RegisterSingleton]
public sealed class UpdateColumnValidator : IValidator<UpdateColumnRequest, UpdateColumnCommand>
{
    public Validated<ValidationError, UpdateColumnCommand> Validate(UpdateColumnRequest request) =>
        ValidateName(request.Body.Name.ToOption())
            .Map(name => new UpdateColumnCommand(request.BoardId, request.ColumnId, name));

    private static Validated<ValidationError, Option<string>> ValidateDescription(Option<string> description) =>
        description.Map(static d => d.Trim()) switch
        {
            Some<string>({ Length: > 255 }) => new ValidationError("description",
                "Board description cannot exceed 255 characters."),
            var d => d,
        };

    private static Validated<ValidationError, Option<string>> ValidateName(Option<string> name) =>
        name.Map(static n => n.Trim()) switch
        {
            Some<string>({ Length: > 100 }) => new ValidationError("Name",
                "Column name cannot be longer than 100 characters."),
            Some<string>({ Length: 0 }) => new ValidationError("Name",
                "Column name cannot be left blank."),
            var n => n,
        };
}
