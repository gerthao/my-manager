import {css, html, LitElement} from 'lit'
import {customElement, property} from 'lit/decorators.js'

@customElement('gt-dropdown-item')
export class GtDropdownItem extends LitElement {
    static styles = css`
        :host([disabled]) .item {
            color: var(--color-neutral-50);
            pointer-events: none;
        }

        .dropdown-item {
            display: block;
            padding: 0.5rem 0.5rem;
            color: black;
            text-decoration: none;
            background-color: transparent;
            border: 0;
            cursor: pointer;
            text-align: left;
            border-radius: 0.45rem;
            transition: background-color 0.2s;

            &:hover {
                background-color: var(--color-neutral-20);
            }
        }
    `
    @property({type: Boolean, reflect: true}) disabled: boolean = false
    @property({type: String}) href?: string

    render() {
        return html`
            <li role="presentation">
                <a
                        role="menuitem"
                        class="dropdown-item"
                        href="${this.href ?? 'javascript:void(0)'}"
                        aria-disabled="${this.disabled}"
                        tabindex="${this.disabled ? -1 : 0}"
                        @click="${this._handleClick}"
                >
                    <slot></slot>
                </a>
            </li>
        `
    }

    private _handleClick(event: Event) {
        if (this.disabled) {
            event.preventDefault()
            return
        }

        if (!this.href) {
            event.preventDefault()
        }

        this.dispatchEvent(
            new CustomEvent('select', {bubbles: true, composed: true})
        )
    }
}
