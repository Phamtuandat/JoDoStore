import { Box, Container, Paper } from "@mui/material"
import { Category, Thumbnail } from "models"
// Import Swiper React components
import { Swiper, SwiperSlide } from "swiper/react"

// Import Swiper styles
import "swiper/css"
import "swiper/css/pagination"

// import required modules

type Props = {
    categories: Category[]
}

const CategorySlide = ({ categories }: Props) => {
    return (
        <Container maxWidth="lg">
            <Box
                sx={{
                    height: "200px",
                    px: 10,
                }}
            >
                <Swiper loop={true} slidesPerView={3} centeredSlides={true} spaceBetween={30}>
                    {categories.map((cate) => (
                        <SwiperSlide key={cate.id}>
                            <Paper>{cate.name}</Paper>
                        </SwiperSlide>
                    ))}
                </Swiper>
            </Box>
        </Container>
    )
}

export default CategorySlide
