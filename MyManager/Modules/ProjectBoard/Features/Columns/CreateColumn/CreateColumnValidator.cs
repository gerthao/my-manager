using MyManager.Attributes;
using MyManager.Common;

namespace MyManager.Modules.ProjectBoard.Features.Columns.CreateColumn;

[RegisterSingleton(typeof(IValidator<CreateColumnRequest, CreateColumnCommand>))]
public sealed class CreateColumnValidator : IValidator<CreateColumnRequest, CreateColumnCommand>
{
    private Validated<ValidationError, string> ValidateName(string name)
    {
        var trimmed = name.Trim();

        return trimmed.Length switch
        {
            > 100 => new ValidationError("name", "Column name cannot exceed 100 characters."),
            < 1 => new ValidationError("name", "Column name cannot be empty."),
            _ => trimmed,
        };
    }

    public Validated<ValidationError, CreateColumnCommand> Validate(CreateColumnRequest request) =>
        ValidateName(request.Body.Name).Map(name => new CreateColumnCommand(request.BoardId, name));
}
