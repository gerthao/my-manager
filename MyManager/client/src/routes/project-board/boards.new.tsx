import { createFileRoute, useNavigate } from "@tanstack/react-router"
import { formOptions, useForm } from "@tanstack/react-form"
import { useMutation, useQueryClient } from "@tanstack/react-query"
import type {
  CreateBoardRequestBody,
  CreateBoardResponse,
  ProblemDetails,
  ValidationProblemDetails,
} from "../../api/Api.ts"
import { isValidationProblem } from "../../common/functions.ts"
import { Form, FormGroup } from "../../components/form.tsx"

export const Route = createFileRoute("/project-board/boards/new")({
  component: CreateBoardComponent,
})

interface CreateBoard {
  name: string
  description: string | null
}

function CreateBoardComponent() {
  const { projectBoardApi } = Route.useRouteContext()
  const navigate = useNavigate()
  const queryClient = useQueryClient()
  const createBoard = useMutation<
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

  const defaultCreateBoard: CreateBoard = {
    name: "",
    description: null,
  }

  const formOpts = formOptions({
    defaultValues: defaultCreateBoard,
  })

  const form = useForm({
    ...formOpts,
    onSubmit: async ({ value, formApi }) => {
      createBoard.mutate(value, {
        onError: (problem) => {
          // TODO: Render the errors on the form
          if (isValidationProblem(problem) && problem.errors) formApi.setErrorMap(problem.errors)
          // TODO: Eventually replace with an alert/toast component
          else alert(problem.title)

          // formAPi.reset seems to allow the form to be submitted again after error
          formApi.reset({ ...value })
        },
        onSuccess: (createBoardResponse) => {
          void queryClient.invalidateQueries({
            queryKey: ["boards"],
            exact: true,
          })
          window.alert("Board created!")
          formApi.reset()

          navigate({
            to: `/project-board/boards/$boardId/$slug`,
            params: {
              boardId: createBoardResponse.boardId.toString(),
              slug: createBoardResponse.slug,
            },
          })
        },
      })
    },
  })

  return (
    <div className="wrapper">
      <div>
        <h1>New Board</h1>
        <Form
          className="form"
          style={{
            "--grid-template-columns": "1fr",
            "--gap": "1rem",
          }}
          onSubmit={(e) => {
            e.preventDefault()
            e.stopPropagation()
            void form.handleSubmit()
          }}
        >
          <form.Field name="name">
            {(field) => (
              <FormGroup>
                <label htmlFor={field.name}>{field.name}</label>
                <input
                  id={field.name}
                  name={field.name}
                  onChange={(e) => field.handleChange(e.target.value)}
                  required
                  aria-required={"true"}
                  autoComplete={"false"}
                  autoCapitalize={"true"}
                  autoFocus={true}
                />
              </FormGroup>
            )}
          </form.Field>
          <form.Field name="description">
            {(field) => (
              <FormGroup>
                <label htmlFor={field.name}>
                  {field.name} <span>(Optional)</span>
                </label>
                <textarea
                  id={field.name}
                  name={field.name}
                  onChange={(e) => field.handleChange(e.target.value)}
                />
              </FormGroup>
            )}
          </form.Field>
          <button className={"button"} data-variant="filled" type="submit">
            Submit
          </button>
        </Form>
      </div>
    </div>
  )
}
