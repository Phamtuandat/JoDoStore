import { Box, Grid } from "@mui/material"
import { useAppSelector } from "app/hooks"
import { ListPageProgress, ProductItem } from "components/product"
import { processingSeclector, productListSelector } from "features/ListProduct/listProductSlice"

type Props = {}

const ListPage = (props: Props) => {
    const productList = useAppSelector(productListSelector)
    const isLoading = useAppSelector(processingSeclector)

    return (
        <Box
            sx={{
                width: "100%",
                height: "100%",
                p: 2,
                mt: 2,
            }}
        >
            {!isLoading ? (
                <Grid container spacing={5}>
                    {productList.map((product) => (
                        <Grid item lg={3} sm={6} key={product.id}>
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
