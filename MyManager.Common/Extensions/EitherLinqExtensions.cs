namespace MyManager.Common.Extensions;

public static class EitherLinqExtensions
{
    extension<TL, TR>(Either<TL, TR> either) where TL : notnull where TR : notnull
    {
        public Either<TL, TResult> Select<TResult>(Func<TR, TResult> selector) where TResult : notnull =>
            either.Map(selector);


        public Either<TL, TResult> SelectMany<TResult>(Func<TR, Either<TL, TResult>> binder) where TResult : notnull =>
            either.FlatMap(binder);

        // Enables: from x in optionA from y in optionB select x + y
        public Either<TL, TResult> SelectMany<TBind, TResult>(
            Func<TR, Either<TL, TBind>> binder,
            Func<TR, TBind, TResult> resultSelector)
            where TBind : notnull
            where TResult : notnull
        {
            return either.FlatMap(val => binder(val).Map(bindVal => resultSelector(val, bindVal)));
        }

        public Either<TL, TR> Where(Func<TL> orElse, Func<TR, bool> predicate) => either.Fold<Either<TL, TR>>(
            left => new Left<TL, TR>(left),
            value => predicate(value) ? new Right<TL, TR>(value) : new Left<TL, TR>(orElse())
        );
    }

    extension<TL, TR>(Task<Either<TL, TR>> taskEither) where TL : notnull where TR : notnull
    {
        public Task<Either<TL, TResult>> Select<TResult>(Func<TR, TResult> selector) where TResult : notnull =>
            taskEither.MapAsync(selector);

        public Task<Either<TL, TResult>> Select<TResult>(Func<TR, Task<TResult>> selector) where TResult : notnull =>
            taskEither.MapAsync(selector);

        public Task<Either<TL, TResult>> SelectMany<TResult>(Func<TR, Either<TL, TResult>> binder)
            where TResult : notnull =>
            taskEither.FlatMapAsync(binder);

        public Task<Either<TL, TResult>> SelectMany<TResult>(Func<TR, Task<Either<TL, TResult>>> binder)
            where TResult : notnull =>
            taskEither.FlatMapAsync(binder);

        public Task<Either<TL, TResult>> SelectMany<TBind, TResult>(
            Func<TR, Either<TL, TBind>> binder,
            Func<TR, TBind, TResult> resultSelector
        )
            where TBind : notnull
            where TResult : notnull =>
            taskEither.FlatMapAsync(val => binder(val).Map(bindVal => resultSelector(val, bindVal)));

        public Task<Either<TL, TResult>> SelectMany<TBind, TResult>(
            Func<TR, Task<Either<TL, TBind>>> binder,
            Func<TR, TBind, TResult> resultSelector
        )
            where TBind : notnull
            where TResult : notnull =>
            taskEither.FlatMapAsync(val => binder(val).MapAsync(bindVal => resultSelector(val, bindVal)));

        public Task<Either<TL, TR>> Where(Func<TL> orElse, Func<TR, bool> predicate) =>
            taskEither.FoldAsync<TL, TR, Either<TL, TR>>(
                left => new Left<TL, TR>(left),
                value => predicate(value) ? new Right<TL, TR>(value) : new Left<TL, TR>(orElse())
            );
    }
}
