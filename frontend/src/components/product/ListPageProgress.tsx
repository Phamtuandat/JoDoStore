import { Box } from "@mui/material"
import CircularProgress from "@mui/material/CircularProgress"

export function ListPageProgress() {
    return (
        <Box width={"100%"} display="flex" height="100%" alignItems="center">
            <CircularProgress disableShrink sx={{ mx: "auto" }} />
        </Box>
    )
}
