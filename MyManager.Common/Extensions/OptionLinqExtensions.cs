namespace MyManager.Common.Extensions;

public static class OptionLinqExtensions
{
    extension<T>(Option<T> option) where T : notnull
    {
        public Option<TResult> Select<TResult>(Func<T, TResult> selector) where TResult : notnull =>
            option.Map(selector);


        public Option<TResult> SelectMany<TResult>(Func<T, Option<TResult>> binder) where TResult : notnull =>
            option.FlatMap(binder);

        // Enables: from x in optionA from y in optionB select x + y
        public Option<TResult> SelectMany<TBind, TResult>(
            Func<T, Option<TBind>> binder,
            Func<T, TBind, TResult> resultSelector)
            where TBind : notnull
            where TResult : notnull
        {
            return option.FlatMap(val => binder(val).Map(bindVal => resultSelector(val, bindVal)));
        }

        public Option<T> Where(Func<T, bool> predicate) => option.Fold(
            () => Option<T>.Empty,
            value => predicate(value) ? option : Option<T>.Empty
        );
    }

    extension<T>(Task<Option<T>> taskOption) where T : notnull
    {
        public Task<Option<TResult>> Select<TResult>(Func<T, TResult> selector) where TResult : notnull =>
            taskOption.MapAsync(selector);

        public Task<Option<TResult>> Select<TResult>(Func<T, Task<TResult>> selector) where TResult : notnull =>
            taskOption.MapAsync(selector);

        public Task<Option<TResult>> SelectMany<TResult>(Func<T, Option<TResult>> binder)
            where TResult : notnull =>
            taskOption.FlatMapAsync(binder);

        public Task<Option<TResult>> SelectMany<TResult>(Func<T, Task<Option<TResult>>> binder)
            where TResult : notnull =>
            taskOption.FlatMapAsync(binder);

        public Task<Option<TResult>> SelectMany<TBind, TResult>(
            Func<T, Option<TBind>> binder,
            Func<T, TBind, TResult> resultSelector
        )
            where TBind : notnull
            where TResult : notnull =>
            taskOption.FlatMapAsync(val => binder(val).Map(bindVal => resultSelector(val, bindVal)));

        public Task<Option<TResult>> SelectMany<TBind, TResult>(
            Func<T, Task<Option<TBind>>> binder,
            Func<T, TBind, TResult> resultSelector
        )
            where TBind : notnull
            where TResult : notnull =>
            taskOption.FlatMapAsync(val => binder(val).MapAsync(bindVal => resultSelector(val, bindVal)));

        public Task<Option<T>> Where(Func<T, bool> predicate) =>
            taskOption.FoldAsync(
                () => Option<T>.Empty,
                value => predicate(value) ? value : Option<T>.Empty
            );
    }
}
