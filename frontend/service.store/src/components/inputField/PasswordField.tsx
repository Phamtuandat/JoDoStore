import { TextField } from "@mui/material"
import React, { InputHTMLAttributes } from "react"
import { Control, useController } from "react-hook-form"

export interface PasswordFieldProps extends InputHTMLAttributes<HTMLInputElement> {
    name: string
    control: Control<any>
    label?: string
    disabled: boolean
}

export const PasswordField = ({ name, control, label, ...inputProps }: PasswordFieldProps) => {
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
            type="password"
            autoComplete="current-password"
        />
    )
}
