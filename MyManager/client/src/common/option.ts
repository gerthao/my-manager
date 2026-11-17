import {err, ok, type Result} from './result'
import {type Either, left, right} from './either'

export type Some<T extends {}> = { _tag: 'some'; value: T };
export type None = { _tag: 'none' };
export type Option<T extends {}> = Some<T> | None;

export const some = <T extends {}>(value: T): Some<T> => ({
    _tag: 'some' as const,
    value,
})
export const none = (): None => ({_tag: 'none' as const})
export const option = <T>(
    value: T | null | undefined,
): Option<NonNullable<T>> => {
    return value == null ? none() : some(value)
}

export const isSome = <T extends {}>(option: Option<T>): option is Some<T> =>
    option._tag === 'some'
export const isNone = <T extends {}>(option: Option<T>): option is None =>
    option._tag === 'none'

export const Option = {
    flatten: <T extends {}>(option: Option<Option<T>>) =>
        isNone(option) ? none() : option.value,

    map: <T extends {}, U extends {}>(option: Option<T>, op: (value: T) => U) =>
        isNone(option) ? none() : some(op(option.value)),

    flatmap: <T extends {}, U extends {}>(
        option: Option<T>,
        op: (value: T) => Option<U>,
    ) => (isNone(option) ? none() : op(option.value)),

    toArray: <T extends {}>(option: Option<T>): Array<T> =>
        isNone(option) ? [] : [option.value],

    toResult: <T extends {}, E>(
        option: Option<T>,
        ifNone: () => E,
    ): Result<T, E> => (isNone(option) ? err(ifNone()) : ok(option.value)),

    toEither: <L, T extends {}>(
        option: Option<T>,
        leftValue: () => L,
    ): Either<L, T> => (isNone(option) ? left(leftValue()) : right(option.value)),
}
