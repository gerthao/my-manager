import {css, html, LitElement} from 'lit'
import {customElement, property} from 'lit/decorators.js'

@customElement('gt-input')
export class GtInput extends LitElement {
    static styles = css`
        :host {
            --label-color: var(--color-neutral-90);
            --label-font-size: 0.875rem;
            --label-margin-bottom: 0.25rem;
            --label-transition: color 0.2s;

            --input-color: var(--color-neutral-90);
            --input-font-size: 1rem;
            --input-padding: 0.4rem;
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

                & input {
                    padding: var(--input-padding);
                    font-size: var(--input-font-size);
                    border: var(--input-border);
                    border-radius: var(--input-border-radius);
                    background-color: var(--input-background-color);
                    color: var(--input-color);
                    transition: var(--input-transition);
                    outline: transparent;

                    &:focus {
                        box-shadow: 5px 4px 0 0 var(--color-neutral-100),
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
    @property({type: Boolean, reflect: true}) disabled: boolean = false
    @property({type: Boolean, reflect: true}) required: boolean = false
    @property({type: Boolean, reflect: true}) readonly: boolean = false
    @property() id: string = ''
    @property() name: string = ''
    @property() label: string = ''
    @property() type:
        | 'text'
        | 'password'
        | 'email'
        | 'date'
        | 'datetime-local'
        | 'month'
        | 'time'
        | 'week'
        | 'number'
        | 'range'
        | 'color'
        | 'tel'
        | 'url'
        | 'search'
        | 'file' = 'text'
    @property() minlength: number = 0
    @property() maxlength: number = Infinity
    @property() autocomplete: string = 'off'
    @property() autofocus: boolean = false
    @property() spellcheck: boolean = false
    @property() autocapitalize: string = 'off'

    render() {
        return html`
            <div class="input-container">
                ${this.label !== '' ? this.renderLabel() : ''}
                ${this.renderInput()}
            </div>
        `
    }

    private renderLabel() {
        return html` <label for="${this.id}">${this.label}</label> `
    }

    private renderInput() {
        return html`
            <input
                    type="${this.type}"
                    name="${this.name}"
                    id="${this.id}"
                    minlength="${this.minlength}"
                    maxlength="${this.maxlength}"
                    autocomplete="${this.autocomplete}"
                    autofocus="${this.autofocus}"
                    spellcheck="${this.spellcheck}"
                    autocapitalize="${this.autocapitalize}"
                    aria-required="${this.required}"
                    aria-disabled="${this.disabled}"
                    aria-readonly="${this.readonly}"
            />
        `
    }
}
