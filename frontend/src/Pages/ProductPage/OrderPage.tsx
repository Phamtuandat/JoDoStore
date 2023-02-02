import { Box, Divider, Paper, Typography } from "@mui/material"
import FormControl from "@mui/material/FormControl"
import FormControlLabel from "@mui/material/FormControlLabel"
import Radio from "@mui/material/Radio"
import RadioGroup from "@mui/material/RadioGroup"
import { useTheme } from "@mui/material/styles"
import { Container } from "@mui/system"
import { useAppSelector } from "app/hooks"
import { MainLayout } from "components/Layout/MainLayout"
import { cartTotalSelector } from "features/cart/cartSelector"
import { cartSelector } from "features/cart/cartSlice"
type Props = {}

const OrderPage = (props: Props) => {
    const carts = useAppSelector(cartSelector)
    const Subtotal = useAppSelector(cartTotalSelector)
    const theme = useTheme()
    return (
        <MainLayout>
            <Container>
                <Box mt={3}>
                    <Paper
                        sx={{
                            p: 2,
                            borderRadius: "3px",
                            overflow: "hidden",
                        }}
                    >
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
                        <Box>
                            {carts.map((cart) => (
                                <Box key={cart.product.id}>
                                    <Divider
                                        sx={{
                                            my: 1,
                                        }}
                                    />
                                    <Box py={1} display="flex" justifyContent="space-between">
                                        <Box display="flex" flexDirection="column">
                                            <Typography fontWeight={700}>
                                                {cart.product.name}
                                            </Typography>
                                            <Typography component="span">
                                                x{cart.quantity}
                                            </Typography>
                                        </Box>
                                        <Box
                                            component="span"
                                            fontWeight={700}
                                            color={theme.palette.secondary.dark}
                                            width={{ xs: "60px" }}
                                            textAlign="center"
                                        >
                                            ${cart.quantity * (cart.product.salePrice || 0)}
                                        </Box>
                                    </Box>
                                </Box>
                            ))}
                        </Box>
                        <Divider />
                        <Box py={2} display="flex" justifyContent="space-between">
                            <Typography variant="h6" fontWeight={700}>
                                Subtotal
                            </Typography>
                            <Typography fontWeight={700} color={theme.palette.secondary.dark}>
                                ${Subtotal}
                            </Typography>
                        </Box>
                        <Divider />
                        <Box mt={2} display="flex" justifyContent="space-between">
                            <Typography>Shipping Method</Typography>
                            <Typography>$35</Typography>
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
                            <Typography
                                fontWeight={700}
                                variant="h6"
                                color={theme.palette.secondary.dark}
                            >
                                ${Subtotal + 35}
                            </Typography>
                        </Box>
                    </Paper>
                </Box>
            </Container>
        </MainLayout>
    )
}

export default OrderPage
