import React from "react"

interface ButtonProperties extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  variant?: "text" | "filled" | "outlined"
  isLoading?: boolean
}

const Button: React.FC<ButtonProperties> = ({
  variant = "primary",
  onClick,
  disabled,
  children,
  ...rest
}) => (
  <button className={"button"} onClick={onClick} disabled={disabled} {...rest}>
    {children}
  </button>
)

export default Button
