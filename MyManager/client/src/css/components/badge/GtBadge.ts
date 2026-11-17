import {css, html, LitElement} from 'lit'
import {customElement} from 'lit/decorators.js'

@customElement('gt-badge')
export class GtBadge extends LitElement {
    static styles = css`
        :host {
            display: inline-block;
        }

        :host([variant='filled']) div {
        }

        :host([variant='outlined']) div {
        }

        :host([variant='text']) div {
        }
    `

    render() {
        return html`
            <div>
                <slot></slot>
            </div>
        `
    }
}
