import ShoppingBasketOutlinedIcon from "@mui/icons-material/ShoppingBasketOutlined"
import { Container, Typography } from "@mui/material"
import Box from "@mui/material/Box/Box"
import { useTheme } from "@mui/material/styles"
import { MainLayout } from "components/Layout/MainLayout"
import { BanerSlider } from "./components/BanerSlider"
import FlashDeal from "./components/FlashDeal"
import ListProductSale from "./components/ListProductSale"

export default function HomePage() {
    const theme = useTheme()

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
                        <Box display="flex" alignItems="center">
                            <Box
                                sx={{
                                    p: 0.3,
                                    borderRadius: "50%",
                                    width: "fit-content",
                                    lineHeight: 0,
                                    mr: 1,
                                    bgcolor: theme.palette.primary.main,
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
                        <ListProductSale />
                    </Box>
                </Container>
            </Box>
        </MainLayout>
    )
}
