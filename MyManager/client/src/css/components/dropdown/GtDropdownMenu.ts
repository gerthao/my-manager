import { css, html, LitElement } from "lit"
import { customElement } from "lit/decorators.js"

@customElement("gt-dropdown-menu")
export class GtDropdownMenu extends LitElement {
  static styles = css`
    .dropdown-menu {
      padding: 0.5rem;
      background-color: #fff;
      border: 1px solid var(--color-neutral-20);
      border-radius: 0.4rem;
      box-shadow:
        3px 3px 0 0 var(--color-neutral-100),
        0 0 0 1px var(--color-neutral-100);
      list-style: none;
    }
  `

  render() {
    return html`
      <ul class="dropdown-menu" role="menu">
        <slot></slot>
      </ul>
    `
  }
}
