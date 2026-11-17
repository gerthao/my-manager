import {createFileRoute, Link} from '@tanstack/react-router'
import {useQuery} from '@tanstack/react-query'
import {Plus} from 'iconoir-react'
import type {ListBoardsResponse} from '../../api/Api.ts'

export const Route = createFileRoute('/project-board/boards/')({
    component: ListBoardsComponent,
    head: () => ({
        meta: [
            {title: 'Boards: MyManager.Client'}
        ]
    }),
})

function ListBoardsComponent() {
    const {isPending, error, data} = useQuery({
        queryKey: ['boards'],
        queryFn: () =>
            fetch('/api/project-board/boards')
                .then((res) => res.json())
                .then(json => json as ListBoardsResponse[]),
    })

    if (isPending) return 'Loading...'

    if (error) return 'An error has occurred: ' + error.message

    return <div className="wrapper">
        <h1>Boards</h1>
        {/*<div style={{marginBottom: '1rem', display: 'flex', gap: '1rem', alignItems: 'center', justifyContent: 'space-between'}}>*/}
        {/*  <h1>Boards</h1>*/}
        {/*  <Link to="/project-board/boards/new"*/}
        {/*        style={{*/}
        {/*          textDecoration: 'none',*/}
        {/*          backgroundColor: 'var(--color-neutral-0)',*/}
        {/*          padding: '0.5em 0.75em',*/}
        {/*          borderRadius: '1rem',*/}
        {/*          boxShadow: '0 0 0 2px var(--color-neutral-100), 4px 4px 0 0 var(--color-neutral-100)'*/}
        {/*        }}>*/}
        {/*    + Add Board*/}
        {/*  </Link>*/}
        {/*</div>*/}
        {/*{*/}
        {/*  data.length == 0 &&*/}
        {/*    <div>*/}
        {/*        No boards created yet!*/}
        {/*    </div>*/}
        {/*}*/}
        <div style={{display: 'grid', gap: '1rem', gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))'}}>
            {
                data.map(board =>
                    <Link to={`/project-board/boards/${board.boardId}/${board.slug}`}
                          style={{
                              display: 'flex',
                              alignItems: 'center',
                              textDecoration: 'none',
                              color: 'var(--color-neutral-90)',
                              padding: '0.5rem',
                              borderRadius: '0.25rem',
                              boxShadow: '0 0 0 2px var(--color-neutral-100), 4px 4px 0 0 var(--color-neutral-100)'
                          }}
                    >
                        {board.name}
                    </Link>
                )
            }
            <Link to="/project-board/boards/new"
                  style={{
                      display: 'flex',
                      alignItems: 'center',
                      textDecoration: 'none',
                      padding: '0.5em',
                      backgroundColor: 'var(--color-neutral-30)',
                      color: 'var(--color-neutral-90)',
                      borderRadius: '0.25rem',
                      boxShadow: '0 0 0 2px var(--color-neutral-100), 4px 4px 0 0 var(--color-neutral-100)'
                  }}>
                <Plus/>
                Add Board
            </Link>
        </div>
    </div>
}
