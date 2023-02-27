import EastIcon from "@mui/icons-material/East"
import HeadsetMicOutlinedIcon from "@mui/icons-material/HeadsetMicOutlined"
import { Box, Paper, Typography } from "@mui/material"
import { styled, useTheme } from "@mui/material/styles"
import Grid from "@mui/material/Unstable_Grid2"
import { motion } from "framer-motion"
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
        <Paper
            elevation={3}
            sx={{
                mt: width === "xs" ? 5 : 25,
                p: 4,
            }}
        >
            <Grid container spacing={2}>
                <Grid
                    xs={12}
                    md={6}
                    sx={{
                        display: "flex",
                        flexDirection: "column",
                        textAlign: "center",
                        justifyContent: "center",
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
                        }}
                    >
                        <Typography textAlign="center" variant="h3">
                            Let's Shopping Today
                        </Typography>
                    </Box>
                    <Box
                        display="flex"
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
                <Grid xs={12} md={6} sx={{ mt: 4, display: "flex", justifyContent: "center" }}>
                    <Box
                        component={motion.div}
                        sx={{
                            backgroundImage:
                                "url(https://new.axilthemes.com/demo/template/etrade/assets/images/product/poster/poster-03.png) ",
                            mt: { lg: "-160px", md: 0, sx: 50 },
                            backgroundSize: "contain",
                            backgroundRepeat: "no-repeat",
                            backgroundPosition: "center",
                            p: { xs: "156px", md: "240px" },
                            position: "relative",
                        }}
                    >
                        <Box
                            position="absolute"
                            sx={{
                                display: "block",
                                top: "48%",
                                left: "44%",
                                width: { xs: "60px", md: "100px" },
                                height: { xs: "60px", md: "100px" },
                                transform: "rotate(-100deg)",
                            }}
                        >
                            <Box
                                component={motion.span}
                                sx={{
                                    position: "absolute",
                                    zIndex: 100,
                                    display: "block",
                                    width: "100%",
                                    height: "100%",
                                    bottom: 0,
                                    left: 0,
                                    borderColor: "secondary.main",
                                    borderStyle: "solid",
                                    borderWidth: "1px 4px 0 0",
                                    borderRadius: "0 100% 0 0",
                                    opacity: 0,
                                }}
                                animate={{
                                    opacity: 1,
                                }}
                                transition={{
                                    delay: 0.8,
                                    duration: 3,
                                    ease: "easeOut",
                                    repeat: Infinity,
                                }}
                            />
                            <Box
                                component={motion.span}
                                sx={{
                                    position: "absolute",
                                    zIndex: 100,
                                    display: "block",
                                    width: "80%",
                                    height: "80%",
                                    bottom: 0,
                                    left: 0,
                                    borderColor: "secondary.main",
                                    borderStyle: "solid",
                                    borderWidth: "1px 4px 0 0",
                                    borderRadius: "0 100% 0 0",
                                    opacity: 0,
                                }}
                                animate={{
                                    opacity: 1,
                                }}
                                transition={{
                                    delay: 0.6,
                                    duration: 3,
                                    ease: "easeOut",
                                    repeat: Infinity,
                                }}
                            />
                            <Box
                                component={motion.span}
                                sx={{
                                    position: "absolute",
                                    zIndex: 100,
                                    display: "block",
                                    width: "60%",
                                    height: "60%",
                                    bottom: 0,
                                    left: 0,
                                    borderColor: "secondary.main",
                                    borderStyle: "solid",
                                    borderWidth: "1px 4px 0 0",
                                    borderRadius: "0 100% 0 0",
                                    opacity: 0,
                                }}
                                animate={{
                                    opacity: 1,
                                }}
                                transition={{
                                    delay: 0.4,
                                    duration: 3,
                                    repeat: Infinity,
                                    ease: "easeOut",
                                }}
                            />
                            <Box
                                component={motion.span}
                                sx={{
                                    position: "absolute",
                                    zIndex: 100,
                                    display: "block",
                                    width: "40%",
                                    height: "40%",
                                    bottom: 0,
                                    left: 0,
                                    borderColor: "secondary.main",
                                    borderStyle: "solid",
                                    borderWidth: "1px 4px 0 0",
                                    borderRadius: "0 100% 0 0",
                                    opacity: 0,
                                }}
                                animate={{
                                    opacity: 1,
                                }}
                                transition={{
                                    delay: 0.2,
                                    duration: 3,
                                    repeat: Infinity,
                                    ease: "easeOut",
                                }}
                            />
                            <Box
                                component={motion.span}
                                sx={{
                                    position: "absolute",
                                    zIndex: 100,
                                    display: "block",
                                    width: "20%",
                                    height: "20%",
                                    bottom: 0,
                                    left: 0,
                                    borderColor: "secondary.main",
                                    borderStyle: "solid",
                                    borderWidth: "1px 4px 0 0",
                                    borderRadius: "0 100% 0 0",
                                    opacity: 0,
                                }}
                                animate={{
                                    opacity: 1,
                                }}
                                transition={{
                                    delay: 0,
                                    ease: "easeOut",
                                    duration: 3,
                                    repeat: Infinity,
                                }}
                            />
                        </Box>
                    </Box>
                </Grid>
            </Grid>
        </Paper>
    )
}

export default FlashDeal
