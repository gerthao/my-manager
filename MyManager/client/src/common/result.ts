import {type Either, left, right} from './either'
import {none, type Option, some} from './option'

export type Ok<T> = { _tag: 'ok'; value: T };
export type Err<E> = { _tag: 'err'; error: E };
export type Result<T, E> = Ok<T> | Err<E>;

export const ok = <T>(value: T): Ok<T> => ({_tag: 'ok' as const, value})
export const err = <E>(error: E): Err<E> => ({_tag: 'err' as const, error})

export const isOk = <T, E>(result: Result<T, E>): result is Ok<T> =>
    result._tag === 'ok'
export const isErr = <T, E>(result: Result<T, E>): result is Err<E> =>
    result._tag === 'err'

export const Result = {
    flatten: <T, E>(result: Result<Result<T, E>, E>): Result<T, E> => {
        return isErr(result) ? err(result.error) : result.value
    },

    fold: <T, E, U>(
        result: Result<T, E>,
        foldError: (e: E) => U,
        foldOk: (x: T) => U,
    ) => {
        return isErr(result) ? foldError(result.error) : foldOk(result.value)
    },

    flatmap: <T, E, U>(
        result: Result<T, E>,
        op: (x: T) => Result<U, E>,
    ): Result<U, E> => {
        return isErr(result) ? err(result.error) : op(result.value)
    },

    filter: <T, E>(
        result: Result<T, E>,
        op: (x: T) => boolean,
        provideErr: () => E,
    ): Result<T, E> => {
        return isErr(result) || !op(result.value)
            ? err(provideErr())
            : ok(result.value)
    },

    map: <T, E, U>(result: Result<T, E>, op: (x: T) => U): Result<U, E> => {
        return isErr(result) ? err(result.error) : ok(op(result.value))
    },

    toOption: <T extends {}, E>(result: Result<T, E>): Option<T> => {
        return isErr(result) ? none() : some(result.value)
    },

    toEither: <E, T>(result: Result<T, E>): Either<E, T> => {
        return isErr(result) ? left(result.error) : right(result.value)
    },
}
