import { Box, Typography } from "@mui/material"
import React from "react"
import { useTheme } from "@mui/material/styles"
type Props = {
    desc: string | null
}

const ProductDesc = ({ desc }: Props) => {
    const theme = useTheme()
    return (
        <Box mt={10}>
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
                <>
                    <Typography variant="h5" fontWeight={700} color={theme.palette.secondary.dark}>
                        Description
                    </Typography>
                    <Box
                        color="primary !important"
                        sx={{
                            "& p,ul,li,a,span": {
                                color: theme.palette.text.primary + "!important",
                            },
                            "& p": {
                                fontSize: theme.typography.h6.fontSize + "!important",
                            },
                            "& div": {
                                maxWidth: "100%",
                            },
                            "& img": {
                                margin: "80px auto 0px !important",
                            },
                        }}
                        dangerouslySetInnerHTML={{
                            __html: desc,
                        }}
                    />
                </>
            )}
        </Box>
    )
}

export default ProductDesc
