import { Autocomplete, Box, TextField } from "@mui/material"
import { Category } from "models"
import { Control, useController } from "react-hook-form"

export interface PasswordFieldProps extends React.InputHTMLAttributes<HTMLInputElement> {
    name: string
    control: Control<any>
    label?: string
    disabled: boolean
    options: Category[]
    isMutiple?: boolean
}
export default function SelectTextFields({
    name,
    control,
    options,
    label,
    isMutiple,
    ...inputProps
}: PasswordFieldProps) {
    const {
        field: { value, onBlur, onChange, ref },
        fieldState: { error },
    } = useController({
        name,
        control,
    })

    return (
        <Box
            sx={{
                "& .MuiTextField-root": { m: 1 },
                my: 5,
            }}
        >
            <Autocomplete
                multiple={isMutiple}
                disablePortal
                id="combo-box-demo"
                options={options}
                getOptionLabel={(option) => option.name}
                onChange={(event, newOptions) => {
                    onChange(newOptions)
                }}
                fullWidth
                renderInput={(params) => (
                    <TextField
                        {...params}
                        value={value || ""}
                        onBlur={onBlur}
                        name={name}
                        inputRef={ref}
                        color="info"
                        error={!!error?.message}
                        variant="standard"
                        fullWidth
                        placeholder={`Please choose ${label}`}
                    />
                )}
            />
        </Box>
    )
}
