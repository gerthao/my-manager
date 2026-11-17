import {css, html, LitElement} from 'lit'
import {customElement} from 'lit/decorators.js'

@customElement('gt-card')
export class GtCard extends LitElement {
    static styles = css`
        :host {
            --width: initial;
        }

        .card {
            width: var(--width);
            background-color: var(--color-neutral-0);
            border-radius: 0.4rem;
            box-shadow: var(--shadow-card);
            padding: 1rem;
            color: black;
        }
    `

    render() {
        return html`
            <div class="card">
                <slot></slot>
            </div>
        `
    }
}
