import { useMutation } from "@tanstack/react-query"
import type {
  Api,
  CreateBoardRequestBody,
  CreateBoardResponse,
  CreateColumnBody,
  CreateColumnResponse,
  ProblemDetails,
  ValidationProblemDetails,
} from "../../../api/Api"

export const useCreateBoardMutation = (projectBoardApi: Api<unknown>) =>
  useMutation<
    CreateBoardResponse,
    ProblemDetails | ValidationProblemDetails,
    CreateBoardRequestBody
  >({
    mutationFn: async (board: CreateBoardRequestBody) => {
      const response = await projectBoardApi.api.createBoard(board)
      if (!response.ok) throw response.error
      return response.data
    },
  })

export const useCreateColumnMutation = (projectBoardApi: Api<unknown>, boardId: number | string) =>
  useMutation<
    CreateColumnResponse,
    ProblemDetails | ValidationProblemDetails,
    { boardId: string; body: CreateColumnBody }
  >({
    mutationFn: async ({ boardId, body }) => {
      const response = await projectBoardApi.api.createColumn(boardId, body)
      if (!response.ok) throw response.error
      return response.data
    },
  })
