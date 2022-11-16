import ShoppingBasketOutlinedIcon from "@mui/icons-material/ShoppingBasketOutlined"
import { Container, Typography } from "@mui/material"
import Button from "@mui/material/Button"
import { styled, useTheme } from "@mui/material/styles"
import { Box } from "@mui/system"
import { productApi } from "ApiClients/ProductApi"
import { ProductItem } from "components/product"
import { motion, Variants } from "framer-motion"
import { useWidth } from "Hooks/width-hook"
import { Product } from "models"
import { useEffect, useState } from "react"
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
    backgroundColor: theme.palette.background.default,
    height: "calc((100% - 30px) / 2) !important",
}))

const ListProductSale = (props: Props) => {
    const theme = useTheme()

    const [productList, setProductList] = useState<Product[]>([])

    const width = useWidth()
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
    useEffect(() => {
        ;(async () => {
            const result = await productApi.getList()
            setProductList(result.data)
        })()
    }, [])

    return (
        <Container
            maxWidth="lg"
            sx={{
                mt: 15,
            }}
        >
            <Box
                component={motion.div}
                initial="offscreen"
                whileInView="onscreen"
                viewport={{ once: true, amount: 0.5 }}
            >
                <motion.div variants={cardVariants}>
                    <Box display="flex" alignItems="center">
                        <Box
                            sx={{
                                p: 0.3,
                                borderRadius: "50%",
                                width: "fit-content",
                                lineHeight: 0,
                                mr: 1,
                                bgcolor: theme.palette.secondary.main,
                            }}
                        >
                            <ShoppingBasketOutlinedIcon />
                        </Box>
                        <Typography component="span" lineHeight={0} fontWeight={700}>
                            Our Products
                        </Typography>
                    </Box>
                    <Typography variant="h4" mt={4}>
                        Explore our Products
                    </Typography>
                    <Box height="840px" mt={4}>
                        <Swiper
                            style={{
                                height: "100%",
                                padding: "10px",
                            }}
                            slidesPerView={slidesPerView()}
                            grid={{
                                rows: 2,
                            }}
                            spaceBetween={20}
                            modules={[Navigation, Grid, Pagination]}
                        >
                            {productList.map((product) => (
                                <CustomizedSlider key={product.id}>
                                    <ProductItem product={product} />
                                </CustomizedSlider>
                            ))}
                        </Swiper>
                    </Box>
                </motion.div>
            </Box>
            <Box maxWidth="250px" mx="auto">
                <Button
                    variant="outlined"
                    color="secondary"
                    sx={{
                        transition: "transform 200ms",
                        "&:hover": {
                            transform: "scale(1.05)",
                        },
                    }}
                    size="large"
                >
                    View All Products
                </Button>
            </Box>
        </Container>
    )
}

export default ListProductSale
