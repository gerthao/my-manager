using System.Diagnostics;

namespace MyManager.Common.Extensions;

public static class ResultExtensions
{
    extension(Error error)
    {
        public Result<T> ToResult<T>() where T : notnull => new Failure<T>(error);
    }

    extension<T>(Result<T> result) where T : notnull
    {
        public Either<Error, T> ToEither() => result.Fold<Either<Error, T>>(
            error => new Left<Error, T>(error),
            value => new Right<Error, T>(value)
        );

        public Option<T> ToOption() => result.Fold<Option<T>>(
            _ => new None<T>(),
            value => new Some<T>(value)
        );

        public Task<Result<T>> ToTask() => Task.FromResult(result);

        public Validated<Error, T> ToValidated() => result.Fold<Validated<Error, T>>(
            error => NonEmptyList<Error>.One(error),
            value => value
        );

        public Validated<TE, T> ToValidated<TE>(Func<Error, TE> mapError) where TE : notnull =>
            result.Fold<Validated<TE, T>>(
                error => NonEmptyList<TE>.One(mapError(error)),
                value => value
            );

        public static async Task<Result<T>> TryAsync<TException>(Func<Task<T>> action, Func<TException, bool> when,
                                                                 Func<TException, Error> mapErr)
            where TException : Exception
        {
            try
            {
                return await action();
            }
            catch (Exception ex) when (ex is TException exception && when(exception) && ex.IsNonFatal())
            {
                return mapErr(exception);
            }
        }

        public static async Task<Result<T>> TryAsync<TException>(Func<Task<T>> action,
                                                                 Func<TException, Error> mapErr)
            where TException : Exception
        {
            try
            {
                return await action();
            }
            catch (Exception ex) when (ex is TException exception && ex.IsNonFatal())
            {
                return mapErr(exception);
            }
        }
    }

    extension<T>(Result<Result<T>> result) where T : notnull
    {
        public Result<T> Flatten() => result.Fold(
            error => new Failure<T>(error),
            innerResult => innerResult
        );
    }

    extension<T>(Task<Result<T>> taskResult) where T : notnull
    {
        public async Task<Result<T>> CatchAsync<TException>(Func<TException, Error> mapErr)
            where TException : Exception
        {
            try
            {
                return await taskResult;
            }
            catch (Exception ex) when (ex is TException exception && ex.IsNonFatal())
            {
                return mapErr(exception);
            }
        }

        public async Task<Result<T>> CatchAsync<TException>(Func<TException, bool> when, Func<TException, Error> mapErr)
            where TException : Exception
        {
            try
            {
                return await taskResult;
            }
            catch (Exception ex) when (ex is TException exception && when(exception) && ex.IsNonFatal())
            {
                return mapErr(exception);
            }
        }

        public async Task<Result<T>> FilterAsync(Func<T, bool> predicate)
        {
            var result = await taskResult.ConfigureAwait(false);
            return result.Filter(predicate);
        }

        public async Task<Result<T>> FilterNotAsync(Func<T, bool> predicate)
        {
            var result = await taskResult.ConfigureAwait(false);
            return result.FilterNot(predicate);
        }

        public async Task<Result<TA>> FlatMapAsync<TA>(Func<T, Result<TA>> flatmap) where TA : notnull
        {
            var result = await taskResult.ConfigureAwait(false);
            return result.FlatMap(flatmap);
        }

        public async Task<Result<TA>> FlatMapAsync<TA>(Func<T, Task<Result<TA>>> flatmap) where TA : notnull
        {
            var result = await taskResult.ConfigureAwait(false);

            return result switch
            {
                Failure<T>(var error) => new Failure<TA>(error),
                Success<T>(var value) => await flatmap(value).ConfigureAwait(false),
                _                     => throw new UnreachableException(),
            };
        }

        public async Task<TA> FoldAsync<TA>(Func<Error, TA> ifFailure, Func<T, TA> ifSuccess) where TA : notnull
        {
            var result = await taskResult.ConfigureAwait(false);
            return result.Fold(ifFailure, ifSuccess);
        }

        public async Task<T> GetOrElseAsync(Func<T> or)
        {
            var result = await taskResult.ConfigureAwait(false);
            return result.GetOrElse(or);
        }

        public async Task<Result<TA>> MapAsync<TA>(Func<T, TA> mapper) where TA : notnull
        {
            var result = await taskResult.ConfigureAwait(false);
            return result.Map(mapper);
        }

        public async Task<Result<TA>> MapAsync<TA>(Func<T, Task<TA>> mapper) where TA : notnull
        {
            var result = await taskResult.ConfigureAwait(false);

            return result switch
            {
                Failure<T>(var error) => new Failure<TA>(error),
                Success<T>(var value) => new Success<TA>(await mapper(value).ConfigureAwait(false)),
                _                     => throw new UnreachableException(),
            };
        }

        public async Task<Result<T>> OrElseAsync(Func<Result<T>> orElse)
        {
            var result = await taskResult.ConfigureAwait(false);
            return result.OrElse(orElse);
        }

        public async Task<Result<T>> TapAsync(Action<T> tap)
        {
            var result = await taskResult.ConfigureAwait(false);
            return result.Tap(tap);
        }

        public async Task<Result<T>> TapFailureAsync(Action<Error> tap)
        {
            var result = await taskResult.ConfigureAwait(false);
            return result.TapFailure(tap);
        }
    }
}
