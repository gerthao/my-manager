using MyManager.Common;

namespace MyManager.Modules.ProjectBoard.Domain.Errors;

public static class ColumnErrors
{
    public static Error AlreadyExists(string name) =>
        new("ProjectBoard.Column.AlreadyExists", $"Column {name} already exists");

    public static Error NotFound() => new("ProjectBoard.Column.NotFound", $"Column not found");
}
