import { createFileRoute, useRouter } from "@tanstack/react-router"
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query"
import type {
  Api,
  CreateColumnBody,
  CreateColumnResponse,
  GetBoardByIdResponse,
  ProblemDetails,
  ValidationProblemDetails,
} from "../../api/Api.ts"
import React from "react"
import Button from "../../components/button.tsx"
import { Form } from "../../components/form.tsx"
import { Xmark } from "iconoir-react"
import { formOptions, useForm } from "@tanstack/react-form"
import { isValidationProblem } from "../../common/functions.ts"

export const Route = createFileRoute("/project-board/boards/$boardId/$slug")({
  component: ViewBoardBySlugComponent,
  // loader: async ({params, context}) =>
  //     await context.projectBoardApi.api.getBoard BySlug(params.boardId, params.slug),
  head: ({ params }) => ({
    meta: [{ title: `${params.slug}: MyManager.Client` }],
  }),
})

interface AddColumnForm {
  name: string
}

function useGetBoardByIdQuery(projectBoardApi: Api<unknown>, boardId: string | number) {
  return useQuery<GetBoardByIdResponse, ProblemDetails>({
    queryKey: ["board", Route.useParams().boardId],
    queryFn: async () => {
      const { data, error } = await projectBoardApi.api.getBoardById(boardId)
      if (error) throw error
      return data
    },
  })
}

function useListColumnsByBoardIdQuery(projectBoardApi: Api<unknown>, boardId: string | number) {
  return useQuery({
    queryKey: ["board", boardId, "columns"],
    queryFn: async () => {
      const { data } = await projectBoardApi.api.listColumns(boardId)

      return data
    },
  })
}

function ViewBoardBySlugComponent() {
  const { boardId } = Route.useParams()
  const { projectBoardApi } = Route.useRouteContext()
  const { isPending, isError, data, error } = useGetBoardByIdQuery(projectBoardApi, boardId)

  if (isPending) return <div>Loading...</div>

  if (isError) return <div>Error: {error.detail}</div>

  return (
    <div>
      <h1>{data.name}</h1>
      <p>{data.description}</p>

      <ViewColumnsByBoardIdComponent></ViewColumnsByBoardIdComponent>
    </div>
  )
}

function ViewColumnsByBoardIdComponent() {
  const router = useRouter()
  const [isVisible, setIsVisible] = React.useState(false)
  const { boardId } = Route.useParams()
  const { projectBoardApi } = Route.useRouteContext()
  const { isPending, isError, data, error } = useListColumnsByBoardIdQuery(projectBoardApi, boardId)
  const queryClient = useQueryClient()

  const createColumn = useMutation<
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

  const defaultNewColumnForm: AddColumnForm = {
    name: "",
  }

  const addColumnFormOpts = formOptions({
    defaultValues: defaultNewColumnForm,
  })

  const addColumnForm = useForm({
    ...addColumnFormOpts,
    onSubmit: async (data) => {
      console.log(data)

      createColumn.mutate(
        { boardId, body: data.value },
        {
          onError: (problem) => {
            if (isValidationProblem(problem) && problem.errors) {
              alert("failed from validation")
            } else {
              alert("failed from other reason")
            }
          },
          onSuccess: (createColumnResponse) => {
            router.invalidate({ sync: true })

            void queryClient.invalidateQueries({
              queryKey: ["board", boardId, "columns"],
              exact: true,
            })
            window.alert("Column created!")
            data.formApi.reset()
            setIsVisible(false)
          },
        },
      )
    },
  })

  return (
    <div>
      <div style={{ marginBottom: "2rem" }}>
        {!isVisible && (
          <div>
            <Button onClick={() => setIsVisible(true)}>Add a column</Button>
          </div>
        )}

        {isVisible && (
          <div
            style={{
              padding: "0.75rem",
              borderRadius: "2px",
              backgroundColor: "var(--color-neutral-0)",
              boxShadow: "0 0 0 2px var(--color-neutral-100)",
              display: "inline-block",
              position: "relative",
            }}
          >
            <Form
              style={{
                "--gap": "1rem",
                "--grid-template-columns": "1fr",
              }}
              onSubmit={(e) => {
                e.preventDefault()
                e.stopPropagation()
                void addColumnForm.handleSubmit()
              }}
            >
              <div
                style={{
                  display: "grid",
                  gap: "1rem",
                }}
              >
                <addColumnForm.Field name={"name"}>
                  {(field) => (
                    <input
                      style={{
                        justifySelf: "start",
                      }}
                      type="text"
                      name={field.name}
                      placeholder="Enter column name"
                      required
                      aria-required={true}
                      autoFocus={true}
                      autoComplete="false"
                      onChange={(e) => field.handleChange(e.target.value)}
                    />
                  )}
                </addColumnForm.Field>
                <div
                  style={{
                    display: "flex",
                    gap: "1rem",
                  }}
                >
                  <Button
                    style={{
                      justifySelf: "start",
                    }}
                    type={"submit"}
                  >
                    Add column
                  </Button>
                </div>
              </div>
            </Form>
            <Button
              onClick={() => setIsVisible(false)}
              style={{
                display: "flex",
                alignItems: "center",
                padding: "0.5rem",
                borderRadius: 0,
                position: "absolute",
                top: "0",
                right: "-25%",
                zIndex: 1,
              }}
            >
              <Xmark
                aria-label={"Cancel"}
                style={{
                  fontSize: "1rem",
                }}
              />
            </Button>
          </div>
        )}
      </div>

      <div
        style={{
          display: "flex",
          gap: "1rem",
          marginBottom: "1rem",
        }}
      >
        {data &&
          data.map((boardColumn) => (
            <div
              key={boardColumn.columnId}
              style={{
                display: "grid",
                gap: "0.5rem",
                padding: "0.5rem",
                borderRadius: "2px",
                boxShadow:
                  "0 0 0 2px var(--color-neutral-100), 4px 4px 0 0 var(--color-neutral-100)",
                minWidth: "280px",
              }}
            >
              <h3 style={{ margin: 0 }}>{boardColumn.name}</h3>
              <button>Add Task</button>
            </div>
          ))}
      </div>
    </div>
  )
}
