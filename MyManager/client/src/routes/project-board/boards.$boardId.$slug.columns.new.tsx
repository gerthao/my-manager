import {createFileRoute} from '@tanstack/react-router'

export const Route = createFileRoute('/project-board/boards/$boardId/$slug/columns/new')({
    component: RouteComponent,
})

function RouteComponent() {
    return <div>Hello "/project-board/boards/$boardId/columns/new"!</div>
}
