import { type Either, left, type Left, right, type Right } from "./either"

export type Both<L, R> = { _tag: "both"; leftValue: L; rightValue: R }

export type Ior<L, R> = Left<L, R> | Right<L, R> | Both<L, R>

export const both = <L, R>(leftValue: L, rightValue: R): Ior<L, R> => {
  return { _tag: "both" as const, leftValue, rightValue }
}

export const Ior = {
  toEither: <L, R>(ior: Ior<L, R>): Either<L, R> =>
    ior._tag === "left" ? left(ior.leftValue) : right(ior.rightValue),
}
