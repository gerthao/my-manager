import {css, html, LitElement} from 'lit'
import {property} from 'lit/decorators.js'

export class GtNumberInput extends LitElement {
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

            & .number-input-container {
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
    `
    @property() value: number = 0
    @property() min: number = 0
    @property() max: number = Infinity
    @property() step: number = 1
    @property() disabled: boolean = false
    @property() readonly: boolean = false
    @property() id: string = ''
    @property() name: string = ''
    @property() label: string = ''
    @property() required: boolean = false
    @property() placeholder: string = ''
    @property() autocomplete: string = 'off'
    @property() autofocus: boolean = false

    render() {
        return html`
            <div class="number-input-container">
                <label for="${this.id}">${this.label}</label>
                <input
                        id="${this.id}"
                        type="number"
                        value="${this.value}"
                        min="${this.min}"
                        max="${this.max}"
                        step="${this.step}"
                        disabled="${this.disabled}"
                        readonly="${this.readonly}"
                />
            </div>
        `
    }
}
