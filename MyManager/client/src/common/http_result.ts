import type { ProblemDetails, ValidationProblemDetails } from "../dto/problem_details.ts"

export class ApiError extends Error {
  public status: number
  public body: ProblemDetails | ValidationProblemDetails

  constructor(status: number, body: ProblemDetails | ValidationProblemDetails) {
    super(body.title)
    this.status = status
    this.body = body
  }
}
