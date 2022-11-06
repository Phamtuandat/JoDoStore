import ShoppingBasketOutlinedIcon from "@mui/icons-material/ShoppingBasketOutlined"
import { Container, Typography } from "@mui/material"
import Button from "@mui/material/Button"
import { styled, useTheme } from "@mui/material/styles"
import { Box } from "@mui/system"
import { ProductItem } from "components/product"
import { motion, Variants } from "framer-motion"
import { useWidth } from "Hooks/width-hook"
import { Product } from "models"
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

const product: Product = {
    id: 1,
    brand: { name: "Dat", id: 1 },
    categories: [{ name: "novel", id: 1 }],
    descriptions: "",
    name: "suoi nguon",
    price: 100,
    priceSale: 15,
    smallImageLink: "",
    thumbnail: "",
}
const ListProductSale = (props: Props) => {
    const theme = useTheme()
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
                                bgcolor: theme.palette.primary.main,
                                borderRadius: "50%",
                                width: "fit-content",
                                lineHeight: 0,
                                mr: 1,
                                color: "text.secondary",
                            }}
                        >
                            <ShoppingBasketOutlinedIcon />
                        </Box>
                        <Typography
                            component="span"
                            color="text.secondary"
                            lineHeight={0}
                            fontWeight={700}
                        >
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
                            spaceBetween={30}
                            modules={[Navigation, Grid, Pagination]}
                        >
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                            <CustomizedSlider>
                                <ProductItem product={product} />
                            </CustomizedSlider>
                        </Swiper>
                    </Box>
                </motion.div>
            </Box>
            <Box maxWidth="250px" mx="auto">
                <Button
                    variant="contained"
                    color="info"
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
