import LocalOfferOutlinedIcon from "@mui/icons-material/LocalOfferOutlined"
import { Box, Container, Paper } from "@mui/material"
import CardMedia from "@mui/material/CardMedia"
import Typography from "@mui/material/Typography"
import { useWidth } from "Hooks/width-hook"
import { Category } from "models"
import { Pagination } from "swiper"
import "swiper/css"
import "swiper/css/pagination"
import { Swiper, SwiperSlide } from "swiper/react"
type Props = {
    categories: Category[]
}

const CategorySlide = ({ categories }: Props) => {
    const width = useWidth()
    const slidesPerView = () => {
        switch (width) {
            case "lg":
                return 6
            case "md":
                return 4
            case "xs":
                return 3
            case "xl":
                return 6
            default:
                return 6
        }
    }
    return (
        <Container maxWidth="lg">
            <Box mt={10}>
                <Box display="flex" justifyContent="flex-end">
                    <Box component="span" fontWeight="600" color="#ff497c">
                        Categories
                    </Box>
                    <Box
                        component="span"
                        sx={{
                            p: "2px",
                            borderRadius: "50%",
                            color: "white",
                            backgroundColor: "#ff497c",
                            lineHeight: 0,
                            ml: "12px ",
                        }}
                    >
                        <LocalOfferOutlinedIcon />
                    </Box>
                </Box>
                <Typography variant="h4" textAlign="end">
                    Browse by Category
                </Typography>
                <Box mt={6} height="146px">
                    <Swiper
                        grabCursor={true}
                        centeredSlides={true}
                        slidesPerView={slidesPerView()}
                        spaceBetween={30}
                        modules={[Pagination]}
                        className="mySwiper"
                    >
                        {categories.map((x) => (
                            <SwiperSlide key={x.id}>
                                <Paper
                                    variant="outlined"
                                    sx={{
                                        textAlign: "center",
                                        my: "30",
                                        p: 1,
                                    }}
                                >
                                    <CardMedia
                                        sx={{
                                            height: "auto",
                                            width: "auto",
                                            mx: "auto",
                                            p: "28px 12px",
                                        }}
                                        component="img"
                                        image="https://new.axilthemes.com/demo/template/etrade-rtl/assets/images/product/categories/elec-7.png"
                                    />
                                    <Box component="span">{x.name}</Box>
                                </Paper>
                            </SwiperSlide>
                        ))}
                    </Swiper>
                </Box>
            </Box>
        </Container>
    )
}

export default CategorySlide
