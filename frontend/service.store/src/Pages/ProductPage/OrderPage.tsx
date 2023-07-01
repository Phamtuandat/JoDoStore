import DeleteOutlinedIcon from "@mui/icons-material/DeleteOutlined"
import {
    Box,
    Button,
    CardMedia,
    Divider,
    Grid,
    Hidden,
    IconButton,
    Paper,
    Typography,
} from "@mui/material"
import { Container } from "@mui/system"
import { addressApi } from "ApiClients/AddressApi"
import { orderApi } from "ApiClients/OrderApi"
import { useAppDispatch, useAppSelector } from "app/hooks"
import { MainLayout } from "components/Layout/MainLayout"
import { cartTotalSelector } from "features/cart/cartSelector"
import { cartProcessing, cartSelector, cartSliceAction } from "features/cart/cartSlice"
import QuantityForm from "features/cart/components/QuantityForm"
import { Address, Product } from "models"
import { useEffect, useRef, useState } from "react"
import { Link } from "react-router-dom"
import { toast } from "react-toastify"
import handleNotify from "utils/Toast-notify"
import OrderInfo from "./components/OrderInfo"
type Props = {}

const Msg = () => (
    <div>
        <Typography color="text.primary"> Order Successfully !</Typography>
        <Link to="/">Go To HomePage</Link>
    </div>
)
const OrderPage = (props: Props) => {
    const ignore = useRef(false)
    const [addressList, setList] = useState<Address[]>([])
    const carts = useAppSelector(cartSelector)
    const Subtotal = useAppSelector(cartTotalSelector)
    const dispatch = useAppDispatch()
    const [loading, setLoading] = useState(false)
    const processing = useAppSelector(cartProcessing)
    const [address, setAddress] = useState<Address | undefined>()
    function handleAddresChange(value: number) {
        const add = addressList.find((x) => x.id === value)
        if (add) {
            setAddress(add)
        }
    }
    useEffect(() => {
        if (!ignore.current) {
            ignore.current = true
            ;(async () => {
                var result = (await addressApi.getAll()).data
                setList(result)
            })()
        }
    }, [])

    const handleQuantityChange = (product: Product, quantity: number) => {
        dispatch(
            cartSliceAction.addToCart({
                product,
                quantity,
            })
        )
        toast.dismiss()
        console.log(product, quantity)
    }
    const handleRemoveCartItem = (id: number) => {
        dispatch(cartSliceAction.removeFromCart(id))
    }
    const handleCreateOrder = async () => {
        try {
            setLoading(true)
            const orders = carts.map((item) => ({
                productId: +item.product.id,
                quantity: item.quantity,
            }))
            if (address) {
                await orderApi.create({
                    addressId: +address.id,
                    orderItems: orders,
                    shippingCash: 0,
                })
                setLoading(false)
                dispatch(cartSliceAction.removeAllCartItem())
                toast(<Msg />)
            }
        } catch (error) {
            handleNotify.error(error as string)
            setLoading(false)
        }
    }
    return (
        <MainLayout>
            <Container maxWidth="lg">
                {carts.length > 0 ? (
                    <Box mt={10} mx={{ md: 1, xs: 0 }} py={3}>
                        <Grid container spacing={2}>
                            <Grid item md={8} xs={12}>
                                <Paper
                                    variant="outlined"
                                    sx={{
                                        borderRadius: "3px",
                                        overflow: "hidden",
                                    }}
                                >
                                    <Container>
                                        <Typography fontWeight={700} variant="h4" py={4}>
                                            Your Order
                                        </Typography>
                                        <Box display="flex" justifyContent="space-between">
                                            <Typography fontWeight={700} variant="h5">
                                                Product
                                            </Typography>
                                        </Box>
                                        <Divider />
                                        <Box>
                                            {carts.map((cart, i) => (
                                                <Box
                                                    key={+cart.product.id + i}
                                                    display="flex"
                                                    py={3}
                                                >
                                                    <Hidden smDown>
                                                        <Box>
                                                            <CardMedia
                                                                component="img"
                                                                height="80px"
                                                                
                                                            />
                                                        </Box>
                                                    </Hidden>
                                                    <Box
                                                        display="flex"
                                                        justifyContent="space-between"
                                                        position="relative"
                                                        flexDirection={{
                                                            xs: "column",
                                                            md: "row",
                                                        }}
                                                        flexGrow={1}
                                                    >
                                                        <Box display="flex">
                                                            <Hidden smUp>
                                                                <Box flexGrow={0}>
                                                                    <CardMedia
                                                                        component="img"
                                                                        height="80px"
                                                                        width="80px"
                                                                    />
                                                                </Box>
                                                            </Hidden>
                                                            <Box ml={1}>
                                                                <Typography
                                                                    fontWeight={700}
                                                                    sx={{
                                                                        display: "-webkit-box",
                                                                        textOverflow: "ellipsis",
                                                                        WebkitLineClamp: 2,
                                                                        maxWidth: "250px",
                                                                        overflow: "hidden",
                                                                    }}
                                                                >
                                                                    {cart.product.name}
                                                                </Typography>
                                                                <Hidden smUp>
                                                                    <Typography
                                                                        component="span"
                                                                        textAlign="center"
                                                                    >
                                                                        ${cart.product.salePrice}
                                                                    </Typography>
                                                                </Hidden>
                                                            </Box>
                                                        </Box>
                                                        <Box
                                                            display="flex"
                                                            flexDirection={{
                                                                xs: "column-reverse",
                                                                md: "row",
                                                            }}
                                                            justifyContent="space-around"
                                                            flexGrow={1}
                                                            alignItems={{
                                                                xs: "start",
                                                                md: "center",
                                                            }}
                                                            mt={1}
                                                            ml={{ xs: 0, md: 4 }}
                                                        >
                                                            <Hidden mdDown>
                                                                <Typography
                                                                    component="span"
                                                                    textAlign="center"
                                                                >
                                                                    ${cart.product.salePrice}
                                                                </Typography>
                                                            </Hidden>
                                                            <Box
                                                                display="flex"
                                                                justifyContent={{
                                                                    xs: "space-between",
                                                                    md: "space-around",
                                                                }}
                                                                width="100%"
                                                            >
                                                                <Box
                                                                    maxWidth={90}
                                                                    border={"1px solid"}
                                                                    display="flex"
                                                                    height={28}
                                                                    alignSelf="center"
                                                                    ml={{ sx: "0", md: "auto" }}
                                                                >
                                                                    <QuantityForm
                                                                        disabled={processing}
                                                                        handleQuantityChange={(
                                                                            quantity: number
                                                                        ) =>
                                                                            handleQuantityChange(
                                                                                cart.product,
                                                                                quantity
                                                                            )
                                                                        }
                                                                        quantity={cart.quantity}
                                                                    />
                                                                </Box>
                                                                <Box
                                                                    sx={{
                                                                        alignSelf: "center",
                                                                        ml: "auto",
                                                                    }}
                                                                >
                                                                    <IconButton
                                                                        onClick={() => {
                                                                            if (cart.product.id) {
                                                                                handleRemoveCartItem(
                                                                                    +cart.product.id
                                                                                )
                                                                            }
                                                                        }}
                                                                    >
                                                                        <DeleteOutlinedIcon />
                                                                    </IconButton>
                                                                </Box>
                                                            </Box>
                                                        </Box>
                                                    </Box>

                                                    <Divider />
                                                </Box>
                                            ))}
                                        </Box>
                                    </Container>
                                </Paper>
                            </Grid>
                            <Grid item md={4} xs={12}>
                                <OrderInfo
                                    handleAddresChange={handleAddresChange}
                                    Subtotal={Subtotal}
                                    addressList={addressList}
                                    carts={carts}
                                    handleCreateOrder={handleCreateOrder}
                                    loading={loading}
                                    address={address ? address : addressList[0]}
                                />
                            </Grid>
                        </Grid>
                    </Box>
                ) : (
                    <Box width="100%" mt={{ md: 16, xs: 10 }}>
                        <Box
                            sx={{
                                height: "60vh",
                                background:
                                    "url(https://res.cloudinary.com/dmzvhnnkh/image/upload/v1678194804/lXFfa6dKHkq6XSJlGrkHMQ%3D%3D_cartEmpty.png)",
                                backgroundPositionX: "center",
                                backgroundRepeat: "no-repeat",
                                backgroundSize: { xs: "contain", md: "auto" },
                                display: "flex",
                                justifyContent: "center",
                            }}
                        >
                            <Box mt="auto">
                                <Box
                                    component={Link}
                                    to="/shop"
                                    sx={{
                                        textDecoration: "none",
                                    }}
                                >
                                    <Button variant="outlined">Shop Now!</Button>
                                </Box>
                            </Box>
                        </Box>
                    </Box>
                )}
            </Container>
        </MainLayout>
    )
}

export default OrderPage
