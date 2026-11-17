import {createFileRoute} from '@tanstack/react-router'
import {useQuery} from '@tanstack/react-query'
import type {GetBoardByIdResponse, ProblemDetails} from '../../api/Api.ts'
import React from 'react'
import Button from '../../components/button.tsx'
import {Form} from '../../components/form.tsx'
import {Plus, Xmark} from 'iconoir-react'
import {formOptions, useForm} from '@tanstack/react-form'

export const Route = createFileRoute('/project-board/boards/$boardId/$slug')({
    component: ViewBoardBySlugComponent,
    // loader: async ({params, context}) =>
    //     await context.projectBoardApi.api.getBoardBySlug(params.boardId, params.slug),
    head: ({params}) => ({
        meta: [
            {title: `${params.slug}: MyManager.Client`}
        ]
    }),
})

interface AddColumnForm {
    name: string
}

function ViewBoardBySlugComponent() {
    const [isVisible, setIsVisible] = React.useState(false)
    const {boardId} = Route.useParams()
    const {projectBoardApi} = Route.useRouteContext()
    const {isPending, isError, data, error} = useQuery<GetBoardByIdResponse, ProblemDetails>({
        queryKey: ['board', Route.useParams().boardId],
        queryFn: async () => {
            const {data, error} = await projectBoardApi.api.getBoardById(boardId)
            if (error) throw error
            return data
        }
    })

    const defaultNewColumnForm: AddColumnForm = {
        name: '',
    }

    const addColumnFormOpts = formOptions({
        defaultValues: defaultNewColumnForm,
    })

    const addColumnForm = useForm({
        ...addColumnFormOpts,
        onSubmit: async (data) => {
            console.log(data);
            data.formApi.reset();
            setIsVisible(false);
        }
    })

    if (isPending) return <div>Loading...</div>

    if (isError) return <div>Error: {error.detail}</div>

    return <div className="wrapper">
        <h1>{data.name}</h1>
        <p>{data.description}</p>
        {
            !isVisible &&
            <div>
                <Button
                    onClick={() => setIsVisible(true)}
                >
                    Add a column</Button>
            </div>
        }

        {isVisible &&
            <div style={{
                padding: '0.75rem',
                borderRadius: '2px',
                backgroundColor: 'var(--color-neutral-0)',
                boxShadow: '0 0 0 2px var(--color-neutral-100)',
                display: 'inline-block',
                position: 'relative',
            }}>
                <Form
                    style={{
                        '--gap': '1rem',
                        '--grid-template-columns': '1fr'
                    }}
                    onSubmit={(e) => {
                        e.preventDefault()
                        e.stopPropagation()
                        void addColumnForm.handleSubmit();
                    }}
                >
                    <div
                        style={{
                            display: 'grid',
                            gap: '1rem',
                        }}
                    >
                        <addColumnForm.Field
                            name={'name'}
                            children={(field) =>
                                <input
                                    style={{justifySelf: 'start'}}
                                    type="text"
                                    name={field.name}
                                    placeholder="Enter column name"
                                    required
                                    aria-required={true}
                                    onChange={(e) => field.handleChange(e.target.value)}
                                />
                            }
                        >
                        </addColumnForm.Field>
                        <div style={{display: 'flex', gap: '1rem'}}>
                            <Button style={{justifySelf: 'start'}} type={'submit'}>Add column</Button>
                        </div>
                    </div>
                </Form>
                <Button onClick={() => setIsVisible(false)} style={{
                    display: 'flex',
                    alignItems: 'center',
                    padding: '0.5rem',
                    borderRadius: 0,
                    position: 'absolute',
                    top: '0',
                    right: '-25%',
                    zIndex: 1
                }}>
                    <Xmark aria-label={'Cancel'} style={{fontSize: '1rem'}}/>
                </Button>
            </div>
        }
    </div>
}
