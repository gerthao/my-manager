using System.Runtime.CompilerServices;
using MyManager.Modules.ProjectBoard.Domain.Errors;

namespace MyManager.Modules.ProjectBoard.Features.Boards.UpdateBoard;

[Union]
public readonly record struct UpdateBoardError : IUnion {
    public object Value { get; }

    public UpdateBoardError(BoardNotFound value) => Value = value;
    public UpdateBoardError(BoardUnchanged value) => Value = value;
}
