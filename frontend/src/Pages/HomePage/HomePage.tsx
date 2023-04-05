import { Button, CardMedia, Container, Hidden, IconButton, Stack, Typography } from "@mui/material"
import Box from "@mui/material/Box/Box"
import { MainLayout } from "components/Layout/MainLayout"
import { Link } from "react-router-dom"
import Slider, { Settings } from "react-slick"
import "slick-carousel/slick/slick-theme.css"
import "slick-carousel/slick/slick.css"
import { BanerSlider } from "./components/BanerSlider"
import FlashDeal from "./components/FlashDeal"
import ListProductSale from "./components/ListProductSale"
const settings: Settings = {
    infinite: true,
    speed: 500,
    autoplaySpeed: 5000,
    autoplay: true,
    fade: true,
}
export default function HomePage() {
    return (
        <MainLayout>
            <Box>
                <BanerSlider />
                <Container
                    sx={{
                        pt: 10,
                    }}
                >
                    <FlashDeal />
                    <Box mt={15}>
                        <Typography fontWeight={700}>Up to 40% Off</Typography>
                        <ListProductSale />
                    </Box>
                    <Stack mt={15}>
                        <Typography my={2} fontSize="24px">
                            Just in
                        </Typography>
                        <Hidden mdDown>
                            <Stack direction={{ md: "row", xs: "column" }} spacing={1}>
                                <CardMedia
                                    component="img"
                                    image="https://res.cloudinary.com/dmzvhnnkh/image/upload/v1680269114/pat-kwon-EJTjetc8tPs-unsplash_uldwfi.jpg"
                                    sx={{
                                        width: { md: "50%" },
                                        height: { md: 600, xs: 400 },
                                    }}
                                />
                                <CardMedia
                                    component="img"
                                    image="https://res.cloudinary.com/dmzvhnnkh/image/upload/v1680271363/236876275_1489292584756238_2210559954390413264_n_gm0jm0.jpg"
                                    sx={{
                                        width: { md: "50%" },
                                        height: { md: 600, xs: 400 },
                                    }}
                                />
                            </Stack>
                        </Hidden>
                        <Hidden mdUp>
                            <Slider {...settings}>
                                <Box>
                                    <CardMedia
                                        component="img"
                                        image="https://res.cloudinary.com/dmzvhnnkh/image/upload/v1680269114/pat-kwon-EJTjetc8tPs-unsplash_uldwfi.jpg"
                                        sx={{
                                            height: { md: 600, xs: 400 },
                                        }}
                                    />
                                </Box>
                                <Box>
                                    <CardMedia
                                        component="img"
                                        image="https://res.cloudinary.com/dmzvhnnkh/image/upload/v1680271363/236876275_1489292584756238_2210559954390413264_n_gm0jm0.jpg"
                                        sx={{
                                            height: { md: 600, xs: 400 },
                                        }}
                                    />
                                </Box>
                            </Slider>
                        </Hidden>
                        <Box
                            sx={{
                                maxWidth: { xs: 260, md: "100%" },
                                textAlign: { md: "center", xs: "left" },
                            }}
                        >
                            <Typography variant="h2" fontWeight={500} mt={4}>
                                AIR JORDAN 1 MID
                            </Typography>
                            <p>
                                The Air Jordan 1 Mid colourway tricks out the ultimate performance
                                shoe to make a bold statement.
                            </p>
                            <Button onClick={(e) => e.preventDefault()} variant="contained">
                                <Typography
                                    component={Link}
                                    to="/shop"
                                    sx={{ textDecoration: "none", color: "text.primary" }}
                                >
                                    Shop
                                </Typography>
                            </Button>
                        </Box>
                    </Stack>

                    <Stack mt={15}>
                        <Typography my={2} fontSize="24px">
                            The Latest
                        </Typography>
                        <Hidden mdDown>
                            <Stack direction={{ md: "row", xs: "column" }} spacing={1}>
                                <Box
                                    position="relative"
                                    sx={{
                                        width: { md: "50%" },
                                    }}
                                >
                                    <CardMedia
                                        component="img"
                                        image="https://res.cloudinary.com/dmzvhnnkh/image/upload/v1680268492/courtney-cook-kaZ6Uu54ZjE-unsplash_fme5y0.jpg"
                                        sx={{
                                            height: { md: 600, xs: 400 },
                                        }}
                                    />
                                    <Box
                                        sx={{
                                            position: "absolute",
                                            top: 500,
                                            left: 60,
                                        }}
                                    >
                                        <Button variant="contained" color="info">
                                            Read Now
                                        </Button>
                                    </Box>
                                </Box>
                                <CardMedia
                                    component="img"
                                    image="https://res.cloudinary.com/dmzvhnnkh/image/upload/v1680274715/nike_s-best-golf-shoes-for-traction-stability-and-comfort_a2uy9y.jpg"
                                    sx={{
                                        width: { md: "50%" },
                                        height: { md: 600, xs: 400 },
                                    }}
                                />
                            </Stack>
                        </Hidden>
                        <Hidden mdUp>
                            <Slider {...settings}>
                                <Box>
                                    <CardMedia
                                        component="img"
                                        image="https://res.cloudinary.com/dmzvhnnkh/image/upload/v1680274715/nike_s-best-golf-shoes-for-traction-stability-and-comfort_a2uy9y.jpg"
                                        sx={{
                                            height: { md: 600, xs: 400 },
                                        }}
                                    />
                                </Box>
                                <Box
                                    sx={{
                                        position: "relative",
                                    }}
                                >
                                    <CardMedia
                                        component="img"
                                        image="https://res.cloudinary.com/dmzvhnnkh/image/upload/v1680268492/courtney-cook-kaZ6Uu54ZjE-unsplash_fme5y0.jpg"
                                        sx={{
                                            height: { md: 600, xs: 400 },
                                        }}
                                    />
                                    <Box
                                        sx={{
                                            position: "absolute",
                                            top: 300,
                                            left: 0,
                                        }}
                                    >
                                        <Typography zIndex={100}>
                                            As a Nike Member, you have access to benefits and
                                            services that make your shopping worry-free.
                                        </Typography>
                                        <IconButton>Read Now</IconButton>
                                    </Box>
                                </Box>
                            </Slider>
                        </Hidden>
                    </Stack>
                </Container>
            </Box>
        </MainLayout>
    )
}
