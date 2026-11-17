import {css, html, LitElement} from 'lit'
import {customElement, state} from 'lit/decorators.js'

@customElement('gt-dropdown')
export class GtDropdown extends LitElement {
    static styles = css`
        :host {
            position: relative;
        }

        .dropdown-trigger {
            cursor: pointer;
            display: inline-flex;
        }

        .dropdown-menu-container {
            display: none;
            opacity: 0;
            top: 100%;
            left: 0;
            transform: translateY(-2px);
            transition-property: display opacity transform;
            transition-duration: 0.2s;
            transition-behavior: allow-discrete;
            position: absolute;
            z-index: 1000;
            min-width: 10rem;
            padding: 0;
            font-size: 1rem;
        }

        .dropdown-menu-container.open {
            display: block;
            opacity: 1;
            transform: translateY(2px);

            @starting-style {
                opacity: 0;
                transform: translateY(-2px);
            }
        }
    `
    @state() open: boolean = false

    connectedCallback() {
        super.connectedCallback()
        document.addEventListener('click', this._handleOutsideClick.bind(this))
        document.addEventListener('keydown', this._handleKeydown.bind(this))
    }

    disconnectedCallback() {
        super.disconnectedCallback()
        document.removeEventListener(
            'click',
            this._handleOutsideClick.bind(this)
        )
        document.removeEventListener('keydown', this._handleKeydown.bind(this))
    }

    updated(changedProperties: Map<string | number | symbol, unknown>) {
        if (!changedProperties.has('open')) return

        const trigger = this.querySelector('[slot="trigger"]')

        trigger?.setAttribute('aria-expanded', this.open.toString())
        trigger?.toggleAttribute('active', this.open)

        this.dispatchEvent(
            new CustomEvent(this.open ? 'open' : 'close', {
                bubbles: true,
                composed: true,
            })
        )
    }

    render() {
        return html`
            <div class="dropdown-trigger" @click="${this._toggle}">
                <slot name="trigger"></slot>
            </div>
            <div class="dropdown-menu-container ${this.open ? 'open' : ''}">
                <slot name="menu"></slot>
            </div>
        `
    }

    private _handleOutsideClick(event: MouseEvent) {
        if (this.contains(event.target as Node)) return

        this.open = false
    }

    private _handleKeydown(event: KeyboardEvent) {
        if (event.key === 'Enter') this._toggle()
        if (event.key === 'Escape') this.open = false
    }

    private _toggle() {
        this.open = !this.open
    }
}
