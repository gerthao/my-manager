namespace MyManager.Common.Extensions;

public static class NonEmptyListExtensions
{
    extension<T>(T value) where T : notnull
    {
        public NonEmptyList<T> ToNonEmptyList() => NonEmptyList<T>.One(value);
    }

    extension<T>(List<T> list) where T : notnull
    {
        public Option<NonEmptyList<T>> ToNonEmptyList() =>
            NonEmptyList<T>.FromList(list);
    }

    extension<T>(T[] array) where T : notnull
    {
        public Option<NonEmptyList<T>> ToNonEmptyList() =>
            NonEmptyList<T>.FromArray(array);
    }
}
