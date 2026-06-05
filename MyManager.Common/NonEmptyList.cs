using System.Collections;

namespace MyManager.Common;

public sealed record NonEmptyList<T>(T Head, IReadOnlyList<T> Tail) : IReadOnlyList<T> where T : notnull
{
    public NonEmptyList(T head) : this(head, [])
    {
    }

    public int Count => 1 + Tail.Count;

    public T this[int index] => index == 0 ? Head : Tail[index - 1];

    public IEnumerator<T> GetEnumerator()
    {
        yield return Head;
        foreach (var item in Tail) yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public NonEmptyList<T> Append(T item) => new(Head, [..Tail, item]);

    public NonEmptyList<T> Concat(NonEmptyList<T> other)
    {
        List<T> newTail = [..Tail, other.Head];
        newTail.AddRange(other.Tail);
        return new NonEmptyList<T>(Head, newTail);
    }

    public static Option<NonEmptyList<T>> FromArray(T[] array) =>
        array.Length != 0
            ? new NonEmptyList<T>(array[0], array.Skip(1).ToArray())
            : Option<NonEmptyList<T>>.Empty;

    public static Option<NonEmptyList<T>> FromList(List<T> list) =>
        list.Count != 0
            ? new NonEmptyList<T>(list[0], list.Skip(1).ToArray())
            : Option<NonEmptyList<T>>.Empty;

    public static NonEmptyList<T> Of(T head, params T[] tail) => new(head, tail);

    public static NonEmptyList<T> OfInitLast(List<T> list, T lastItem) =>
        new(list[0], list.Skip(1).Append(lastItem).ToArray());

    public static NonEmptyList<T> One(T item) => new(item);

    public NonEmptyList<T> Prepend(T item) => new(item, [Head, ..Tail]);

    public T[] ToArray() => ToList().ToArray();

    public List<T> ToList()
    {
        List<T> list = new(Count) { Head };
        list.AddRange(Tail);
        return list;
    }
}
