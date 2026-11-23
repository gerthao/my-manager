import React, {
  createContext,
  useRef,
  useContext,
  useEffect,
  useState,
  type ReactNode,
  type RefObject,
} from "react"
import { useClickOutside } from "../hooks/useClickOutside"

// Context so the trigger can toggle state without prop-drilling
const DropdownContext = createContext<{ open: boolean; toggle: () => void }>({
  open: false,
  toggle: () => {},
})

export const GtDropdown = ({ children }: { children: ReactNode }) => {
  const [open, setOpen] = useState(false)
  const ref = useRef<HTMLDivElement>(null)

  useClickOutside(ref, () => setOpen(false), open)

  const toggle = () => setOpen((isOpen) => !isOpen)

  return (
    <DropdownContext.Provider value={{ open, toggle }}>
      <div ref={ref} className="dropdown">
        {children}
      </div>
    </DropdownContext.Provider>
  )
}

export const GtDropdownTrigger = ({ children }: { children: ReactNode }) => {
  const { open, toggle } = useContext(DropdownContext)
  return (
    <button type="button" className="dropdown-trigger" onClick={toggle}>
      {children}
    </button>
  )
}

export const GtDropdownContent = ({ children }: { children: ReactNode }) => {
  const { open } = useContext(DropdownContext)
  return (
    <ul className="dropdown-content" data-dropdown-state={open ? "open" : "closed"}>
      {children}
    </ul>
  )
}

export const GtDropdownContentItem = ({ children }: { children: ReactNode }) => {
  return <li className="dropdown-content-item">{children}</li>
}
