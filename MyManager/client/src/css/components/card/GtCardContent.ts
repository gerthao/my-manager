import {css, html, LitElement} from 'lit'
import {customElement} from 'lit/decorators.js'

@customElement('gt-card-content')
export class GtCardContent extends LitElement {
    static styles = css`
        :host {
        }

        .card-content {
            display: grid;
            margin-block: 0;
        }

        .card-title ::slotted(*) {
            font-size: 1.5rem;
            font-weight: 600;
            margin: 0 0 0.5rem;
        }

        .card-subtitle ::slotted(*) {
            font-size: 1rem;
            font-weight: 500;
            margin: 0 0 0.5rem;
        }

        .card-text ::slotted(*) {
            margin: 1rem 0rem;
        }

        .card-actions {
            display: flex;
            gap: 0.5rem;
            flex-wrap: wrap;
        }
    `

    render() {
        return html`
            <div class="card-content">
                <div class="card-title">
                    <slot name="title"></slot>
                </div>
                <div class="card-subtitle">
                    <slot name="subtitle"></slot>
                </div>
                <div class="card-text">
                    <slot name="text"></slot>
                </div>
                <div class="card-actions">
                    <slot name="actions"></slot>
                </div>
            </div>
        `
    }
}
