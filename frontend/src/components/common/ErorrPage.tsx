import { Box, Typography } from "@mui/material"
export default function ErrorPage() {
    return (
        <Box
            display="flex"
            flexDirection="column"
            alignItems="center"
            height="100vh"
            justifyContent="center"
        >
            <Typography variant="h3">Oops!</Typography>
            <Typography variant="h6">Sorry, an unexpected error has occurred.</Typography>
            <Typography variant="body1">erorr Message is here</Typography>
        </Box>
    )
}
