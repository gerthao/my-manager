using MyManager.Attributes;
using MyManager.Common;
using MyManager.Common.Extensions;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

[RegisterSingleton]
public sealed class UpdateBoardValidator : IValidator<UpdateBoardRequest, UpdateBoardCommand>
{
    public Validated<ValidationError, UpdateBoardCommand> Validate(UpdateBoardRequest request) => (
        ValidateName(request.Body.Name),
        ValidateDescription(request.Body.Description.ToOption())
    ).MapN((name, description) => new UpdateBoardCommand(request.Id, name, description));

    private static Validated<ValidationError, Option<string>> ValidateDescription(Option<string> description) =>
        Validated<ValidationError, Option<string>>.Condition(
            description.IsEmpty || description.Exists(d => d.Trim().Length <= 255),
            () => description.Map(d => d.Trim()),
            () => new ValidationError("description", "Project description cannot exceed 255 characters.")
        );

    private static Validated<ValidationError, string> ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new ValidationError("name", "Project name must be at least 1 character long.");

        if (name.Length > 100)
            return new ValidationError("name", "Project name cannot exceed 100 characters.");

        return name.Trim();
    }
}
