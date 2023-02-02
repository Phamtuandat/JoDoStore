import ArrowBackIcon from "@mui/icons-material/ArrowBack"
import ArrowForwardIcon from "@mui/icons-material/ArrowForward"
import { Hidden } from "@mui/material"
import Button from "@mui/material/Button"
import { styled } from "@mui/material/styles"
import { Box } from "@mui/system"
import { useAppDispatch, useAppSelector } from "app/hooks"
import { ProductItem } from "components/product"
import { productListSelector, ProductSliceActions } from "features/ListProduct/listProductSlice"
import { motion, Variants } from "framer-motion"
import { useWidth } from "Hooks/width-hook"
import { useEffect, useRef } from "react"
import { Grid, Navigation, Pagination } from "swiper"
import "swiper/css"
import "swiper/css/grid"
import "swiper/css/pagination"
import { Swiper, SwiperSlide } from "swiper/react"
type Props = {}
const cardVariants: Variants = {
    offscreen: {
        opacity: 0,
    },
    onscreen: {
        opacity: 1,
        transition: {
            type: "spring",
            duration: 0.8,
        },
    },
}
const CustomizedSlider = styled(SwiperSlide)(({ theme }) => ({
    textAlign: "center",
    height: "calc((100% - 30px) / 2) !important",
}))

const ListProductSale = (props: Props) => {
    const nextRef = useRef(null)
    const backRef = useRef(null)
    const dispatch = useAppDispatch()
    const width = useWidth()
    const ignore = useRef(false)
    const productList = useAppSelector(productListSelector)

    useEffect(() => {
        if (!ignore.current) {
            ignore.current = true
            dispatch(ProductSliceActions.getList({ top: 20 }))
        }
    }, [dispatch])

    const slidesPerView = (): number => {
        switch (width) {
            case "xs":
                return 1
            case "sm":
                return 2
            case "md":
                return 3
            default:
                return 4
        }
    }

    return (
        <Box position="relative">
            <Hidden mdDown>
                <Box textAlign="right" position="absolute" zIndex={10} top={-80} right={0}>
                    <Button
                        sx={{
                            mr: 2,
                            transition: "all  1s ease-out",
                            "&:hover>svg": {
                                transform: "translateX(-15%)",
                            },
                        }}
                        size="large"
                        color="inherit"
                        variant="contained"
                        ref={backRef}
                    >
                        <ArrowBackIcon color="primary" />
                    </Button>
                    <Button
                        sx={{
                            transition: "transform all 0.5s ease-out",
                            "&:hover>svg": {
                                transform: "translateX(15%)",
                            },
                        }}
                        size="large"
                        color="inherit"
                        variant="contained"
                        ref={nextRef}
                    >
                        <ArrowForwardIcon color="primary" />
                    </Button>
                </Box>
            </Hidden>
            <Box
                component={motion.div}
                initial="offscreen"
                whileInView="onscreen"
                viewport={{ once: true, amount: 0.1 }}
            >
                <motion.div variants={cardVariants}>
                    <Box height={{ xs: "auto", md: "840px" }} mt={4}>
                        <Swiper
                            style={{
                                height: "100%",
                            }}
                            slidesPerView={slidesPerView()}
                            grid={{
                                rows: 1,
                            }}
                            navigation={{
                                nextEl: nextRef.current,
                                prevEl: backRef.current,
                            }}
                            spaceBetween={20}
                            modules={[Navigation, Grid, Pagination]}
                        >
                            {productList.length !== 0 &&
                                productList.map((product) => (
                                    <CustomizedSlider key={product.id}>
                                        <ProductItem product={product} />
                                    </CustomizedSlider>
                                ))}
                        </Swiper>
                    </Box>
                </motion.div>
            </Box>
        </Box>
    )
}

export default ListProductSale
