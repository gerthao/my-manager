import {queryOptions} from '@tanstack/react-query'
import type {Api} from '../../../api/Api.ts'

export const boardQuery = (api: Api<unknown>, id: number) => queryOptions({
    queryKey: ['boards', 'detail', id],
    queryFn: async ({signal}) => {
        const response = await api.api.getBoardById(id, {signal})

        if (response.error) throw response.error
        return response.data
    },
    staleTime: 30_000,
})
