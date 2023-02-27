import FilterAltIcon from "@mui/icons-material/FilterAlt"
import { Box, Container, Divider, Drawer, Hidden, IconButton, Stack } from "@mui/material"
import { useAppDispatch } from "app/hooks"
import { MainLayout } from "components/Layout/MainLayout"
import { ProductSliceActions } from "features/ListProduct/listProductSlice"
import qs from "qs"
import { useEffect, useMemo, useState } from "react"
import { useLocation, useNavigate } from "react-router-dom"
import ListPage from "./components/ListPage"
import MenuFilterOption from "./components/MenuFilter"
import ProductSort from "./components/ProductSort"
import { debounce } from "lodash"
export type Params = {
    gt: number
    lt: number
    category: { name: { in: string[] } } | undefined
    brand: { name: { in: string[] } } | undefined
    orderBy?: string
}
const ProductShopPage = () => {
    // const theme = useTheme()
    const [isDebouce, setDebouce] = useState(false)
    const navigate = useNavigate()
    const location = useLocation()
    const dispatch = useAppDispatch()
    const [opentFilter, setOpentFilter] = useState(false)
    const [param, setParams] = useState<Params>({
        gt: 30,
        lt: 3000,
        category: { name: { in: [] } },
        brand: { name: { in: [] } },
        orderBy: "salePrice desc",
    })

    const handleClose = () => {
        setOpentFilter(false)
    }

    const queryParam = useMemo(() => {
        const params = qs.parse(location.search.slice(1))
        return {
            brand: params.brand,
            category: params.category,
            gt: Number(params["gt"]) || 30,
            lt: Number(params.lt) || 3000,
            orderBy: params.orderBy,
        }
    }, [location.search])

    useEffect(() => {
        setDebouce(true)
        const param = {
            filter: {
                category: queryParam.category as { name: { in: string[] } },
                brand: queryParam.brand as { name: { in: string[] } },
                salePrice: { gt: queryParam.gt, lt: queryParam.lt },
            },
            orderBy: queryParam.orderBy as string,
        }
        setParams({
            brand: {
                name: param.filter.brand?.name || { in: [] },
            },
            category: {
                name: param.filter.category?.name || { in: [] },
            },
            gt: param.filter.salePrice?.gt || 30,
            lt: param.filter.salePrice?.lt || 3000,
            orderBy: param.orderBy || "salePrice desc",
        })
        const debouncedEffect = debounce(() => {
            dispatch(ProductSliceActions.getList(param))
            dispatch(ProductSliceActions.setFilter(param))
            setDebouce(false)
        }, 1000)
        debouncedEffect()
        return () => debouncedEffect.cancel()
        // dispatch(ProductSliceActions.getList(param))
        // dispatch(ProductSliceActions.setFilter(param))
    }, [queryParam, dispatch])

    const handleFilterChange = (value: Params) => {
        const newParam = {
            ...param,
            ...value,
        }

        setParams(newParam)
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
                <Box bgcolor="background.default">
                    <Stack direction="row" spacing={1}>
                        <Drawer anchor="left" open={opentFilter} onClose={handleClose}>
                            <Box width={300} p={2} pt={5}>
                                <MenuFilterOption
                                    handleFilterChange={handleFilterChange}
                                    param={param}
                                    queryParam={queryParam}
                                />
                            </Box>
                        </Drawer>
                        <Hidden mdDown>
                            <Box width="360px">
                                <MenuFilterOption
                                    handleFilterChange={handleFilterChange}
                                    param={param}
                                    queryParam={queryParam}
                                />
                            </Box>
                        </Hidden>
                        <Box
                            sx={{
                                width: "100%",
                            }}
                        >
                            <Box
                                sx={{
                                    display: "flex",
                                    justifyContent: "space-between",
                                }}
                            >
                                <ProductSort handleSortChange={handleSortChange} />
                                <Hidden mdUp>
                                    <Box
                                        sx={{
                                            alignSelf: "end",
                                        }}
                                    >
                                        <IconButton onClick={() => setOpentFilter(true)}>
                                            <FilterAltIcon />
                                        </IconButton>
                                    </Box>
                                </Hidden>
                            </Box>
                            <Divider
                                sx={{
                                    mt: 1,
                                    boxShadow: "rgba(33, 35, 38, 0.1) 0px 10px 10px -10px",
                                }}
                            />
                            <ListPage debouce={isDebouce} />
                        </Box>
                    </Stack>
                </Box>
            </Container>
        </MainLayout>
    )
}

export default ProductShopPage
