import { Box, Button, CardMedia, Typography } from "@mui/material"
import Stack from "@mui/material/Stack"
import { MainLayout } from "components/Layout/MainLayout"
import { useNavigate, useRouteError } from "react-router-dom"

export default function ErrorPage() {
    const error: any = useRouteError()
    const naviagte = useNavigate()
    console.log(error)
    return (
        <MainLayout>
            <Stack
                display="flex"
                flexDirection="column"
                alignItems="center"
                justifyContent="center"
                minHeight="60vh"
            >
                <Box
                    mt={{ md: 20, xs: 10, sm: 15 }}
                    sx={{
                        display: { md: "flex", sm: "block" },
                        alignItems: "center",
                        justifyContent: "space-between",
                    }}
                >
                    <CardMedia
                        component="img"
                        image="https://res.cloudinary.com/dmzvhnnkh/image/upload/v1680011106/error-removebg-preview_flld10.png"
                        sx={{
                            width: { md: 600, xs: 200, sm: 300 },
                        }}
                    />
                    <Stack textAlign="center" alignItems="center">
                        <Box>
                            <Typography fontSize={120}>{error.status}</Typography>
                        </Box>
                        <Typography component="span" variant="h3">
                            Oops!
                        </Typography>
                        <Typography component="span" variant="h3" fontWeight={700}>
                            {error.status === 404 ? "Page Not Found" : error.data}
                        </Typography>
                        <Box>
                            <Button variant="contained" onClick={() => naviagte(-1)}>
                                Go Back
                            </Button>
                        </Box>
                    </Stack>
                </Box>
            </Stack>
        </MainLayout>
    )
}
