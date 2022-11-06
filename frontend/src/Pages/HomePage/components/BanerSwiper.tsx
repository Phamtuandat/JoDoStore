import { CardMedia, Container, Skeleton } from "@mui/material"
import { Box } from "@mui/system"
import { Category } from "models"
import { Autoplay, EffectFade, Pagination } from "swiper"
import "swiper/css"
import "swiper/css/navigation"
import "swiper/css/pagination"
import { Swiper, SwiperSlide } from "swiper/react"
type Props = {
    categories: Category[]
}

export const BanerSwiper = ({ categories }: Props) => {
    return (
        <Container maxWidth="lg">
            <Box sx={{ mt: 5, height: 350 }}>
                <Swiper
                    style={{
                        height: "100%",
                        borderRadius: 10,
                    }}
                    effect={"slide"}
                    grabCursor={true}
                    centeredSlides={true}
                    loop={true}
                    slidesPerView={"auto"}
                    pagination={true}
                    autoplay={{
                        pauseOnMouseEnter: true,
                        disableOnInteraction: false,
                        delay: 5000,
                    }}
                    modules={[Pagination, EffectFade, Autoplay]}
                    className="mySwiper"
                >
                    {categories.length !== 0 ? (
                        categories.map((category) => (
                            <SwiperSlide
                                key={category.id}
                                style={{
                                    marginLeft: "auto",
                                    marginRight: "auto",
                                }}
                            >
                                <CardMedia
                                    component="img"
                                    image="https://source.unsplash.com/random/1200x390"
                                />
                            </SwiperSlide>
                        ))
                    ) : (
                        <SwiperSlide>
                            <Skeleton
                                sx={{
                                    mx: "auto",
                                }}
                                variant="rectangular"
                                width="100%"
                                height="100%"
                            />
                        </SwiperSlide>
                    )}
                </Swiper>
            </Box>
        </Container>
    )
}
