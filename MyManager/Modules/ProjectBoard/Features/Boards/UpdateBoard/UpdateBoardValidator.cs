using MyManager.Attributes;
using MyManager.Common;
using MyManager.Common.Extensions;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

[RegisterSingleton]
public sealed class UpdateBoardValidator : IValidator<UpdateBoardRequest, UpdateBoardCommand>
{
    public Validated<ValidationError, UpdateBoardCommand> Validate(UpdateBoardRequest request) =>
    (
        ValidateName(request.Body.Name),
        ValidateDescription(request.Body.Description.ToOption())
    ).MapN((name, description) => new UpdateBoardCommand(request.Id, name, description));

    private static Validated<ValidationError, Option<string>> ValidateDescription(Option<string> description)
    {
        var d = description.Map(d => d.Trim()).FilterNot(d => d == "");

        return Validated<ValidationError, Option<string>>.Condition(
            d is None || d.Exists(e => e.Trim().Length <= 255),
            () => d,
            () => new ValidationError("description", "Board description cannot exceed 255 characters.")
        );
    }

    private static Validated<ValidationError, string> ValidateName(string name)
    {
        var n = name.Trim();

        if (string.IsNullOrWhiteSpace(n))
            return new ValidationError("name", "Board name must be at least 1 character long.");

        if (n.Length > 100)
            return new ValidationError("name", "Board name cannot exceed 100 characters.");

        return n;
    }
}
