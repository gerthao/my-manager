import { html, LitElement } from "lit"
import { customElement } from "lit/decorators.js"

@customElement("gt-card-actions")
export class GtCardActions extends LitElement {
  render() {
    return html`
      <div>
        <slot></slot>
      </div>
    `
  }
}
