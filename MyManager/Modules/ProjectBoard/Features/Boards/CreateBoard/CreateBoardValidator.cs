using MyManager.Attributes;
using MyManager.Common;
using MyManager.Common.Extensions;

namespace MyManager.Modules.ProjectBoard.Features.Boards.CreateBoard;

[RegisterSingleton]
public sealed class CreateBoardValidator : IValidator<CreateBoardRequest, CreateBoardCommand>
{
    public Validated<ValidationError, CreateBoardCommand> Validate(CreateBoardRequest request) => (
        ValidateName(request.Body.Name),
        ValidateDescription(request.Body.Description.ToOption())
    ).MapN(CreateBoardCommand.Apply);

    private static Validated<ValidationError, Option<string>> ValidateDescription(Option<string> description)
    {
        var desc = description.Map(d => d.Trim());

        return Validated<ValidationError, Option<string>>.Condition(
            desc is None || desc.Exists(d => d.Length <= 255),
            () => description.FilterNot(d => d == ""),
            () => new ValidationError("description", "Board description cannot exceed 255 characters.")
        );
    }

    private static Validated<ValidationError, string> ValidateName(string name)
    {
        var n = name?.Trim();

        if (string.IsNullOrWhiteSpace(n))
            return new ValidationError("name", "Board name must be at least 1 character long.");

        if (n.Length > 100)
            return new ValidationError("name", "Board name cannot exceed 100 characters.");

        return n;
    }
}
