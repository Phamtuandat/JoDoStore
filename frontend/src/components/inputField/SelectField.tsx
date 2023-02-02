import { Autocomplete, Box, TextField } from "@mui/material"
import { Category, Tag } from "models"
import { Control, useController } from "react-hook-form"

export interface PasswordFieldProps extends React.InputHTMLAttributes<HTMLInputElement> {
    name: string
    control: Control<any>
    label?: string
    disabled: boolean
    options: Category[] | Tag[]
    isMutiple: boolean
    setValue: (value: string) => void
}
export default function SelectTextFields({
    setValue,
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
                disableClearable
                freeSolo
                isOptionEqualToValue={(option, value) => option === value.name}
                value={value}
                multiple={isMutiple}
                disablePortal
                options={options.map((opt) => opt.name)}
                getOptionLabel={(option) => option}
                fullWidth
                onChange={(event, value) => {
                    onChange(value)
                }}
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
                        onChange={(event) => {
                            setValue(event.target.value)
                        }}
                    />
                )}
            />
        </Box>
    )
}
