import ClearIcon from "@mui/icons-material/Clear"
import { Box, CardMedia, Divider, ListItem, Stack, Typography } from "@mui/material"
import Button from "@mui/material/Button"
import IconButton from "@mui/material/IconButton"
import Link from "@mui/material/Link/Link"
import Paper from "@mui/material/Paper"
import { useTheme } from "@mui/material/styles"
import { useAppDispatch, useAppSelector } from "app/hooks"
import { Product } from "models"
import { NavLink } from "react-router-dom"
import { cartTotalSelector } from "../cartSelector"
import { cartAction, CartItems } from "../cartSlice"
import QuantityForm from "./QuantityForm"
type Props = {
    carts: CartItems[]
}

const CartReview = ({ carts }: Props) => {
    const theme = useTheme()
    const subTotal = useAppSelector(cartTotalSelector)
    const dispatch = useAppDispatch()

    const handleRemoveItem = (product: Product) => {
        dispatch(cartAction.removeFromCart(product.id))
    }
    return (
        <Box
            display="flex"
            flexDirection="column"
            width={{ xs: "400px", sm: "480px", md: "480px" }}
            height="100%"
            bgcolor={theme.palette.background.default}
        >
            <Paper
                sx={{
                    pb: 3,
                    pt: 8,
                    display: "flex",
                    flexDirection: "column",
                    height: "100%",
                    zIndex: 1000,
                }}
            >
                <Box my={4} px={2}>
                    <Typography variant="h5" fontWeight={700} color={theme.palette.text.primary}>
                        Cart review:
                    </Typography>
                    <Divider variant="middle" />
                </Box>
                <Box
                    maxHeight={600}
                    sx={{
                        overflowY: "scroll",
                        "&::-webkit-scrollbar": {
                            display: "none",
                        },
                    }}
                >
                    {carts.map((cart) => (
                        <Box key={cart.product.id}>
                            <ListItem>
                                <Box
                                    display="flex"
                                    width="100%"
                                    position="relative"
                                    pt={1}
                                    justifyContent="space-between"
                                >
                                    <Box
                                        sx={{
                                            zIndex: 10,
                                            position: "absolute",
                                            top: -5,
                                            left: -5,
                                            p: 0,
                                            borderRadius: "50%",
                                            bgcolor: "Background",
                                            transition: "all 0.2s ease-in",
                                            "&:hover": {
                                                bgcolor: "Highlight",
                                                "&>button": {
                                                    color: "#fff",
                                                },
                                            },
                                            boxShadow: theme.shadows[1],
                                            lineHeight: 0,
                                        }}
                                    >
                                        <IconButton
                                            size="small"
                                            color="success"
                                            onClick={() => handleRemoveItem(cart.product)}
                                            sx={{
                                                fontSize: "12px",
                                                p: 0.1,
                                                color: "primary",
                                                lineHeight: 0,
                                            }}
                                        >
                                            <ClearIcon fontSize="small" />
                                        </IconButton>
                                    </Box>
                                    <CardMedia
                                        image={cart.product.thumbnails[0].imageUrl}
                                        sx={{
                                            width: "80px",
                                            height: "80px",
                                            borderRadius: "8px",
                                            mr: 1,
                                        }}
                                    />
                                    <Box
                                        display="flex"
                                        justifyContent="space-between"
                                        flexDirection="column"
                                        sx={{
                                            width: { xs: "64px", sm: "240px" },
                                        }}
                                        height="100%"
                                    >
                                        <Typography
                                            fontWeight={700}
                                            variant="inherit"
                                            sx={{
                                                overflow: "hidden",
                                                textOverflow: "ellipsis",
                                                WebkitLineClamp: 1,
                                                display: "inline-block",
                                                whiteSpace: "nowrap",
                                            }}
                                        >
                                            {cart.product.name}
                                        </Typography>
                                        <Typography my={1} component="span">
                                            ${cart.product.salePrice}
                                        </Typography>
                                    </Box>
                                    <Box
                                        display="flex"
                                        flexDirection="column"
                                        justifyContent="space-between"
                                        width="100px"
                                    >
                                        <Typography
                                            color="secondary"
                                            variant="h6"
                                            fontWeight={700}
                                            component="span"
                                            textAlign="center"
                                        >
                                            ${(cart.product.salePrice || 0) * cart.quantity}
                                        </Typography>
                                        <Box color={theme.palette.secondary.main}>
                                            <QuantityForm
                                                quantity={cart.quantity}
                                                handleQuantityChange={(quantity) =>
                                                    dispatch(
                                                        cartAction.setQuantity({
                                                            product: cart.product,
                                                            quantity,
                                                        })
                                                    )
                                                }
                                            />
                                        </Box>
                                    </Box>
                                </Box>
                            </ListItem>
                            <Divider variant="middle" />
                        </Box>
                    ))}
                </Box>
                <Box px={2} mt="auto">
                    <Divider variant="middle" />
                    <Box display="flex" justifyContent="space-between" mt={2}>
                        <Typography variant="h6">Subtotal:</Typography>
                        <Typography color="secondary.dark" variant="h6">
                            ${subTotal}
                        </Typography>
                    </Box>
                    <Stack
                        mt={4}
                        display="flex"
                        direction={{ xs: "column", sm: "row" }}
                        spacing={3}
                    >
                        <Button
                            size="large"
                            fullWidth
                            variant="contained"
                            color="secondary"
                            sx={{ fontWeight: 700 }}
                        >
                            vewiew cart
                        </Button>
                        <Button
                            size="large"
                            fullWidth
                            variant="contained"
                            color="primary"
                            sx={{ fontWeight: 700 }}
                        >
                            <Link
                                component={NavLink}
                                to="/orders"
                                sx={{
                                    textDecoration: "none",
                                    color: theme.palette.primary.contrastText,
                                }}
                                onClick={() => dispatch(cartAction.hideMiniCart())}
                            >
                                Checkout!
                            </Link>
                        </Button>
                    </Stack>
                </Box>
            </Paper>
        </Box>
    )
}

export default CartReview
