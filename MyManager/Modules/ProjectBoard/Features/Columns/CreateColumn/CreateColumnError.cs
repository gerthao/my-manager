using System.Runtime.CompilerServices;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Columns.CreateColumn;

[Union]
public readonly record struct CreateColumnError : IUnion {
    public object Value { get; }

    public CreateColumnError(BoardNotFound value) => Value = value;
    public CreateColumnError(ColumnAlreadyExists value) => Value = value;
}
