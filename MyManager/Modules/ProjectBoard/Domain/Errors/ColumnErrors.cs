using System.Diagnostics;
using System.Runtime.CompilerServices;
using MyManager.Attributes;
using MyManager.Common;

namespace MyManager.Modules.ProjectBoard.Domain.Errors;

public static class ColumnErrors
{
    public record AlreadyExists(string Name)
        : Error("ProjectBoard.Column.AlreadyExists", $"Column {Name} already exists")
    {
        public string Name { get; init; } = Name;
    }

    public record NotFound(): Error("ProjectBoard.Column.NotFound", "Column not found");

    public record HasCards() : Error("ProjectBoard.Column.HasCards", "Column cannot be deleted until cards are moved");
}

// [UnionType]
// public partial record ColumnError
// {
//     public record AlreadyExists(string Name);
//
//     public record NotFound();
//
//     public record CannotDelete();
// }
//
// partial record ColumnError
// {
//     private object? _value;
//
//     private ColumnError()
//     {
//     }
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public T Match<T>(
//         Func<AlreadyExists, T> alreadyExists,
//         Func<NotFound, T> notFound,
//         Func<CannotDelete, T> cannotDelete
//     ) =>
//         _value switch
//         {
//             AlreadyExists v1 => alreadyExists(v1),
//             NotFound v2 => notFound(v2),
//             CannotDelete v3 => cannotDelete(v3),
//             _ => throw new UnreachableException(),
//         };
//
//     public static implicit operator ColumnError (AlreadyExists alreadyExists)
//     {
//         var e = new ColumnError
//         {
//             _value = alreadyExists,
//         };
//         return e;
//     }
//
//     public static implicit operator ColumnError (NotFound notFound)
//     {
//         var e = new ColumnError
//         {
//             _value = notFound,
//         };
//         return e;
//     }
//
//     public static implicit operator ColumnError (CannotDelete cannotDelete)
//     {
//         var e = new ColumnError
//         {
//             _value = cannotDelete,
//         };
//         return e;
//     }
// }
