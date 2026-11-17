import React, {type CSSProperties, type FormHTMLAttributes, type HTMLAttributes} from 'react'

type CSSVariableProperties<T extends string> = CSSProperties & {
    [K in T]?: string | number
}

type FormStyle = CSSVariableProperties<
    | '--gap'
    | '--grid-template-columns'
>

type FormGroupStyle = CSSVariableProperties<
    | '--gap'
    | '--grid-template-columns'
>

interface FormProperties extends FormHTMLAttributes<HTMLFormElement> {
    children?: React.ReactNode
    style?: FormStyle
}

interface FormGroupProperties extends HTMLAttributes<HTMLDivElement> {
    children?: React.ReactNode
    style?: FormGroupStyle
}

const Form: React.FC<FormProperties> = ({children, className, style, ...props}: FormProperties) => {
    return (
        <form className={className} style={style} {...props}>
            {children}
        </form>
    )
}

const FormGroup: React.FC<FormGroupProperties> = ({children, className, style, ...props}) =>
    <div className={'form-group'} style={style} {...props}>{children}</div>

export {Form, FormGroup}
