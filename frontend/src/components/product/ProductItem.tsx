import FavoriteIcon from "@mui/icons-material/Favorite"
import RemoveRedEyeOutlinedIcon from "@mui/icons-material/RemoveRedEyeOutlined"
import { Box, CardContent, Paper, Typography } from "@mui/material"
import Button from "@mui/material/Button/Button"
import { styled, useTheme } from "@mui/material/styles"
import { cartAction } from "features/cart/cartSlice"
import { motion } from "framer-motion"
import { useWidth } from "Hooks/width-hook"
import { Product } from "models"

import { useEffect, useState } from "react"
import { useDispatch } from "react-redux"
import { useNavigate } from "react-router-dom"
type Props = {
    product: Product
}
const StyledButton = styled(Box)`
    ${({ theme }) => `
            border-radius: 50%;
            line-height: 0;
            align-self: center;
            box-shadow: rgba(0, 0, 0, 0.25) 0px 54px 55px, rgba(0, 0, 0, 0.12) 0px -12px 30px, rgba(0, 0, 0, 0.12) 0px 4px 6px, rgba(0, 0, 0, 0.17) 0px 12px 13px, rgba(0, 0, 0, 0.09) 0px -3px 5px;
            padding: 4px;
            font-size: 10px;
            cursor: pointer;
            background-color: ${theme.palette.primary.dark};
            transition: ${theme.transitions.create(["transform"], {
                duration: theme.transitions.duration.short,
            })};
            &:hover {
                transform: scale(1.2);
            }
            
        `}
`

const variants = {
    open: { opacity: 1, y: -80 },
    closed: {
        opacity: 0,
        y: 0,
    },
}
export const ProductItem = ({ product }: Props) => {
    const theme = useTheme()
    const [imgIndex, setIndex] = useState<number>(0)
    const [checked, setCheck] = useState(false)
    const width = useWidth()
    const dispatch = useDispatch()
    const navigate = useNavigate()

    const handleClick = (id: string | number | null) => {
        if (id) navigate(`/product/${id}`)
    }

    useEffect(() => {
        if (checked === true) {
            setIndex((prev) => {
                if (prev < product.thumbnails.length - 1) {
                    return prev + 1
                } else {
                    return 0
                }
            })
        } else {
            setIndex(0)
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [checked])

    return (
        <Paper
            elevation={0}
            sx={{
                color: "text.default",
                width: "auto",
                cursor: "pointer",
            }}
            onMouseEnter={() => setCheck((prv) => !prv)}
            onMouseLeave={() => setCheck((prv) => !prv)}
        >
            <Box height="250px" overflow="hidden" onClick={() => handleClick(product.id)}>
                <Box
                    component={motion.div}
                    sx={{
                        p: "125px",
                        backgroundImage: ` url(${product.thumbnails[imgIndex]?.imageUrl})`,
                        backgroundPosition: "center",
                        backgroundSize: "cover",
                        backgroundRepeat: "no-repeat",
                        bgcolor: theme.palette.background.paper,
                    }}
                    animate={checked ? { scale: 1.1 } : { scale: 1 }}
                    transition={{ duration: 0.9 }}
                />
                <Box
                    sx={{
                        display: "flex",
                        justifyContent: "center",
                    }}
                    component={motion.div}
                    initial={{ opacity: 0 }}
                    animate={checked && width !== "xs" ? "open" : "closed"}
                    variants={variants}
                    transition={{ duration: 0.3 }}
                >
                    <StyledButton
                        sx={{
                            color: "text.primary",
                        }}
                    >
                        <FavoriteIcon color="action" />
                    </StyledButton>
                    <Button
                        variant="contained"
                        size="medium"
                        sx={{
                            mx: 1,
                            transition: ` transform ${theme.transitions.duration.complex}ms `,
                            "&:hover": {
                                transform: "scale(1.05)",
                            },
                        }}
                        onClick={() =>
                            dispatch(
                                cartAction.addToCart({
                                    product,
                                    quantity: 1,
                                })
                            )
                        }
                    >
                        Add to cart
                    </Button>
                    <StyledButton aria-label="watch more">
                        <RemoveRedEyeOutlinedIcon color="action" />
                    </StyledButton>
                </Box>
            </Box>
            <CardContent>
                <Box display="flex" flexDirection="column" pt={2}>
                    <Typography
                        sx={{
                            overflow: "hidden",
                            textOverflow: "ellipsis",
                            WebkitLineClamp: 1,
                            width: "100%",
                            display: "inline-block",
                            whiteSpace: "nowrap",
                        }}
                        variant="h6"
                    >
                        {product.name}
                    </Typography>
                    <Typography
                        textAlign="center"
                        variant="body1"
                        color={theme.palette.text.secondary}
                    >
                        {product.brand?.name}
                    </Typography>
                </Box>
                <Box textAlign="center">
                    <Typography
                        fontSize="22px"
                        sx={{
                            fontWeight: "900",
                            opacity: 0.4,
                            textDecoration: "line-through",
                            mr: 2,
                        }}
                        component="span"
                    >
                        ${product.price}
                    </Typography>
                    <Typography fontSize="22px" sx={{ fontWeight: "900" }} component="span">
                        ${product.salePrice}
                    </Typography>
                </Box>
            </CardContent>
        </Paper>
    )
}
