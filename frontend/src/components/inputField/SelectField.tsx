import { Autocomplete, Box, TextField } from "@mui/material"
import { Category } from "models"
import { Control, useController } from "react-hook-form"

export interface PasswordFieldProps extends React.InputHTMLAttributes<HTMLInputElement> {
    name: string
    control: Control<any>
    label?: string
    disabled: boolean
    options: Category[]
    isMutiple: boolean
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
                value={value}
                multiple={isMutiple}
                disablePortal
                options={options.map((o) => o.id)}
                getOptionLabel={(option) => options.filter((o) => o.id === option)[0]?.name}
                onChange={(event, value) => {
                    onChange(value)
                }}
                fullWidth
                renderInput={(params) => (
                    <TextField
                        {...params}
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
