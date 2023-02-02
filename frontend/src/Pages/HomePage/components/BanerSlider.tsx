import { Box, Button } from "@mui/material"
import thumbApi from "ApiClients/ThumbApi"
import { AnimatePresence, motion, useAnimationControls, wrap } from "framer-motion"
import { Thumbnail } from "models"
import buildQuery from "odata-query"
import { useEffect, useRef, useState } from "react"
// import CategorySlide from "./CategorySlide"
const variants = {
    enter: (direction: number) => {
        return {
            x: direction > 0 ? 1000 : -1000,
            opacity: 0,
        }
    },
    center: {
        zIndex: 1,
        x: 0,
        opacity: 1,
    },
    exit: (direction: number) => {
        return {
            zIndex: 0,
            x: direction < 0 ? 1000 : -1000,
            opacity: 0,
        }
    },
}

const swipeConfidenceThreshold = 10000
const swipePower = (offset: number, velocity: number) => {
    return Math.abs(offset) * velocity
}

export const BanerSlider = () => {
    const [isMounted, setIsMounted] = useState(false)
    const controls = useAnimationControls()
    const ignore = useRef(false)
    const [images, setImages] = useState<Thumbnail[]>([])
    const [[page, direction], setPage] = useState([0, 0])
    // const [categories, setCategories] = useState<Category[]>([])
    const imageIndex = wrap(0, images.length, page)
    useEffect(() => {
        setIsMounted(true)
        if (!ignore.current) {
            ignore.current = true
            ;(async () => {
                const param = buildQuery({
                    filter: {
                        imageCollections: {
                            name: "Banner",
                        },
                    },
                })
                // const result = await categoryApi.getAll()
                // setCategories(result.data)
                const mediaResource = (await thumbApi.getAll(param)).data
                setImages(mediaResource)
            })()
            return () => {
                setIsMounted(false)
            }
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    const paginate = (newDirection: number) => {
        setPage([page + newDirection, newDirection])
    }

    return (
        <Box>
            <Box>
                <Box
                    sx={{
                        display: "flex",
                        justifyContent: "center",
                        position: "relative",
                        overflow: "hidden",
                        "&::before": {
                            content: `"  "`,
                            position: "absolute",
                            top: 0,
                            bottom: 0,
                            right: "50%",
                            transform: "translateX(50%)",
                            height: "100%",
                            width: "100%",
                        },
                        height: "600px",
                        width: "100%",
                        alignItems: "center",
                    }}
                >
                    <AnimatePresence custom={direction} initial={false}>
                        <Box
                            component={motion.img}
                            src={images[imageIndex]?.imageUrl}
                            key={page}
                            sx={{
                                height: { xs: "100%", md: "100%" },
                                width: { xs: "100%", md: "100%" },
                                position: "absolute",
                            }}
                            custom={direction}
                            variants={variants}
                            initial="enter"
                            animate="center"
                            exit="exit"
                            transition={{
                                x: {
                                    type: "spring",
                                    stiffness: 300,
                                    damping: 30,
                                },
                                opacity: { duration: 0.1 },
                            }}
                            drag="x"
                            dragConstraints={{ left: 0, right: 0 }}
                            dragElastic={1}
                            onDragEnd={(e, { offset, velocity }) => {
                                const swipe = swipePower(offset.x, velocity.x)
                                if (swipe < -swipeConfidenceThreshold) {
                                    paginate(1)
                                } else if (swipe > swipeConfidenceThreshold) {
                                    paginate(-1)
                                }
                            }}
                            onDragStart={() => {
                                if (isMounted) {
                                    controls.start({
                                        x: 0,
                                        opacity: 0,
                                    })
                                }
                            }}
                            onAnimationComplete={() =>
                                controls.start({
                                    x: "100%",
                                    transition: { duration: 1 },
                                    opacity: 1,
                                })
                            }
                        />
                        <motion.div style={{ zIndex: 10, opacity: 0 }} animate={controls}>
                            <Button variant="contained">Shop now!</Button>
                        </motion.div>
                    </AnimatePresence>
                </Box>
            </Box>
        </Box>
    )
}
