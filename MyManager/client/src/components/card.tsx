import React from "react"

interface CardProperties extends React.HTMLAttributes<HTMLDivElement> {
  variant?: "outlined" | "filled"
}

const CardHeader = {}
const CardContext = {}
const CardActions = {}

const Card: React.FC<CardProperties> = ({ variant = "primary", onClick, children, ...rest }) => (
  <div className={"card"}>
    <div className={"card-body"}>
      <h2 className={"card-title"}>{title}</h2>
      <p className={"card-content"}></p>
    </div>
  </div>
)

export default Card
