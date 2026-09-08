namespace MyManager.Common.Extensions;

public static class ResultLinqExtensions
{
    extension<T>(Result<T> result) where T : notnull
    {
        public Result<TResult> Select<TResult>(Func<T, TResult> selector) where TResult : notnull =>
            result.Map(selector);

        public Result<TResult> SelectMany<TResult>(Func<T, Result<TResult>> binder) where TResult : notnull =>
            result.FlatMap(binder);

        public Result<TResult>
            SelectMany<TBind, TResult>(Func<T, Result<TBind>> binder, Func<T, TBind, TResult> resultSelector)
            where TBind : notnull where TResult : notnull =>
            result.FlatMap(val => binder(val).Map(bindVal => resultSelector(val, bindVal)));

        public Result<T> Where(Func<T, bool> predicate) => result.Filter(predicate);
    }

    extension<T>(Task<Result<T>> taskResult) where T : notnull
    {
        public Task<Result<TResult>> Select<TResult>(Func<T, TResult> selector) where TResult : notnull =>
            taskResult.MapAsync(selector);

        public Task<Result<TResult>> Select<TResult>(Func<T, Task<TResult>> selector) where TResult : notnull =>
            taskResult.MapAsync(selector);

        public Task<Result<TResult>> SelectMany<TResult>(Func<T, Result<TResult>> selector) where TResult : notnull =>
            taskResult.FlatMapAsync(selector);

        public Task<Result<TResult>> SelectMany<TResult>(Func<T, Task<Result<TResult>>> selector)
            where TResult : notnull =>
            taskResult.FlatMapAsync(selector);

        public Task<Result<TResult>> SelectMany<TBind, TResult>(
            Func<T, Result<TBind>> binder,
            Func<T, TBind, TResult> resultSelector
        )
            where TBind : notnull
            where TResult : notnull =>
            taskResult.FlatMapAsync(val => binder(val).Map(bindVal => resultSelector(val, bindVal)));

        public Task<Result<TResult>> SelectMany<TBind, TResult>(
            Func<T, Task<Result<TBind>>> binder,
            Func<T, TBind, TResult> resultSelector
        )
            where TBind : notnull
            where TResult : notnull =>
            taskResult.FlatMapAsync(val => binder(val).MapAsync(bindVal => resultSelector(val, bindVal)));

        public Task<Result<T>> Where(Func<T, bool> predicate) =>
            taskResult.FoldAsync<T, Result<T>>(
                error => new Failure<T>(error),
                value => predicate(value) ? value : new Failure<T>(Error.Empty)
            );
    }
}
