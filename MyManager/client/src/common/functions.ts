import { err, type Result } from "./result"
import { unknownError, type UnknownError } from "./unknown_error"
import type { ProblemDetails, ValidationProblemDetails } from "../api/Api.ts"

export const pipe =
  <T>(...fns: Array<(arg: T) => T>) =>
  (value: T) =>
    fns.reduce((acc, fn) => fn(acc), value)

type TryPromiseOptions<T, E> = {
  try: () => Promise<Result<T, E>>
}

type TryCatchPromiseOptions<T, E> = {
  try: () => Promise<Result<T, E>>
  catch: (unknown: unknown) => E
}

export const tryPromise = async <T, E>(
  t: TryPromiseOptions<T, E | UnknownError>,
): Promise<Result<T, E | UnknownError>> => {
  try {
    return await t.try()
  } catch (e) {
    return err(unknownError(e))
  }
}

export const tryCatchPromise = async <T, E>(
  t: TryCatchPromiseOptions<T, E>,
): Promise<Result<T, E>> => {
  try {
    return await t.try()
  } catch (e) {
    return err(t.catch(e))
  }
}

export const isValidationProblem = (
  problem: ProblemDetails | ValidationProblemDetails,
): problem is ValidationProblemDetails => {
  return "errors" in problem
}
