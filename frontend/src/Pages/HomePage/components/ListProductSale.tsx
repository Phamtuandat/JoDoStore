import ArrowBackIcon from "@mui/icons-material/ArrowBack"
import ArrowForwardIcon from "@mui/icons-material/ArrowForward"
import { Hidden } from "@mui/material"
import Button from "@mui/material/Button"
import { styled } from "@mui/material/styles"
import { Box } from "@mui/system"
import { productApi } from "ApiClients/ProductApi"
import { useWidth } from "Hooks/width-hook"
import { useAppDispatch } from "app/hooks"
import { ProductItem } from "components/product"
import { Variants, motion } from "framer-motion"
import { Product } from "models"
import buildQuery, { Filter } from "odata-query"
import { useEffect, useRef, useState } from "react"
import { Navigation, Pagination } from "swiper"
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
    const [productList, setProductList] = useState<Product[]>([])

    useEffect(() => {
        if (!ignore.current) {
            ignore.current = true
            ;(async () => {
                const filter: Filter = {
                    tags: {
                        any: {
                            name: "In Stock",
                        },
                    },
                }
                const param = buildQuery({ filter })
                const res = await productApi.getList(param)
                setProductList(res.data)
            })()
        }
    }, [dispatch])

    const slidesPerView = (): number => {
        switch (width) {
            case "xs":
                return 1.5
            case "sm":
                return 2.5
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
                    <Box height="auto" mt={4}>
                        <Swiper
                            slidesPerView={slidesPerView()}
                            navigation={{
                                nextEl: nextRef.current,
                                prevEl: backRef.current,
                            }}
                            spaceBetween={20}
                            modules={[Navigation, Pagination]}
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
