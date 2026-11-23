import { css, html, LitElement } from "lit"
import { customElement, property } from "lit/decorators.js"

@customElement("gt-button-group")
export class GtButtonGroup extends LitElement {
  static styles = css`
    :host {
      --gap: 0.5rem;
    }

    :host([vertical]) {
      & .button-group {
        flex-direction: column;
      }
    }

    .button-group {
      display: flex;
      gap: var(--gap);
      flex-wrap: nowrap;
    }
  `
  @property({ type: Boolean, reflect: true }) vertical: boolean = false

  render() {
    return html`
      <div class="button-group">
        <slot></slot>
      </div>
    `
  }
}
