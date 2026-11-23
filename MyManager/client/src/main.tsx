import { StrictMode } from "react"
import ReactDOM from "react-dom/client"
import { createRouter, RouterProvider } from "@tanstack/react-router"

// Import the generated route tree
import { routeTree } from "./routeTree.gen"
import { Api } from "./api/Api.ts"
import { QueryClient, QueryClientProvider } from "@tanstack/react-query"

const projectBoardApi = new Api()
const queryClient = new QueryClient()

// Create a new router instance
const router = createRouter({
  routeTree,
  context: {
    projectBoardApi,
    queryClient,
  },
  defaultPreloadStaleTime: 0, // why 0?
})

// Register the router instance for type safety
declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router
  }
}

// Render the app
const rootElement = document.getElementById("root")!
if (!rootElement.innerHTML) {
  const root = ReactDOM.createRoot(rootElement)
  root.render(
    <StrictMode>
      <QueryClientProvider client={queryClient}>
        <RouterProvider router={router} context={{ projectBoardApi, queryClient }} />
      </QueryClientProvider>
    </StrictMode>,
  )
}
