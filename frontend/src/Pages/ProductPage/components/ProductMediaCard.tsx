import Box from "@mui/material/Box"
import CardMedia from "@mui/material/CardMedia"
import { useTheme } from "@mui/material/styles"
import { AnimatePresence, motion } from "framer-motion"
import { useWidth } from "Hooks/width-hook"
import { Product } from "models"
import { useState } from "react"
import "swiper/css"
import "swiper/css/pagination"
import { Swiper, SwiperSlide } from "swiper/react"
type Props = {
    product: Product
}
const variants = {
    enter: {
        opacity: 0,
    },
    center: {
        zIndex: 1,
        opacity: 1,
    },
}

const ProductMediaCard = ({ product }: Props) => {
    const width = useWidth()
    const [imgPath, setImg] = useState<number>(0)
    const theme = useTheme()
    const handleChangeImage = (value: number) => {
        setImg(value)
    }

    return (
        <Box
            sx={{
                mt: 3,
                mb: 6,
                display: "flex",
                flexDirection: "column",
                position: { xs: "sticky", sm: "static" },
                top: 40,
            }}
        >
            <Box
                sx={{
                    display: "flex",
                    width: { lg: 480, md: 400, sm: 600 },
                    height: { lg: 480, md: 400, sm: 600 },
                    overflow: "hidden",
                    mx: "auto",
                }}
            >
                <AnimatePresence initial={false}>
                    <Box
                        component={motion.img}
                        key={product.imagePaths[imgPath]}
                        variants={variants}
                        initial="enter"
                        animate="center"
                        exit="exit"
                        transition={{
                            opacity: {
                                duration: 0.3,
                                easings: theme.transitions.getAutoHeightDuration,
                            },
                        }}
                        src={product.imagePaths[imgPath]}
                        sx={{
                            width: "100%",
                        }}
                    />
                </AnimatePresence>
            </Box>
            <Box
                className="mySwiper"
                component={Swiper}
                width={{ xs: 300, sm: 450, lg: 500 }}
                mx="auto"
                mt={5}
                p={1}
                slidesPerView={width === "xs" ? 3 : 5}
            >
                {product.imagePaths.map((media, idx) => (
                    <SwiperSlide key={media} onClick={() => handleChangeImage(idx)}>
                        <Box sx={{}}>
                            <CardMedia
                                component="img"
                                image={media}
                                sx={{
                                    objectFit: "contain",
                                    borderRadius: "3px",
                                    overflow: "hidden",
                                    width: 80,
                                    height: 80,
                                    cursor: "pointer",
                                    boxShadow:
                                        idx === imgPath
                                            ? `${theme.palette.secondary.main} 0px 0px 0px 2px, rgba(6, 24, 44, 0.65) 0px 4px 6px -1px, rgba(255, 255, 255, 0.08) 0px 1px 0px inset`
                                            : "none",
                                }}
                            />
                        </Box>
                    </SwiperSlide>
                ))}
            </Box>
        </Box>
    )
}

export default ProductMediaCard
