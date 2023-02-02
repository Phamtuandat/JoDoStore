import { Box, CardMedia, Typography } from "@mui/material"
import Stack from "@mui/material/Stack"
import thumbApi from "ApiClients/ThumbApi"
import { MainLayout } from "components/Layout/MainLayout"
import { Thumbnail } from "models"
import { useEffect, useRef, useState } from "react"
import { useRouteError } from "react-router-dom"
import buildQuery from "odata-query"

export default function ErrorPage() {
    const ignore = useRef(false)
    const error = useRouteError() as string
    const [errorThumb, setErrorThumn] = useState<Thumbnail>()
    useEffect(() => {
        if (!ignore.current) {
            ignore.current = true
            const param = buildQuery({
                filter: {
                    title: "Error Page",
                },
            })
            ;(async () => {
                var result = await thumbApi.getAll(param)
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
                <Box height={300} width={300}>
                    <CardMedia
                        component="img"
                        image={errorThumb?.imageUrl}
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
                    {error}
                </Typography>
            </Stack>
        </MainLayout>
    )
}
