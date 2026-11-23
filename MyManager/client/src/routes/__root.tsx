import { createRootRouteWithContext, HeadContent, Link, Outlet } from "@tanstack/react-router"
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools"
import { QueryClient } from "@tanstack/react-query"
import type { Api } from "../api/Api.ts"
import "../css/main.css"
import "../css/colors.css"
import "../css/shadows.css"
import "../css/form.css"
import "../css/button.css"
import "../css/wrapper.css"
import "../css/dropdown.css"
import "@fontsource/crimson-text"
import "@fontsource/shippori-mincho"
import "@fontsource-variable/grandstander"
import "@fontsource-variable/chivo-mono"

interface ProjectBoardRouterContext {
  projectBoardApi: Api<any>
  queryClient: QueryClient
}

export const RootLayout = () => (
  <>
    <div style={{ display: "flex", gap: "1ch" }}>
      <Link to="/" className="[&.active:font-bold">
        Home
      </Link>
      <Link to="/about" className="[&.active]:font-bold">
        About
      </Link>
      <Link to="/project-board/boards" className="[&.active]:font-bold">
        Boards
      </Link>
      <Link to="/example" className="[&.active]:font-bold">
        Example
      </Link>
    </div>
    <hr />
    <HeadContent />
    <main>
      <Outlet />
    </main>
    <TanStackRouterDevtools />
  </>
)

export const Route = createRootRouteWithContext<ProjectBoardRouterContext>()({
  component: RootLayout,
})
