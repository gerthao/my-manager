import {html, LitElement} from 'lit'
import {customElement} from 'lit/decorators.js'

@customElement('gt-card-header')
export class GtCardHeader extends LitElement {
    render() {
        return html`
            <div class="card-header">
                <slot></slot>
            </div>
        `
    }
}
