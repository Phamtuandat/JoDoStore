import { Box, Button } from "@mui/material"
import Typography from "@mui/material/Typography"
import { InputHTMLAttributes } from "react"
import { Control, useController } from "react-hook-form"
interface InputProps extends InputHTMLAttributes<HTMLInputElement> {
    control: Control<any>
    name: string
    handleUpload: (e: any) => void
    label: string
    variant?: "text" | "contained" | "outlined" | undefined
}

export const ImageFeild = ({ control, name, handleUpload, label, variant }: InputProps) => {
    const {
        field: { onBlur, ref },
        fieldState: { error },
    } = useController({
        name,
        control,
    })

    return (
        <Box
            mt={3}
            justifyContent="center"
            display="flex"
            flexDirection="column"
            textAlign="center"
        >
            <Box>
                <Button variant={variant || "outlined"} component="label">
                    <input
                        ref={ref}
                        type="file"
                        hidden
                        onChange={(e) => {
                            handleUpload(e.target.files)
                        }}
                        name={name}
                        onBlur={onBlur}
                        accept="image/*"
                        multiple
                    />
                    {label}
                </Button>
            </Box>
            <Box>
                {!!error && (
                    <Typography variant="overline" color="error">
                        {error?.message}
                    </Typography>
                )}
            </Box>
        </Box>
    )
}
