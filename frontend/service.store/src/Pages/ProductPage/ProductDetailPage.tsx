/* eslint-disable react-hooks/exhaustive-deps */
import ShoppingBasketOutlinedIcon from "@mui/icons-material/ShoppingBasketOutlined"
import { Box, Divider, Grid, Typography } from "@mui/material"
import { useTheme } from "@mui/material/styles"
import { Container } from "@mui/system"
import ListProductSale from "Pages/HomePage/components/ListProductSale"
import { useAppDispatch } from "app/hooks"
import { MainLayout } from "components/Layout/MainLayout"
import { ProductSliceActions } from "features/ListProduct/listProductSlice"
import { Product } from "models"
import { Suspense, lazy, useEffect, useRef } from "react"
import { useLoaderData, useParams, useRevalidator } from "react-router-dom"
import ProductContent from "./components/ProductContent"
import ProductMediaCard from "./components/ProductMediaCard"

const DescptionComponent = lazy(() => import("./components/ProductDesc"))

type Props = {}

const ProductDetailPage = (props: Props) => {
    const ignore = useRef(false)
    const theme = useTheme()
    const { productId } = useParams()
    const revalidator = useRevalidator()
    const product = useLoaderData() as Product
    const dispath = useAppDispatch()

    useEffect(() => {
        if (!ignore.current) {
            revalidator.revalidate()
            window.scrollTo({ top: 0, left: 0, behavior: "smooth" })
            dispath(
                ProductSliceActions.getList({
                    filter: {
                        not: {
                            id: product.id,
                        },
                    },
                })
            )
        }
    }, [productId])

    return (
        <MainLayout>
            <Container maxWidth="xl">
                <Box pt={20} px={2} mb={10}>
                    <Grid container spacing={5} sx={{ position: "relative" }}>
                        <Grid item lg={6} md={6} sm={12} xs={12} textAlign="center">
                            <Box
                                sx={{
                                    position: "relative",
                                    height: "100%",
                                }}
                            >
                                <ProductMediaCard product={product} />
                            </Box>
                        </Grid>
                        <Grid item lg={6} md={6} sm={12} xs={12}>
                            <ProductContent product={product} />
                        </Grid>
                    </Grid>
                </Box>
                <Divider />
                <Box mt={3}>
                    <Suspense fallback={<div>Loading...</div>}>
                        <DescptionComponent desc={product.description} />
                    </Suspense>
                    <Box mt={5}>
                        <Divider />
                        <Box
                            fontSize={"16px"}
                            display="flex"
                            width="100px"
                            justifyContent="space-between"
                            alignContent="center"
                            mt={3}
                        >
                            <Box
                                component="span"
                                sx={{
                                    p: 0.3,
                                    lineHeight: 0,
                                    bgcolor: theme.palette.primary.main,
                                    borderRadius: "50%",
                                }}
                            >
                                <ShoppingBasketOutlinedIcon />
                            </Box>
                            <Box
                                color={theme.palette.primary.main}
                                component="span"
                                alignSelf="center"
                            >
                                Related
                            </Box>
                        </Box>
                        <Typography component="span" variant="h4">
                            Related Product
                        </Typography>
                    </Box>
                    <ListProductSale />
                </Box>
            </Container>
        </MainLayout>
    )
}

export default ProductDetailPage
