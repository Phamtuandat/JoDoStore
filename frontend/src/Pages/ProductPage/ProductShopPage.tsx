import { Box, Container, Stack } from "@mui/material"
import { useAppDispatch } from "app/hooks"
import { MainLayout } from "components/Layout/MainLayout"
import { ProductSliceActions } from "features/ListProduct/listProductSlice"
import qs from "qs"
import { useEffect, useMemo, useState } from "react"
import { useLocation, useNavigate } from "react-router-dom"
import ListPage from "./components/ListPage"
import MenuFilterOption from "./components/MenuFilter"
import ProductSort from "./components/ProductSort"
export type Params = {
    gt: number
    lt: number
    category: { name: { in: string[] } } | undefined
    brand: { name: { in: string[] } } | undefined
    orderBy?: string
}
const ProductShopPage = () => {
    // const theme = useTheme()
    const navigate = useNavigate()
    const location = useLocation()
    const dispatch = useAppDispatch()

    const [param, setParams] = useState<Params>({
        gt: 30,
        lt: 3000,
        category: { name: { in: [] } },
        brand: { name: { in: [] } },
        orderBy: "salePrice desc",
    })

    const queryParam = useMemo(() => {
        const params = qs.parse(location.search)
        return {
            brand: params.brand,
            category: params.category,
            gt: Number(params.gt) || 30,
            lt: Number(params.lt) || 3000,
            orderBy: params.orderBy,
        }
    }, [location.search])

    useEffect(() => {
        const param = {
            filter: {
                category: queryParam.category as { name: { in: string[] } },
                brand: queryParam.brand as { name: { in: string[] } },
                salePrice: { gt: queryParam.gt, lt: queryParam.lt },
            },
            orderBy: queryParam.orderBy as string,
        }
        dispatch(ProductSliceActions.getList(param))
        dispatch(ProductSliceActions.setFilter(param))
    }, [queryParam, dispatch])

    const handleFilterChange = (value: Params) => {
        const newParam = {
            ...param,
            ...value,
        }
        setParams(newParam)
        console.log(newParam)
        navigate({
            pathname: location.pathname,
            search: qs.stringify(newParam),
        })
    }
    const handleSortChange = (value: "salePrice desc" | "salePrice asc") => {
        const newParam = {
            ...param,
            orderBy: value,
        }
        setParams(newParam)
        navigate({
            pathname: location.pathname,
            search: qs.stringify(newParam),
        })
    }
    return (
        <MainLayout>
            <Container
                maxWidth="xl"
                sx={{
                    pt: 20,
                }}
            >
                <Stack direction="row" spacing={1}>
                    <Box width="360px">
                        <MenuFilterOption
                            handleFilterChange={handleFilterChange}
                            param={param}
                            queryParam={queryParam}
                        />
                    </Box>
                    <Box
                        sx={{
                            width: "100%",
                        }}
                    >
                        <Box>
                            <ProductSort handleSortChange={handleSortChange} />
                        </Box>
                        <ListPage />
                    </Box>
                </Stack>
            </Container>
        </MainLayout>
    )
}

export default ProductShopPage
