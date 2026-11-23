import { css, html, LitElement } from "lit"
import { customElement } from "lit/decorators.js"

@customElement("gt-dropdown-header")
export class GtDropdownHeader extends LitElement {
  static styles = css`
    .dropdown-header {
      font-weight: 600;
      font-size: 0.875rem;
      padding: 0.5rem;
      margin: 0;
      color: var(--color-neutral-50);
    }
  `

  render() {
    return html`
      <h6 class="dropdown-header">
        <slot></slot>
      </h6>
    `
  }
}
