import { queryOptions } from "@tanstack/react-query"
import type { Api, RequestParams } from "../../../api/Api.ts"

export const listBoardsQuery = (api: Api<unknown>) =>
  queryOptions({
    queryKey: ["boards"],
    queryFn: async ({ signal }) => {
      const response = await api.api.listBoards({}, { signal })

      if (response.error) throw response.error
      return response.data
    },
  })

export const getBoardByIdQuery = (api: Api<unknown>, boardId: number | string) =>
  queryOptions({
    queryKey: ["boards", boardId],
    queryFn: async ({ signal }) => {
      const response = await api.api.getBoardById(boardId, { signal })

      if (response.error) throw response.error
      return response.data
    },
    staleTime: 30_000,
  })
