using MyManager.Attributes;
using MyManager.Common;
using MyManager.Common.Extensions;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

[RegisterSingleton]
public sealed class UpdateBoardValidator : IValidator<UpdateBoardRequest, UpdateBoardCommand>
{
    public Validated<ValidationError, UpdateBoardCommand> Validate(UpdateBoardRequest request) =>
    (
        ValidateName(request.Body.Name.ToOption()),
        ValidateDescription(request.Body.Description.ToOption())
    ).MapN((name, description) => new UpdateBoardCommand(request.BoardId, name, description));

    private static Validated<ValidationError, Option<string>> ValidateDescription(Option<string> description)
    {
        var d = description.Map(static d => d.Trim()).FilterNot(static d => d == "");

        return d switch
        {
            Some<string>({ Length: > 255 }) => new ValidationError("description", "Board description cannot exceed 255 characters."),
            _ => d,
        };
    }

    private static Validated<ValidationError, Option<string>> ValidateName(Option<string> name)
    {
        var n = name.Map(static n => n.Trim());

        return n switch
        {
            Some<string>(var value) when string.IsNullOrEmpty(value) => new ValidationError("name", "Board name must be at least 1 character long."),
            Some<string>({ Length: > 100 }) => new ValidationError("name", "Board name cannot exceed 100 characters."),
            _ => n,
        };
    }
}
