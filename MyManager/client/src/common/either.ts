import { none, type Option, some } from "./option"
import { err, ok, type Result } from "./result"

export type Left<L, R> = { _tag: "left"; leftValue: L }
export type Right<L, R> = { _tag: "right"; rightValue: R }
export type Either<L, R> = Left<L, R> | Right<L, R>

export const left = <L, R>(value: L): Left<L, R> => ({
  _tag: "left" as const,
  leftValue: value,
})
export const right = <L, R>(value: R): Right<L, R> => ({
  _tag: "right" as const,
  rightValue: value,
})

export const isLeft = <L, R>(either: Either<L, R>): either is Left<L, R> => either._tag === "left"
export const isRight = <L, R>(either: Either<L, R>): either is Right<L, R> =>
  either._tag === "right"

export const Either = {
  swap: <L, R>(either: Either<L, R>): Either<R, L> => {
    return isLeft(either) ? right(either.leftValue) : left(either.rightValue)
  },

  map: <L, R, R1>(either: Either<L, R>, op: (value: R) => R1): Either<L, R1> => {
    return isLeft(either) ? left(either.leftValue) : right(op(either.rightValue))
  },

  flatmap: <L, R, R1>(either: Either<L, R>, op: (value: R) => Either<L, R1>) => {
    return isLeft(either) ? left(either.leftValue) : op(either.rightValue)
  },

  flatten: <L, R>(either: Either<L, Either<L, R>>) => {
    return isLeft(either) ? left(either.leftValue) : either.rightValue
  },

  filter: <L, R>(either: Either<L, R>, predicate: (value: R) => boolean, leftValue: () => L) => {
    return isLeft(either) || predicate(either.rightValue)
      ? left(leftValue())
      : right(either.rightValue)
  },

  getOrElse: <L, R>(either: Either<L, R>, defaultValue: () => R): R => {
    return isLeft(either) ? defaultValue() : either.rightValue
  },

  orElse: <L, R>(either: Either<L, R>, defaultValue: () => Either<L, R>): Either<L, R> => {
    return isLeft(either) ? defaultValue() : either
  },

  toOption: <L, R extends {}>(either: Either<L, R>): Option<R> => {
    return isLeft(either) ? none() : some(either.rightValue)
  },

  toResult: <L, R>(either: Either<L, R>): Result<R, L> => {
    return isLeft(either) ? err(either.leftValue) : ok(either.rightValue)
  },
}
