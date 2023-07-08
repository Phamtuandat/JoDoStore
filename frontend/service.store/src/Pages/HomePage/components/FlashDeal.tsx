import EastIcon from "@mui/icons-material/East"
import HeadsetMicOutlinedIcon from "@mui/icons-material/HeadsetMicOutlined"
import { Box, Button, Typography } from "@mui/material"
import { styled, useTheme } from "@mui/material/styles"
import Grid from "@mui/material/Unstable_Grid2"
import { useWidth } from "Hooks/width-hook"
import { Link } from "react-router-dom"
type Props = {}

const CustomBtn = styled("button")(({ theme }) => ({
    backgroundColor: theme.palette.background.paper,
    cursor: "pointer",
    display: "flex",
    alignItems: "center",
    fontSize: "16px",
    width: "200px",
    height: "60px",
    justifyContent: "center",
    border: "1px solid #ccc",
    color: theme.palette.text.primary,
    "&>span>svg": {
        marginLeft: "10px",
        width: "0",
        transition: " all 0.3s",
        overflow: "hidden",
    },
    "&:hover>span>svg": {
        width: "20px",
    },
}))

const FlashDeal = (props: Props) => {
    const theme = useTheme()
    const width = useWidth()
    return (
        <Box
            sx={{
                backgroundSize: "cover",
                mt: width === "xs" ? 2 : 5,
                backgroundImage: {
                    md: "url(https://static.nike.com/a/images/f_auto/dpr_1.3,cs_srgb/w_1406,c_limit/8127cdb2-6a27-4d27-96a4-dfc7cf5e6cdb/nike-just-do-it.jpg)",
                    xs: "url(https://static.nike.com/a/images/f_auto/dpr_1.3,cs_srgb/h_713,c_limit/a1cc9ddb-92d7-47b2-973f-1a17bbd17ff2/nike-just-do-it.jpg)",
                },
                backgroundRepeat: "no-repeat",
                backgroundPosition: "center",
                justifyContent: "center",
                height: 600,
                display: "flex",
            }}
        >
            <Grid container spacing={2} flexGrow={1}>
                <Grid
                    xs={12}
                    md={6}
                    sx={{
                        display: "flex",
                        flexDirection: "column",
                        textAlign: "center",
                        justifyContent: "center",
                        color: "white",
                    }}
                >
                    <Box>
                        <Box
                            component="span"
                            sx={{
                                p: "4px",
                                borderRadius: "50%",
                                backgroundColor: theme.palette.primary.main,
                                lineHeight: 0,
                            }}
                        >
                            <HeadsetMicOutlinedIcon />
                        </Box>
                        <Box component="span" color="primary" ml={2}>
                            Don’t Miss!!
                        </Box>
                    </Box>
                    <Box
                        sx={{
                            fontWeight: "700",
                            mt: "15px",
                            textShadow: " 2px 1px grey",
                        }}
                    >
                        <Typography textAlign="center" variant="h3">
                            Let's Shopping Today
                        </Typography>
                    </Box>
                    <Box
                        display="flex"
                        color="text.primary"
                        sx={{
                            justifyContent: "center",
                            my: 5,
                            "&>div": {
                                display: "flex",
                                height: "80px",
                                width: "80px",
                                backgroundColor: "background.default",
                                borderRadius: "50%",
                                mr: 2,
                                textAlign: "center",
                                fontWeight: 700,
                                fontSize: "24px",
                                flexDirection: "column",
                                justifyContent: "center",
                                fontFamily: "DM Sans",
                                "&>div:last-child": {
                                    fontSize: 16,
                                    opacity: 0.6,
                                },
                            },
                        }}
                    >
                        <Box>
                            <Box>0</Box>
                            <Box>Day</Box>
                        </Box>
                        <Box>
                            <Box>00</Box>
                            <Box>Hrs</Box>
                        </Box>
                        <Box>
                            <Box>00</Box>
                            <Box>Mins</Box>
                        </Box>
                        <Box>
                            <Box>00</Box>
                            <Box>Sec</Box>
                        </Box>
                    </Box>
                    <Box
                        component={Link}
                        width="100%"
                        display="flex"
                        justifyContent="center"
                        to="/shop"
                        sx={{
                            textDecoration: "none",
                        }}
                    >
                        <CustomBtn>
                            Check It Out!
                            <span>
                                <EastIcon />
                            </span>
                        </CustomBtn>
                    </Box>
                </Grid>
            </Grid>
        </Box>
    )
}

export default FlashDeal
