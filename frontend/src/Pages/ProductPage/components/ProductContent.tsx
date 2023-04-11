import CheckIcon from "@mui/icons-material/Check"
import { Divider } from "@mui/material"
import Box from "@mui/material/Box"
import Button from "@mui/material/Button"
import Rating from "@mui/material/Rating"
import Typography from "@mui/material/Typography"
import { styled, useTheme } from "@mui/material/styles"
import { Stack } from "@mui/system"
import { useWidth } from "Hooks/width-hook"
import { useAppDispatch, useAppSelector } from "app/hooks"
import QuantityField from "components/inputField/QuantityField"
import { selectLogin } from "features/authenticate/authSlice"
import { cartSliceAction } from "features/cart/cartSlice"
import { Product } from "models"
import { useState } from "react"
import { useForm } from "react-hook-form"
import { useNavigate } from "react-router-dom"
import ProductDesc from "./ProductDesc"
import handleNotify from "utils/Toast-notify"
type Props = {
    product: Product
}
const CustomButton = styled(Button)(({ theme }) => ({
    height: "55px",
    transition: `transform 0.5s`,
    "&:hover": {
        transform: "scale(1.01)",
    },
}))

export interface IFormState {
    quantity: number
}

const ProductContent = ({ product }: Props) => {
    const { handleSubmit, setValue, control } = useForm<IFormState>({
        defaultValues: {
            quantity: 0,
        },
    })

    const theme = useTheme()
    const [value, setStart] = useState<number | null>(4.5)
    const dispatch = useAppDispatch()
    const isLogged = useAppSelector(selectLogin)
    const navigate = useNavigate()
    const width = useWidth()
    const [quantity, setQuantity] = useState(0)
    const onSubmit = (value: IFormState) => {
        if (!isLogged) {
            handleNotify.warn("Please login to add products to cart!")
            return navigate("/auth")
        }
        if (value.quantity > 0) {
            dispatch(
                cartSliceAction.addToCart({
                    product: product,
                    quantity: value.quantity,
                })
            )
        }
    }

    return (
        <Box mt={3}>
            <Box>
                <Typography variant="h4">{product.name}</Typography>
            </Box>
            <Box
                my={4}
                width="100%"
                sx={{
                    flexDirection: { xs: "column", sm: "column", md: "row" },
                }}
                display="flex"
                justifyContent="space-between"
            >
                <Box
                    display="flex"
                    fontSize={theme.typography.h6.fontSize}
                    width={{ md: "150px", xs: "200px" }}
                    justifyContent="space-between"
                    alignItems="end"
                >
                    <Typography fontWeight={700} variant="h5" component="span">
                        ${product.salePrice}
                    </Typography>
                    <Typography variant="h5" fontWeight={700} component="span">
                        {" - "}
                    </Typography>

                    <Typography
                        variant="h5"
                        fontWeight={700}
                        sx={{
                            textDecoration: "line-through",
                            opacity: 0.4,
                        }}
                        component="span"
                    >
                        ${product.price}
                    </Typography>
                </Box>
                <Box display="flex" flexDirection={{ sm: "row", md: "column", lg: "row" }}>
                    <Rating
                        precision={0.5}
                        name="simple-controlled"
                        value={value}
                        onChange={(event, newValue) => {
                            setStart(newValue)
                        }}
                    />
                    <Typography
                        component="legend"
                        sx={{
                            opacity: 0.6,
                        }}
                    >
                        ( 2 Customer review )
                    </Typography>
                </Box>
            </Box>
            <Divider
                sx={{
                    my: { xs: 1, md: 3 },
                }}
            />
            <Stack
                sx={{
                    color: theme.palette.success.main,
                }}
            >
                <Box display="flex" mt={1} flexDirection="column">
                    {product.tags?.map((tag) => (
                        <Box display="flex" key={tag.id}>
                            <CheckIcon />
                            <Typography variant="button" component="span" mx={1}>
                                {tag.name}
                            </Typography>
                        </Box>
                    ))}
                </Box>
            </Stack>
            <Box mt={{ xs: 2, md: 5 }}>
                <Typography variant="h5" component="span">
                    Category:
                </Typography>
                <Typography
                    ml={2}
                    fontWeight={300}
                    color={theme.palette.secondary.main}
                    variant="h6"
                    component="span"
                >
                    {product.category?.name}
                </Typography>
            </Box>
            {(width !== "sm" || "xs") && (
                <Typography
                    textAlign="justify"
                    width="100%"
                    component="p"
                    color={theme.palette.grey[700]}
                    pt={4}
                >
                    Lorem ipsum dolor, sit amet consectetur adipisicing elit. Deserunt, odit
                    veritatis! Dolore excepturi aperiam harum cumque mollitia laborum officia at,
                    molestias rem saepe eveniet deleniti voluptatibus, aliquid sapiente laboriosam
                    consequuntur.
                </Typography>
            )}

            {/* <Box>
                <OptionPicker />
            </Box> */}
            <Box
                mt={{ xs: 2, md: 6 }}
                flexDirection={{ xs: "column", sm: "row", md: "row" }}
                display="flex"
                component="form"
                onSubmit={handleSubmit(onSubmit)}
                alignItems="center"
            >
                <Box alignSelf="center" mx={2} my={2} maxWidth={100} border="1px solid">
                    <QuantityField
                        handleQuantityChange={(value) => {
                            if (!(quantity < 1 && value < 0)) {
                                setValue("quantity", quantity + value)
                                setQuantity(quantity + value)
                            }
                        }}
                        control={control}
                    />
                </Box>
                <CustomButton fullWidth variant="contained" size="large" type="submit">
                    ADD TO CART!
                </CustomButton>
            </Box>
            <ProductDesc desc={product.description} />
        </Box>
    )
}

export default ProductContent
