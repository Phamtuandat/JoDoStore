import EastIcon from "@mui/icons-material/East"
import { Box, Hidden, Skeleton, Typography } from "@mui/material"
import { styled } from "@mui/material/styles"
import PhotoApi from "ApiClients/PhotoApi"
import { useWidth } from "Hooks/width-hook"
import { motion, useAnimationControls, useMotionValue } from "framer-motion"
import { Photo } from "models"
import buildQuery from "odata-query"
import { useEffect, useRef, useState } from "react"
import { Link } from "react-router-dom"
import Slider, { Settings } from "react-slick"
import "slick-carousel/slick/slick-theme.css"
import "slick-carousel/slick/slick.css"

const CustomBtn = styled("button")(({ theme }) => ({
    backgroundColor: theme.palette.background.default,
    cursor: "pointer",
    display: "flex",
    alignItems: "center",
    fontSize: "inherit",
    width: "100%",
    height: "100%",
    justifyContent: "center",
    color: theme.palette.text.primary,
    border: "1px solid #ccc",
    padding: "2px",
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

export const BanerSlider = () => {
    const ignore = useRef(false)
    const width = useWidth()
    const textStyles = {
        y: useMotionValue(50),
        opacity: useMotionValue(1),
    }
    const btnStyles = {
        opacity: useMotionValue(1),
    }
    const textCtrl = useAnimationControls()
    const btnCtrl = useAnimationControls()
    const [images, setImages] = useState<Photo[] | []>()
    const [currentSlide, setCurrentSlide] = useState<number>(0)
    const handleBeforeChange = (oldIndex: number, newIndex: number) => {
        textCtrl.set({
            opacity: 0,
            y: 300,
        })
        btnCtrl.set({
            opacity: 0,
        })
        setCurrentSlide(newIndex)
    }
    const settings: Settings = {
        infinite: true,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplaySpeed: 5000,
        autoplay: true,
        fade: true,
        dots: true,
        draggable: true,
    }
    useEffect(() => {
        if (!ignore.current) {
            ignore.current = true
            ;(async () => {
                const param = buildQuery({
                    filter: {
                        imageCollections: {
                            name: "hero",
                        },
                    },
                })

                const list = await (await PhotoApi.getAll(param)).data
                setImages(list)
            })()
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])
    useEffect(() => {
        textCtrl.start({
            opacity: 1,
            y: width !== "xs" ? 50 : 0,
            transition: { duration: 1.5 },
        })
        btnCtrl.start({
            opacity: 1,
            transition: { delay: width !== "xs" ? 1.2 : 0, duration: 0.7 },
        })
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [currentSlide])
    return (
        <Box>
            <Box
                sx={{
                    overflow: "hidden",
                    position: "relative",
                    mt: "60px",
                }}
            >
                <Slider {...settings} beforeChange={handleBeforeChange}>
                    {images?.map((slide) => (
                        <Box
                            key={slide.id}
                            sx={{
                                backgroundImage: `url(${slide.imageUrl})`,
                                backgroundSize: "contain",
                                backgroundPosition: "top center",
                                paddingBottom: "57%",
                                width: "100%",
                                backgroundRepeat: "no-repeat",
                                px: 0,
                            }}
                        />
                    )) || (
                        <Skeleton animation="wave" variant="rectangular" width="100%">
                            <Box
                                sx={{
                                    backgroundSize: "contain",
                                    backgroundPosition: "top center",
                                    paddingBottom: "57%",
                                    width: "100%",
                                    backgroundRepeat: "no-repeat",
                                    px: 0,
                                }}
                            />
                        </Skeleton>
                    )}
                </Slider>
                <Box
                    sx={{
                        width: "fit-context",
                        height: { xs: "fit-content", md: 300 },
                        position: "absolute",
                        top: { md: "40%", xs: "50%" },
                        left: { xs: "50%", md: 0 },
                        transform: { md: "translate(5%)", xs: "translate(-100%, 20%)" },
                        overflow: "hidden",
                        ml: 10,
                    }}
                >
                    <Hidden mdDown>
                        <motion.div animate={textCtrl} style={textStyles}>
                            <Typography
                                color="white"
                                variant="h2"
                                fontFamily="fantasy"
                                letterSpacing={1}
                                my={1}
                            >
                                Lorem ipsum
                            </Typography>
                            <Typography color="white" variant="h6" component="span">
                                sit amet adipisicing elit!
                            </Typography>
                        </motion.div>
                        <motion.div animate={btnCtrl} style={btnStyles}>
                            <Box
                                component={Link}
                                to="/shop"
                                sx={{
                                    textDecoration: "none",
                                }}
                            >
                                <Box
                                    width={{ md: 250, xs: 150 }}
                                    height={{ xs: 50, md: 70 }}
                                    mt={{ md: 10 }}
                                >
                                    <CustomBtn>
                                        Shop now!
                                        <span>
                                            <EastIcon />
                                        </span>
                                    </CustomBtn>
                                </Box>
                            </Box>
                        </motion.div>
                    </Hidden>
                </Box>
            </Box>
            <Hidden smUp>
                <Box
                    sx={{
                        text: "text.primary",
                        textAlign: "center",
                    }}
                >
                    <Typography variant="h2" fontFamily="fantasy" letterSpacing={1} my={1}>
                        Lorem ipsum
                    </Typography>
                    <Typography variant="h6" component="span">
                        sit amet adipisicing elit!
                    </Typography>
                    <Box
                        component={Link}
                        to="/shop"
                        sx={{
                            textDecoration: "none",
                        }}
                    >
                        <Box
                            height={{ xs: 50, md: 70 }}
                            mt={{ sm: 40, md: 10 }}
                            sx={{
                                color: "text.primary",
                                px: { sm: 13, xs: 2 },
                            }}
                        >
                            <CustomBtn>
                                Shop now!
                                <span>
                                    <EastIcon />
                                </span>
                            </CustomBtn>
                        </Box>
                    </Box>
                </Box>
            </Hidden>
        </Box>
    )
}
