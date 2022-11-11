import FavoriteIcon from "@mui/icons-material/Favorite"
import RemoveRedEyeOutlinedIcon from "@mui/icons-material/RemoveRedEyeOutlined"
import { Box, CardContent, Paper, Typography } from "@mui/material"
import Button from "@mui/material/Button/Button"
import { styled, useTheme } from "@mui/material/styles"
import { motion } from "framer-motion"
import { Product } from "models"
import { useState } from "react"
type Props = {
    product: Product
}

export const ProductItem = ({ product }: Props) => {
    const [checked, setCheck] = useState(false)

    const variants = {
        open: { opacity: 1, y: -100 },
        closed: {
            opacity: 0,
            y: 0,
        },
    }

    const theme = useTheme()
    const StyledButton = styled(Box)`
        ${({ theme }) => `
            border-radius: 50%;
            line-height: 0;
            align-self: center;
            box-shadow: rgba(0, 0, 0, 0.25) 0px 54px 55px, rgba(0, 0, 0, 0.12) 0px -12px 30px, rgba(0, 0, 0, 0.12) 0px 4px 6px, rgba(0, 0, 0, 0.17) 0px 12px 13px, rgba(0, 0, 0, 0.09) 0px -3px 5px;
            padding: 4px;
            font-size: 10px;
            cursor: pointer;
            background-color: ${theme.palette.secondary.main};
            transition: ${theme.transitions.create(["transform"], {
                duration: theme.transitions.duration.short,
            })};
            &:hover {
                transform: scale(1.2);
            }
        `}
    `

    return (
        <Paper
            elevation={2}
            sx={{
                color: "text.default",
                width: "auto",
            }}
            onMouseEnter={() => setCheck((prv) => !prv)}
            onMouseLeave={() => setCheck((prv) => !prv)}
        >
            <Box height="250px" overflow="hidden">
                <Box
                    component={motion.div}
                    sx={{
                        p: "125px",
                        backgroundImage:
                            "url( https://media.istockphoto.com/photos/old-victorian-book-cover-in-gold-and-black-leather-the-chefsdoeuvre-picture-id1367848168?b=1&k=20&m=1367848168&s=170667a&w=0&h=JFcW7yy7rqiYxDtE3gLfnskg7G_9NgSOHHKN1zzdtSQ=)",
                        backgroundPosition: "center",
                        backgroundSize: "contain",
                        backgroundRepeat: "no-repeat",
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
                    animate={checked ? "open" : "closed"}
                    variants={variants}
                    transition={{ duration: 0.3 }}
                >
                    <StyledButton
                        sx={{
                            color: "text.primary",
                        }}
                    >
                        <FavoriteIcon />
                    </StyledButton>
                    <Button
                        color="secondary"
                        variant="contained"
                        size="medium"
                        sx={{
                            mx: 1,
                            transition: ` transform ${theme.transitions.duration.complex}ms `,
                            "&:hover": {
                                transform: "scale(1.05)",
                            },
                        }}
                    >
                        Add to cart
                    </Button>
                    <StyledButton aria-label="watch more">
                        <RemoveRedEyeOutlinedIcon />
                    </StyledButton>
                </Box>
            </Box>
            <CardContent
                sx={{
                    backgroundColor: "background.default",
                }}
            >
                <Box display="flex">
                    <Typography variant="h6">
                        {product.name} {"-"}
                    </Typography>
                    <Box>
                        <Typography variant="h6">{"brands"}</Typography>
                    </Box>
                </Box>
                <Box>
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
                        ${product.priceSale}
                    </Typography>
                </Box>
            </CardContent>
        </Paper>
    )
}
