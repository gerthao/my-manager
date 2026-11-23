import { css, html, LitElement } from "lit"
import { customElement, property } from "lit/decorators.js"

@customElement("gt-button")
export class GtButton extends LitElement {
  static styles = css`
    .button {
      font-family: inherit;
      cursor: pointer;
      border: none;
      border-radius: 0.5rem;
      font-weight: 500;
      transition:
        background-color 0.2s,
        opacity 0.2s;
      box-shadow: var(--shadow-button);
    }

    :host([variant="filled"]) .button {
      --_hover-background-color: oklch(from var(--_background-color) calc(l - 0.1) c h);
      --_hover-text-color: var(--_text-color);
      --_pressed-background-color: oklch(from var(--_background-color) calc(l - 0.2) c h);
      --_pressed-text-color: var(--_text-color);

      background-color: var(--_background-color);
      color: var(--_text-color);

      &:hover:not(:active) {
        background-color: var(--_hover-background-color);
        color: var(--_hover-text-color);
      }

      &:active {
        background-color: var(--_pressed-background-color);
        color: var(--_pressed-text-color);
      }
    }

    :host([variant="outlined"]) .button {
      --_hover-background-color: oklch(from var(--_background-color) calc(l - 0.1) c h / 0.2);
      --_hover-text-color: oklch(from var(--_background-color) calc(l - 0.1) c h);
      --_pressed-background-color: oklch(from var(--_background-color) calc(l - 0.1) c h);
      --_pressed-text-color: var(--_text-color);

      box-shadow:
        5px 4px 0 0 var(--_background-color),
        0 0 0 1px var(--_background-color);
      background-color: transparent;
      color: var(--_background-color);

      &:hover {
        background-color: var(--_hover-background-color);
        color: var(--_hover-text-color);
      }

      &:active {
        background-color: var(--_pressed-background-color);
        color: var(--_pressed-text-color);
        box-shadow:
          5px 4px 0 0 var(--color-neutral-100),
          0 0 0 1px var(--color-neutral-100);
      }
    }

    :host([variant="text"]) .button {
      --_hover-background-color: oklch(from var(--_background-color) calc(l - 0.1) c h / 0.2);
      --_hover-text-color: oklch(from var(--_background-color) calc(l - 0.1) c h);
      --_pressed-background-color: oklch(from var(--_background-color) calc(l - 0.1) c h);
      --_pressed-text-color: var(--_text-color);

      background-color: transparent;
      color: var(--_background-color);

      &:hover {
        background-color: var(--_hover-background-color);
        color: var(--_hover-text-color);
      }

      &:active {
        background-color: var(--_pressed-background-color);
        color: var(--_pressed-text-color);
      }
    }

    :host([active]) .button {
      background-color: var(--_pressed-background-color);
      color: var(--_pressed-text-color);
    }

    :host([size="sm"]) .button {
      padding: 0.25rem 0.5rem;
      font-size: 0.875rem;
    }

    :host([size="md"]) .button {
      padding: 0.5rem 1rem;
      font-size: 1rem;
    }

    :host([size="lg"]) .button {
      padding: 0.75rem 1.5rem;
      font-size: 1.125rem;
    }

    :host([color="primary"]) .button {
      --_background-color: var(--color-primary-50);
      --_text-color: white;
    }

    :host([color="secondary"]) .button {
      --_background-color: var(--color-neutral-10);
      --_text-color: var(--color-neutral-90);
    }

    :host([color="danger"]) .button {
      --_background-color: var(--color-danger-50);
      --_text-color: white;
    }

    :host([color="success"]) .button {
      --_background-color: var(--color-success-50);
      --_text-color: white;
    }

    :host([color="warning"]) .button {
      --_background-color: var(--color-warning-40);
      --_text-color: white;
    }

    :host([disabled]) .button {
      opacity: 0.5;
      cursor: not-allowed;
    }
  `
  @property({ type: Boolean, reflect: true }) disabled: boolean = false
  @property({ reflect: true }) size: "sm" | "md" | "lg" = "md"
  @property({ reflect: true }) color: "primary" | "secondary" = "primary"
  @property({ reflect: true }) variant: "filled" | "outlined" | "text" = "filled"

  render() {
    return html`
      <button class="button" ?disabled=${this.disabled}>
        <slot></slot>
      </button>
    `
  }
}
