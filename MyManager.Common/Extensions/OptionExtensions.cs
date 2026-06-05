namespace MyManager.Common.Extensions;

public static class OptionExtensions
{
    extension<T>(T? value) where T : notnull
    {
        public Option<T> ToOption() => value ?? Option<T>.Empty;
    }

    extension<TA, TB>(Option<(TA, TB)> option)
        where TA : notnull
        where TB : notnull
    {
        public (Option<TA>, Option<TB>) Unzip() => option switch
        {
            Some<(TA, TB)>(var (ta, tb)) => (new Some<TA>(ta), new Some<TB>(tb)),
            _                            => (Option<TA>.Empty, Option<TB>.Empty),
        };
    }

    extension<T>(Option<T> opt) where T : class
    {
        public T? OrNull() => opt switch
        {
            Some<T>(var value) => value,
            _                  => null,
        };
    }

    extension<T>(Option<T> opt) where T : struct
    {
        public T? OrNullable() => opt switch
        {
            Some<T>(var value) => value,
            _                  => null,
        };

        public T OrDefault() => opt switch
        {
            Some<T>(var value) => value,
            _                  => default,
        };
    }

    extension<T>(Option<T> option) where T : notnull
    {
        public Either<T, TR> ToLeft<TR>(Func<TR> rightFn) where TR : notnull => option.Fold<Either<T, TR>>(
            () => rightFn(),
            value => value
        );

        public Either<T, TR> ToLeft<TR>(TR right) where TR : notnull => option.Fold<Either<T, TR>>(
            () => right,
            value => value
        );

        public List<T> ToList() => option.Fold<List<T>>(
            () => [],
            value => [value]
        );

        public Result<T> ToResult(Func<Error> fn) => option.Fold<Result<T>>(
            () => fn(),
            value => value
        );

        public Result<T> ToResult(Error error) => option.Fold<Result<T>>(
            () => error,
            value => value
        );

        public Either<TL, T> ToRight<TL>(Func<TL> leftFn) where TL : notnull => option.Fold<Either<TL, T>>(
            () => leftFn(),
            value => value
        );

        public Either<TL, T> ToRight<TL>(TL left) where TL : notnull => option.Fold<Either<TL, T>>(
            () => left,
            value => value
        );

        public Task<Option<T>> ToTask() => Task.FromResult(option);

        public Validated<TE, T> ToValidated<TE>(Func<TE> mapNone) where TE : notnull =>
            option.Fold<Validated<TE, T>>(
                () => NonEmptyList<TE>.One(mapNone()),
                value => value
            );

        public Validated<TE, T> ToValidated<TE>(TE error) where TE : notnull =>
            option.Fold<Validated<TE, T>>(
                () => NonEmptyList<TE>.One(error),
                value => value
            );
    }

    extension<T>(Option<Option<T>> option) where T : notnull
    {
        public Option<T> Flatten() => option.Fold(
            () => Option<T>.Empty,
            innerOption => innerOption
        );
    }

    extension<T>(Task<Option<Option<T>>> t) where T : notnull
    {
        public async Task<Option<T>> FlattenAsync()
        {
            var nestedOption = await t.ConfigureAwait(false);
            return nestedOption.Flatten();
        }
    }

    extension<T>(Task<Option<T>> taskOption) where T : notnull
    {
        public async Task<Option<T>> FilterAsync(Func<T, bool> predicate)
        {
            var option = await taskOption.ConfigureAwait(false);
            return option.Filter(predicate);
        }

        public async Task<Option<T>> FilterNotAsync(Func<T, bool> predicate)
        {
            var option = await taskOption.ConfigureAwait(false);
            return option.FilterNot(predicate);
        }

        public async Task<Option<TA>> FlatMapAsync<TA>(Func<T, Option<TA>> binder) where TA : notnull
        {
            var option = await taskOption.ConfigureAwait(false);
            return option.FlatMap(binder);
        }

        public async Task<Option<TA>> FlatMapAsync<TA>(Func<T, Task<Option<TA>>> binder) where TA : notnull
        {
            var option = await taskOption.ConfigureAwait(false);

            return option switch
            {
                Some<T>(var val) => await binder(val).ConfigureAwait(false),
                _                => new None<TA>(),
            };
        }

        public async Task<TA> FoldAsync<TA>(Func<TA> ifEmpty, Func<T, TA> operation) where TA : notnull
        {
            var option = await taskOption.ConfigureAwait(false);
            return option.Fold(ifEmpty, operation);
        }

        public async Task<T> GetOrElseAsync(Func<T> defaultValue)
        {
            var option = await taskOption.ConfigureAwait(false);
            return option.GetOrElse(defaultValue);
        }

        public async Task<Option<TA>> MapAsync<TA>(Func<T, Task<TA>> mapper) where TA : notnull
        {
            var option = await taskOption.ConfigureAwait(false);

            return option switch
            {
                Some<T>(var val) => new Some<TA>(await mapper(val).ConfigureAwait(false)),
                _                => new None<TA>(),
            };
        }

        public async Task<Option<TA>> MapAsync<TA>(Func<T, TA> mapper) where TA : notnull
        {
            var option = await taskOption.ConfigureAwait(false);
            return option.Map(mapper);
        }


        public async Task<Option<T>> OrElseAsync(Func<Option<T>> orElse)
        {
            var option = await taskOption.ConfigureAwait(false);
            return option.OrElse(orElse);
        }

        public async Task<List<T>> ToListAsync()
        {
            var option = await taskOption.ConfigureAwait(false);
            return option.ToList();
        }

        public async Task<Result<T>> ToResult(Func<Error> fn)
        {
            var option = await taskOption.ConfigureAwait(false);
            return option.ToResult(fn);
        }
    }
}
