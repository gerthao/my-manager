import { LitElement } from "lit"
import { customElement, property } from "lit/decorators.js"
import { Icons } from "./Icons.ts"

@customElement("gt-icon")
export class GtIcon extends LitElement {
  @property() name: string = ""

  render() {
    return Icons[this.name]
  }
}
