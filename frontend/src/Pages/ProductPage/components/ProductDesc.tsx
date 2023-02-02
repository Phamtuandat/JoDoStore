import { Box, Typography } from "@mui/material"
import React from "react"
import { useTheme } from "@mui/material/styles"
import convert from "Hooks/useConvert"
type Props = {
    desc: string | null
}

const ProductDesc = ({ desc }: Props) => {
    const theme = useTheme()
    return (
        <Box mt={10}>
            <Typography variant="h5" fontWeight={700} color={theme.palette.secondary.dark}>
                Description
            </Typography>

            {!desc ? (
                <Typography
                    component="p"
                    mt={2}
                    color={theme.palette.text.primary}
                    sx={{ opacity: 0.7 }}
                >
                    We’ve created a full-stack structure for our working workflow processes, were
                    from the funny the century initial all the made, have spare to negatives. But
                    the structure was from the funny the century rather, initial all the made, have
                    spare to negatives.
                </Typography>
            ) : (
                <Box
                    dangerouslySetInnerHTML={{
                        __html: convert.convertToHtml(desc),
                    }}
                />
            )}

            <Typography variant="h6" mt={5}>
                Care & Maintenance:
            </Typography>
            <Typography
                component="p"
                mt={2}
                color={theme.palette.text.primary}
                sx={{ opacity: 0.7 }}
            >
                Use warm water to describe us as a product team that creates amazing UI/UX
                experiences, by crafting top-notch user experience.
            </Typography>
        </Box>
    )
}

export default ProductDesc
