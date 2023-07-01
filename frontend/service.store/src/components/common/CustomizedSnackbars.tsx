import { AlertColor } from "@mui/material"
import MuiAlert, { AlertProps } from "@mui/material/Alert"
import Snackbar from "@mui/material/Snackbar"
import Stack from "@mui/material/Stack"
import { forwardRef, useEffect, useState } from "react"

const Alert = forwardRef<HTMLDivElement, AlertProps>(function Alert(props, ref) {
    return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />
})
type IProps = {
    message: string
    open: boolean
    severity: AlertColor | undefined
}
export default function CustomizedSnackbars(props: IProps) {
    const [open, setOpen] = useState(false)

    useEffect(() => {
        setOpen(props.open)
    }, [props.open])

    const handleClose = (event?: React.SyntheticEvent | Event, reason?: string) => {
        if (reason === "clickaway") {
            return
        }

        setOpen(false)
    }

    return (
        <Stack spacing={2} sx={{ width: "100%" }}>
            <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
                <Alert onClose={handleClose} severity={props.severity} sx={{ width: "100%" }}>
                    {props.message}
                </Alert>
            </Snackbar>
        </Stack>
    )
}
