export type UnknownError = {
  _tag: "unknown"
  error: unknown
}

export function unknownError(error: unknown): UnknownError {
  return { _tag: "unknown" as const, error }
}
