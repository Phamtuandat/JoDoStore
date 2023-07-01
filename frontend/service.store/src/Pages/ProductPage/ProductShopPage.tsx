import FilterAltIcon from "@mui/icons-material/FilterAlt"
import {
    Box,
    Container,
    Divider,
    Drawer,
    Hidden,
    IconButton,
    Pagination,
    Stack,
} from "@mui/material"
import { productApi } from "ApiClients/ProductApi"
import { useAppDispatch } from "app/hooks"
import { MainLayout } from "components/Layout/MainLayout"
import { debounce } from "lodash"
import { ListParams, PaginationMetadata, Product } from "models"
import qs from "qs"
import { ChangeEvent, useEffect, useMemo, useState } from "react"
import { useLocation, useNavigate } from "react-router-dom"
import handleNotify from "utils/Toast-notify"
import ListPage from "./components/ListPage"
import MenuFilterOption from "./components/MenuFilter"
import ProductSort from "./components/ProductSort"

const ProductShopPage = () => {
    const [productList, setProductList] = useState<Product[]>([])
    const [count, setCount] = useState<number>(1)
    const [isLoading, setLoading] = useState(false)
    const navigate = useNavigate()
    const location = useLocation()
    const dispatch = useAppDispatch()
    const [opentFilter, setOpentFilter] = useState(false)
    const [params, setParams] = useState<ListParams>({
        minPrice: 30,
        maxPrice: 3000,
        categoryIds: [],
        orderBy: "salePrice desc",
        pageSize: 9,
        currentPage: 0,
    })

    const handleClose = () => {
        setOpentFilter(false)
    }

    const queryParam = useMemo(() => {
        const params = qs.parse(location.search.slice(1))
        return {
            iconId: params.iconId,
            categoryIds: params.categoryIds,
            minPrice: Number(params["minPrice"]) || 30,
            maxPrice: Number(params.maxPrice) || 3000,
            orderBy: params.orderBy,
            page: params.page as string,
        }
    }, [location.search])

    useEffect(() => {
        window.scrollTo(0, 0)
        setLoading(true)
        const param = {
            categoryIds: (queryParam.categoryIds as string[])?.map(Number),
            iconId: (queryParam.iconId as string[])?.map(Number),
            minPrice: queryParam.minPrice as number,
            maxPrice: queryParam.maxPrice as number,
            orderBy: queryParam.orderBy as string,
            top: 9,
            skip: ((+queryParam.page || 1) - 1) * 9,
        }
        setParams({
            iconId: params.iconId,
            categoryIds: param.categoryIds,
            minPrice: param.minPrice || 30,
            maxPrice: param.maxPrice || 3000,
            orderBy: param.orderBy || "salePrice desc",
            top: 9,
            page: +queryParam.page || 1,
        })
        const debouncedEffect = debounce(() => {
            search(param)
        }, 200)
        debouncedEffect()
        return () => debouncedEffect.cancel()
    }, [queryParam, dispatch])

    const search = async (value: ListParams) => {
        try {
            const queryString = qs.stringify(value)
            const res = await productApi.getList(queryString)
            const pagination = JSON.parse(res.headers["x-pagination"]) as PaginationMetadata
            setProductList(res.data)
            setCount(pagination.totalItems)
            setLoading(false)
        } catch (error) {
            setLoading(false)
            handleNotify.error(error as string)
        }
    }
    const handleFilterChange = (value: ListParams) => {
        const newParam = {
            ...params,
            ...value,
        }

        navigate({
            pathname: location.pathname,
            search: qs.stringify(newParam),
        })
    }
    const handleSortChange = (value: "salePrice desc" | "salePrice asc") => {
        const newParam = {
            ...params,
            orderBy: value,
        }
        setParams(newParam)
        navigate({
            pathname: location.pathname,
            search: qs.stringify(newParam),
        })
    }
    function handlePageChange(event: ChangeEvent<unknown>, page: number): void {
        const newParam = {
            ...params,
            page,
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
                                    param={params}
                                    queryParam={queryParam}
                                />
                            </Box>
                        </Drawer>
                        <Hidden mdDown>
                            <Box width="360px">
                                <MenuFilterOption
                                    handleFilterChange={handleFilterChange}
                                    param={params}
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
                            <Stack minHeight="100vh">
                                <ListPage isLoading={isLoading} productList={productList} />
                                <Pagination
                                    sx={{
                                        mt: "auto",
                                        mx: "auto",
                                    }}
                                    count={count}
                                    variant="outlined"
                                    color="primary"
                                    onChange={handlePageChange}
                                    page={params.currentPage || 0}
                                />
                            </Stack>
                        </Box>
                    </Stack>
                </Box>
            </Container>
        </MainLayout>
    )
}

export default ProductShopPage
