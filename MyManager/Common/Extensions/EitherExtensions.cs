using System.Diagnostics;

namespace MyManager.Common.Extensions;

public static class EitherExtensions
{
    extension<T>(T value) where T : notnull
    {
        public Either<T, TR> ToLeft<TR>() where TR : notnull => new Left<T>(value);
        public Either<TL, T> ToRight<TL>() where TL : notnull => new Right<T>(value);
    }

    extension<TL, TR>(Either<TL, TR> either)
        where TL : notnull
        where TR : notnull
    {
        public Option<TR> ToOption() => either.Fold<Option<TR>>(
            _ => new None(),
            right => new Some<TR>(right)
        );

        public Option<TL> ToOptionLeft() => either.Fold<Option<TL>>(
            left => new Some<TL>(left),
            _ => new None()
        );

        public Task<Either<TL, TR>> ToTask() => Task.FromResult(either);

        public Validated<TL, TR> ToValidated() => either.Fold<Validated<TL, TR>>(
            left => NonEmptyList<TL>.One(left),
            right => right
        );

        public Validated<TE, TR> ToValidated<TE>(Func<TL, TE> mapLeft) where TE : notnull =>
            either.Fold<Validated<TE, TR>>(
                left => NonEmptyList<TE>.One(mapLeft(left)),
                right => right
            );
    }

    extension<TL, TR>(Either<TL, Either<TL, TR>> nestedEither)
        where TL : notnull
        where TR : notnull
    {
        public Either<TL, TR> Flatten() => nestedEither.Fold(
            left => left,
            right => right
        );
    }

    extension<TL, TR>(Task<Either<TL, TR>> taskEither)
        where TL : notnull
        where TR : notnull
    {
        public async Task<Either<TL, TR>> FilterOrElseAsync(Func<TR, bool> predicate, Func<TL> orElse)
        {
            var either = await taskEither.ConfigureAwait(false);

            return either.FilterOrElse(predicate, orElse);
        }

        public async Task<Either<TL, TR1>> FlatMapAsync<TR1>(Func<TR, Either<TL, TR1>> binder) where TR1 : notnull
        {
            var either = await taskEither.ConfigureAwait(false);

            return either.FlatMap(binder);
        }

        public async Task<Either<TL, TR1>> FlatMapAsync<TR1>(Func<TR, Task<Either<TL, TR1>>> binder) where TR1 : notnull
        {
            var either = await taskEither.ConfigureAwait(false);

            return either switch
            {
                Left<TL>(var left)   => left,
                Right<TR>(var right) => await binder(right).ConfigureAwait(false),
                _                        => throw new UnreachableException(),
            };
        }

        public async Task<TA> FoldAsync<TA>(Func<TL, TA> ifLeft, Func<TR, TA> ifRight)
        {
            var either = await taskEither.ConfigureAwait(false);

            return either.Fold(ifLeft, ifRight);
        }

        public async Task<TR> GetOrElseAsync(Func<TR> orElse)
        {
            var either = await taskEither.ConfigureAwait(false);

            return either.GetOrElse(orElse);
        }

        public async Task<Either<TL, TR1>> MapAsync<TR1>(Func<TR, TR1> mapper) where TR1 : notnull
        {
            var either = await taskEither.ConfigureAwait(false);

            return either.Map(mapper);
        }

        public async Task<Either<TL, TR1>> MapAsync<TR1>(Func<TR, Task<TR1>> mapper) where TR1 : notnull
        {
            var either = await taskEither.ConfigureAwait(false);

            return either switch
            {
                Left<TL> (var leftValue)   => new Left<TL>(leftValue),
                Right<TR> (var rightValue) => new Right<TR1>(await mapper(rightValue).ConfigureAwait(false)),
                _                              => throw new UnreachableException(),
            };
        }

        public async Task<Either<TL, TR>> OrElseAsync(Func<Either<TL, TR>> orElse)
        {
            var either = await taskEither.ConfigureAwait(false);

            return either.OrElse(orElse);
        }

        public async Task<Either<TR, TL>> SwapAsync()
        {
            var either = await taskEither.ConfigureAwait(false);

            return either.Swap;
        }

        public async Task<Either<TL, TR>> TapAsync(Action<TR> tap)
        {
            var either = await taskEither.ConfigureAwait(false);

            return either.Tap(tap);
        }

        public async Task<Either<TL, TR>> TapLeftAsync(Action<TL> tap)
        {
            var either = await taskEither.ConfigureAwait(false);

            return either.TapLeft(tap);
        }
    }
}
