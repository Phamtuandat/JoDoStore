import { Box, Grid } from "@mui/material"
import { ListPageProgress, ProductItem } from "components/product"
import { Product } from "models"

type Props = {
    productList: Product[]
    isLoading: boolean
}

const ListPage = ({ productList, isLoading }: Props) => {
    return (
        <Box
            sx={{
                width: "100%",
                height: "100%",
                p: { xs: 0, md: 2 },
                mt: { xs: 2, md: 2 },
            }}
        >
            {!isLoading ? (
                <Grid container spacing={{ xs: 1, md: 4 }}>
                    {productList.map((product) => (
                        <Grid item lg={4} md={4} sm={4} xs={6} key={product.id}>
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
