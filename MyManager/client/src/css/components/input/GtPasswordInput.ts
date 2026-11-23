import { css, html, LitElement } from "lit"
import { customElement, property, state } from "lit/decorators.js"

@customElement("gt-password-input")
export class GtPasswordInput extends LitElement {
  static styles = css`
    :host {
      --label-color: var(--color-neutral-90);
      --label-font-size: 0.875rem;
      --label-margin-bottom: 0.25rem;
      --label-transition: color 0.2s;

      --input-color: var(--color-neutral-90);
      --input-font-size: 1rem;
      --input-padding: 0.5rem 2.5rem 0.5rem 0.5rem;
      --input-border: 1px solid var(--color-neutral-100);
      --input-border-radius: 0.4rem;
      --input-background-color: var(--color-neutral-0);
      --input-transition: border-color 0.2s, box-shadow 0.2s;

      & .input-container {
        display: flex;
        flex-direction: column;
        gap: 0.25rem;
        width: 100%;

        & label {
          color: var(--label-color);
          font-size: var(--label-font-size);
          transition: var(--label-transition);
        }

        & .password-toggle {
          background-color: transparent;
          position: absolute;
          right: 0.5rem;
          top: 50%;
          transform: translateY(-50%);
          cursor: pointer;
          padding: 0.25rem;
          border-radius: 2px;
          line-height: 0;
          border: none;
          color: var(--color-neutral-50);
          font-size: var(--input-font-size);
        }

        & .password-container {
          position: relative;
          display: flex;
          align-items: center;
          gap: 0.5rem;
        }

        & input {
          padding: var(--input-padding);
          font-size: var(--input-font-size);
          border: var(--input-border);
          border-radius: var(--input-border-radius);
          background-color: var(--input-background-color);
          color: var(--input-color);
          transition: var(--input-transition);
          outline: transparent;
          width: 100%;

          &:focus {
            box-shadow:
              5px 4px 0 0 var(--color-neutral-100),
              0 0 0 1px var(--color-neutral-100);
          }
        }

        &:has(input:focus) {
          & label {
            color: var(--color-primary-50);
          }
        }
      }
    }
  `
  @property({ type: Boolean, reflect: true }) disabled: boolean = false
  @property({ type: Boolean, reflect: true }) required: boolean = false
  @property({ type: Boolean, reflect: true }) readonly: boolean = false
  @property() id: string = ""
  @property() name: string = ""
  @property() label: string = ""
  @property() minlength: number = 0
  @property() maxlength: number = Infinity
  @state() type: "password" | "text" = "password"
  @property() pattern: string = ""

  protected render(): unknown {
    if (this.label !== "") {
      return html`
        <div class="input-container">
          <label for="${this.id}">${this.label}</label>
          <div class="password-container">
            <input
              type="${this.type}"
              name="${this.name}"
              id="${this.id}"
              minlength="${this.minlength}"
              maxlength="${this.maxlength}"
            />
            <button class="password-toggle" @click="${this.togglePassword}">
              <gt-icon name="eye"></gt-icon>
            </button>
          </div>
        </div>
      `
    }

    return html`
      <div class="input-container">
        <input
          type="password"
          name="${this.name}"
          id="${this.id}"
          minlength="${this.minlength}"
          maxlength="${this.maxlength}"
        />
      </div>
    `
  }

  private togglePassword() {
    this.type = this.type !== "password" ? "password" : "text"
  }
}
