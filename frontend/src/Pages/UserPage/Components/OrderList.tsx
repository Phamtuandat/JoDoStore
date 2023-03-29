import { Box, CardMedia, CircularProgress, Divider, Paper, Stack, Typography } from "@mui/material"
import { orderApi } from "ApiClients/OrderApi"
import { productApi } from "ApiClients/ProductApi"
import { Order, Product } from "models"
import buildQuery from "odata-query"
import { useEffect, useRef, useState } from "react"
import { useTheme } from "@mui/material/styles"
type Props = {}

const OrderList = (props: Props) => {
    const ignore = useRef(false)
    const [orders, setOrders] = useState<Order[]>([])
    const [products, setProducts] = useState<Product[]>([])
    const [isLoading, setLoading] = useState(false)
    const theme = useTheme()

    useEffect(() => {
        setLoading(true)
        if (!ignore.current) {
            ignore.current = true
            try {
                ;(async () => {
                    const respone = await orderApi.getUserOrders()
                    const orderList = respone.data
                    setOrders(orderList)
                    const listProductId = orderList.reduce<(number | undefined)[]>(
                        (IdList: (number | undefined)[], o: Order) =>
                            IdList.concat(o.orderItems.map((x) => x.productId)),
                        []
                    )
                    const uniq = listProductId.filter(
                        (item, index) => listProductId.indexOf(item) === index
                    )
                    const params = buildQuery({
                        filter: {
                            id: { in: uniq },
                        },
                    })
                    const res = await productApi.getList(params)
                    setProducts(res.data)
                })()
            } catch (error) {}
        }
        setLoading(false)

        return () => setLoading(false)
    }, [])
    const convertTime = (utcTime: string | undefined) => {
        if (utcTime) {
            const localTime = new Date(utcTime)
            const localTimeString = localTime.toLocaleString("en-US", { timeZone: "UTC" })
            return localTimeString
        }
    }
    return (
        <>
            {!isLoading ? (
                <Stack spacing={2}>
                    {orders.map((x) => (
                        <Paper key={x.id} elevation={0}>
                            <Box
                                height={40}
                                justifyContent="space-between"
                                display="flex"
                                alignItems="center"
                                mx={1}
                                my={1}
                            >
                                <Box display="flex" flexDirection="column" py={1}>
                                    <Typography
                                        fontSize={14}
                                        sx={{
                                            overflow: "hidden",
                                            textOverflow: "ellipsis",
                                            WebkitLineClamp: 1,
                                            maxWidth: { xs: 150, sm: "fit-content" },
                                            display: "inline-block",
                                            whiteSpace: "nowrap",
                                        }}
                                    >
                                        {x.addressBook?.name}, {x.addressBook?.address},{" "}
                                        {x.addressBook?.ward} {x.addressBook?.district},{" "}
                                        {x.addressBook?.province}
                                    </Typography>
                                    <Typography color="#4caf50" fontSize={12}>
                                        {convertTime(x.orderDate)}
                                    </Typography>
                                </Box>
                                <Box
                                    component="span"
                                    sx={{
                                        px: 1,
                                        borderRadius: 3,
                                        boxShadow: theme.shadows[1],
                                    }}
                                >
                                    Shipped
                                </Box>
                            </Box>
                            <Divider />
                            <Stack sx={{ p: 0.2 }}>
                                {x.orderItems.map((x) => {
                                    const product = products.find((p) => +p.id === x.productId)
                                    return (
                                        <Box key={x.id}>
                                            <Box display="flex">
                                                <CardMedia
                                                    component="img"
                                                    image={product?.thumbnails[0].imageUrl}
                                                    sx={{
                                                        height: 80,
                                                        width: 80,
                                                    }}
                                                />
                                                <Box
                                                    ml={1}
                                                    display="flex"
                                                    justifyContent="space-between"
                                                    flexGrow={1}
                                                    mr={{ md: 10, xs: 1 }}
                                                    flexDirection={{ xs: "column", sm: "row" }}
                                                >
                                                    <Typography>{product?.name}</Typography>
                                                    <Typography component="span">
                                                        Quantity: {x.quantity}
                                                    </Typography>
                                                    <Typography>$ {product?.salePrice}</Typography>
                                                </Box>
                                            </Box>
                                        </Box>
                                    )
                                })}
                            </Stack>
                        </Paper>
                    ))}
                </Stack>
            ) : (
                <Box display="flex" justifyContent="center" alignItems="center" height="50vh">
                    <CircularProgress />
                </Box>
            )}
        </>
    )
}

export default OrderList
