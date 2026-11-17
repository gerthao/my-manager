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
        var d = description.Map(static d => d.Trim()).FilterNot(static d => d == "");

        return d switch
        {
            Some<string>({ Length: > 255 }) => new ValidationError("description", "Board description cannot exceed 255 characters."),
            _ => d,
        };

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
