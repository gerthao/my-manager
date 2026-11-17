import {createFileRoute, notFound, Outlet} from '@tanstack/react-router'

// export const Route = createFileRoute('/project-board/boards/$boardId')({
    // beforeLoad: async ({params, context}) => {
    //     const board = await context.projectBoardApi.api.getBoardById(params.boardId)
    //     if (board.error)
    //         throw notFound()
    //
    //     if (board.ok)
    //         throw Route.redirect({
    //             to: '/project-board/boards/$boardId/$slug',
    //             params: {boardId: board.data.boardId.toString(), slug: board.data.slug},
    //             replace: true,
    //         })
    //
    // },
    // component: GetBoardByIdComponent
// })

// async function GetBoardByIdComponent() {
    // const [isVisible, setIsVisible] = React.useState(false)
    //
    // const {boardId} = Route.useParams()
    // const {projectBoardApi} = Route.useRouteContext()
    // const {isPending, isError, data, error} = useQuery<GetBoardByIdResponse, ProblemDetails>({
    //     queryKey: ['board', Route.useParams().boardId],
    //     queryFn: async () => {
    //         const {data, error} = await projectBoardApi.api.getBoardById(boardId)
    //         if (error) throw error
    //         return data
    //     }
    // })
    //
    // if (isPending) return <div>Loading...</div>
    //
    // if (isError) return <div>Error: {error.detail}</div>
    //
    // return <div className="wrapper">
    //     <h1>{data.name}</h1>
    //     <div>{data.description}</div>
    //
    //     {
    //         !isVisible &&
    //         <div>
    //             <Button onClick={() => setIsVisible(true)}>Add column</Button>
    //         </div>
    //     }
    //
    //     {isVisible &&
    //         <div style={{
    //             padding: '0.75rem',
    //             borderRadius: '2px',
    //             backgroundColor: 'var(--color-neutral-0)',
    //             boxShadow: '0 0 0 2px var(--color-neutral-100)',
    //             display: 'inline-block',
    //             position: 'relative',
    //         }}>
    //             <Form
    //                 style={{
    //                     '--gap': '1rem',
    //                     '--grid-template-columns': '1fr'
    //                 }}
    //             >
    //                 <div
    //                     style={{
    //                         display: 'grid',
    //                         gap: '1rem',
    //                     }}
    //                 >
    //                     <input style={{justifySelf: 'start'}} type="text" placeholder="Enter column name" required
    //                            aria-required={true}/>
    //                     <div style={{display: 'flex', gap: '1rem'}}>
    //                         <Button style={{justifySelf: 'start'}} type={'submit'}>Add column</Button>
    //                     </div>
    //                 </div>
    //             </Form>
    //             <Button onClick={() => setIsVisible(false)} style={{
    //                 display: 'flex',
    //                 alignItems: 'center',
    //                 padding: '0.5rem',
    //                 borderRadius: 0,
    //                 position: 'absolute',
    //                 top: '0',
    //                 right: '-25%',
    //                 zIndex: 1
    //             }}>
    //                 <Xmark aria-label={'Cancel'} style={{fontSize: '1rem'}}/>
    //             </Button>
    //         </div>
    //     }
    // </div>
// }
