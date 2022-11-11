import { Box, Button } from "@mui/material"
import { InputHTMLAttributes } from "react"
import { Control, useController } from "react-hook-form"
interface InputProps extends InputHTMLAttributes<HTMLInputElement> {
    control: Control<any>
    name: string
    handleUpload: (e: any) => void
}

export const ImageFeild = ({ control, name, handleUpload }: InputProps) => {
    const {
        field: { onBlur, ref },
    } = useController({
        name,
        control,
    })

    return (
        <Box mt={3} justifyContent="center" display="flex">
            <Button variant="outlined" component="label">
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
                UPLOAD
            </Button>
        </Box>
    )
}
