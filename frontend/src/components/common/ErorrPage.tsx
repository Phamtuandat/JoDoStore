import { Box, CardMedia, Typography } from "@mui/material"
import Stack from "@mui/material/Stack"
import PhotoApi from "ApiClients/PhotoApi"
import { MainLayout } from "components/Layout/MainLayout"
import { Photo } from "models"
import { useEffect, useRef, useState } from "react"
import { useRouteError } from "react-router-dom"
import buildQuery from "odata-query"

export default function ErrorPage() {
    const ignore = useRef(false)
    const error: any = useRouteError()
    const [errorPhoto, setErrorThumn] = useState<Photo>()
    useEffect(() => {
        if (!ignore.current) {
            ignore.current = true
            const param = buildQuery({
                filter: {
                    title: "Error Page",
                },
            })
            ;(async () => {
                var result = await PhotoApi.getAll(param)
                setErrorThumn(result.data[0])
            })()
        }
    }, [])

    return (
        <MainLayout>
            <Stack
                display="flex"
                flexDirection="column"
                alignItems="center"
                justifyContent="center"
                minHeight="60vh"
            >
                <Box height={300} width={300} mt={{ md: 20, xs: 0, sm: 10 }}>
                    <CardMedia
                        component="img"
                        image={errorPhoto?.imageUrl}
                        sx={{
                            height: "100%",
                            width: "100%",
                        }}
                    />
                </Box>

                <Typography component="span" variant="h3">
                    Oops!
                </Typography>
                <Typography component="span" variant="h6">
                    Sorry, an unexpected error has occurred.
                </Typography>
                <Typography component="span" variant="h3" fontWeight={700}>
                    {error.data}
                </Typography>
            </Stack>
        </MainLayout>
    )
}
