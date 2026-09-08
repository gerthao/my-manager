using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyManager.Attributes;
using MyManager.Common;
using MyManager.Common.Extensions;
using MyManager.Http;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Columns.UpdateColumn;

using UpdateColumnResult = Results<BadRequest<ErrorDetails>, NotFound<ErrorDetails>, Ok<UpdateColumnResponse>>;

public static class UpdateColumnEndpoint
{
    public static Task<UpdateColumnResult> HandleEndpoint(
        [AsParameters] UpdateColumnRequest request,
        [AsParameters] UpdateColumnServices services,
        CancellationToken ct = default) =>
        services.Validator.Validate(request).FoldAsync(
            errors => errors.Pipe(ErrorDetails.FieldErrorOnly).Pipe(TypedResults.BadRequest),
            async command => (await services.Handler.Handle(command, ct)).Fold<UpdateColumnResult>(
                error => error.Pipe(ErrorDetails.GlobalErrorOnly).Pipe(TypedResults.NotFound),
                response => TypedResults.Ok(response)
            )
        );
}

public sealed record UpdateColumnRequest(
    [FromRoute] int BoardId,
    [FromRoute] int ColumnId,
    [FromBody] UpdateColumnRequestBody Body
);

public sealed record UpdateColumnRequestBody(
    string Name,
    string? Description
);

public sealed record UpdateColumnCommand(
    int BoardId,
    int ColumnId,
    string Name,
    Option<string> Description
);

public sealed record UpdateColumnResponse(
    int BoardId,
    int ColumnId,
    string Name,
    string? Description,
    DateTimeOffset ModifiedAt
);

public readonly record struct UpdateColumnServices(
    IValidator<UpdateColumnRequest, UpdateColumnCommand> Validator,
    IRequestHandler<UpdateColumnCommand, Result<UpdateColumnResponse>> Handler,
    ILogger<ProjectBoardModule> Logger
);

[RegisterScoped]
public sealed class UpdateColumnHandler(ProjectBoardDbContext db)
    : IRequestHandler<UpdateColumnCommand, Result<UpdateColumnResponse>>
{
    public async Task<Result<UpdateColumnResponse>> Handle(UpdateColumnCommand request, CancellationToken ct = default)
    {
        var column =
            await db.Columns.FirstOrDefaultAsync(c => c.BoardId == request.BoardId && c.Id == request.ColumnId, ct);

        if (column is null) return new ColumnErrors.NotFound();

        column.Name = request.Name;
        column.Description = request.Description.OrNull();

        await db.SaveChangesAsync(ct);

        return new UpdateColumnResponse(column.BoardId, column.Id, column.Name, column.Description, column.ModifiedAt);
    }
}

[RegisterSingleton]
public sealed class UpdateColumnValidator : IValidator<UpdateColumnRequest, UpdateColumnCommand>
{
    public Validated<ValidationError, UpdateColumnCommand> Validate(UpdateColumnRequest request) =>
    (
        ValidateName(request.Body.Name),
        ValidateDescription(request.Body.Description.ToOption())
    ).MapN((name, description) => new UpdateColumnCommand(request.BoardId, request.ColumnId, name, description));

    private static Validated<ValidationError, Option<string>> ValidateDescription(Option<string> description) =>
        Validated<ValidationError, Option<string>>.Condition(
            description is None || description.Exists(d => d.Trim().Length <= 255),
            () => description.Map(d => d.Trim()).FilterNot(d => d == ""),
            () => new ValidationError("description", "Board description cannot exceed 255 characters.")
        );

    private static Validated<ValidationError, string> ValidateName(string name)
    {
        var n = name.Trim();

        if (string.IsNullOrWhiteSpace(n))
            return new ValidationError("Name", "Column name cannot be empty.");

        if (n.Length > 100)
            return new ValidationError("Name", "Column name cannot be longer than 100 characters.");

        return n;
    }
}
