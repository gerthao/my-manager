import { css, html, LitElement } from "lit"
import { customElement } from "lit/decorators.js"

@customElement("gt-dropdown-divider")
export class GtDropdownDivider extends LitElement {
  static styles = css`
    .dropdown-divider {
      margin-block: 0.25rem;
      border-top: 1px solid var(--color-neutral-80);
    }
  `

  render() {
    return html` <div class="dropdown-divider"></div> `
  }
}
