import { Box, Grid } from "@mui/material"
import { useAppSelector } from "app/hooks"
import { ListPageProgress, ProductItem } from "components/product"
import { processingSeclector, productListSelector } from "features/ListProduct/listProductSlice"

type Props = {
    debouce?: boolean
}

const ListPage = ({ debouce }: Props) => {
    const productList = useAppSelector(productListSelector)
    const isLoading = useAppSelector(processingSeclector)

    return (
        <Box
            sx={{
                width: "100%",
                height: "100%",
                p: { xs: 0, md: 2 },
                mt: { xs: 2, md: 2 },
            }}
        >
            {!isLoading && !debouce ? (
                <Grid container spacing={{ xs: 1, md: 4 }}>
                    {productList.map((product) => (
                        <Grid item lg={3} md={4} sm={4} xs={6} key={product.id}>
                            <Box
                                sx={{
                                    borderTopRightRadius: "10px",
                                    borderTopLeftRadius: "10px",
                                    overflow: "hidden",
                                }}
                            >
                                <ProductItem product={product} />
                            </Box>
                        </Grid>
                    ))}
                </Grid>
            ) : (
                <ListPageProgress />
            )}
        </Box>
    )
}

export default ListPage
