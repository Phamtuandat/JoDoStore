import DeleteOutlinedIcon from "@mui/icons-material/DeleteOutlined"
import { Box, Divider, Hidden, IconButton, Paper, Typography } from "@mui/material"
import FormControl from "@mui/material/FormControl"
import FormControlLabel from "@mui/material/FormControlLabel"
import Radio from "@mui/material/Radio"
import RadioGroup from "@mui/material/RadioGroup"
import { Container } from "@mui/system"
import { useAppDispatch, useAppSelector } from "app/hooks"
import { MainLayout } from "components/Layout/MainLayout"
import { cartTotalSelector } from "features/cart/cartSelector"
import { cartAction, cartSelector } from "features/cart/cartSlice"
import QuantityForm from "features/cart/components/QuantityForm"
import { Product } from "models"
type Props = {}

const OrderPage = (props: Props) => {
    const carts = useAppSelector(cartSelector)
    const Subtotal = useAppSelector(cartTotalSelector)
    const dispatch = useAppDispatch()
    const handleQuantityChange = (product: Product, quantity: number) => {
        dispatch(
            cartAction.setQuantity({
                product,
                quantity,
            })
        )
    }
    const handleRemoveCartItem = (id: number) => {
        dispatch(cartAction.removeFromCart(id))
    }
    return (
        <MainLayout>
            <Box mt={5} mx={1}>
                <Paper
                    variant="outlined"
                    sx={{
                        py: 3,
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
                            <Typography fontWeight={700} variant="h5">
                                Subtotal
                            </Typography>
                        </Box>
                        <Divider />
                        <Box>
                            {carts.map((cart) => (
                                <Box key={cart.product.id} display="flex">
                                    <Divider />
                                    <Box
                                        py={1}
                                        display="flex"
                                        justifyContent="space-between"
                                        position="relative"
                                        flexDirection={{
                                            xs: "column",
                                            md: "row",
                                        }}
                                        flexGrow={1}
                                    >
                                        <Box
                                            display="flex"
                                            flexDirection="column"
                                            justifyContent="space-between"
                                        >
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
                                        >
                                            <Hidden mdDown>
                                                <Typography component="span" textAlign="center">
                                                    ${cart.product.salePrice}
                                                </Typography>
                                            </Hidden>
                                            <Box maxWidth={90} border={"1px solid"}>
                                                <QuantityForm
                                                    handleQuantityChange={(quantity: number) =>
                                                        handleQuantityChange(cart.product, quantity)
                                                    }
                                                    quantity={cart.quantity}
                                                />
                                            </Box>
                                            <Typography component="span" color="red" fontSize={20}>
                                                ${cart.quantity * (cart.product.salePrice || 0)}
                                            </Typography>
                                        </Box>
                                    </Box>
                                    <Box
                                        sx={{
                                            alignSelf: "center",
                                        }}
                                    >
                                        <IconButton
                                            onClick={() => {
                                                if (cart.product.id) {
                                                    handleRemoveCartItem(+cart.product.id)
                                                }
                                            }}
                                        >
                                            <DeleteOutlinedIcon />
                                        </IconButton>
                                    </Box>
                                </Box>
                            ))}
                        </Box>
                        <Divider />
                        <Box py={2} display="flex" justifyContent="space-between">
                            <Typography variant="h6" fontWeight={700}>
                                Subtotal
                            </Typography>
                            <Typography fontWeight={700}>${Subtotal}</Typography>
                        </Box>
                        <Divider />
                        <Box mt={2} display="flex" justifyContent="space-between">
                            <Typography>Shipping Method</Typography>
                            <Typography>$3</Typography>
                        </Box>
                        <Box my={2}>
                            <FormControl margin="none" size="small">
                                <RadioGroup
                                    aria-labelledby="demo-radio-buttons-group-label"
                                    defaultValue="freeShipping"
                                    name="radio-buttons-group"
                                >
                                    <FormControlLabel
                                        value="freeShipping"
                                        control={<Radio />}
                                        label="Free Shipping"
                                    />
                                    <FormControlLabel
                                        value="local"
                                        control={<Radio />}
                                        label="Local"
                                    />
                                    <FormControlLabel
                                        value="flatRate"
                                        control={<Radio />}
                                        label="flatRate"
                                    />
                                </RadioGroup>
                            </FormControl>
                        </Box>
                        <Divider />
                        <Box mt={2} display="flex" justifyContent="space-between">
                            <Typography fontWeight={700} variant="h4">
                                Total
                            </Typography>
                            <Typography fontWeight={700} variant="h6">
                                ${Subtotal + 35}
                            </Typography>
                        </Box>
                    </Container>
                </Paper>
            </Box>
        </MainLayout>
    )
}

export default OrderPage
