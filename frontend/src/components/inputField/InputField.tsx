import { TextField } from "@mui/material"
import { InputHTMLAttributes } from "react"
import { Control, useController } from "react-hook-form"

export interface InputFieldProps extends InputHTMLAttributes<HTMLInputElement> {
    name: string
    control: Control<any>
    label?: string
    disabled: boolean
}
export const InputField = ({ name, control, label, ...inputProps }: InputFieldProps) => {
    const {
        field: { value, onChange, onBlur, ref },
        fieldState: { error },
    } = useController({
        name,
        control,
    })
    return (
        <TextField
            size="small"
            margin="normal"
            fullWidth
            label={label}
            variant="outlined"
            onChange={onChange}
            onBlur={onBlur}
            inputRef={ref}
            value={value || ""}
            error={!!error}
            helperText={error?.message}
            inputProps={inputProps}
        />
    )
}
