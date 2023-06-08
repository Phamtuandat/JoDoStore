import { Box, CardContent, Paper, Typography } from "@mui/material"
import { useTheme } from "@mui/material/styles"
import { motion } from "framer-motion"
import { Product } from "models"

import { useState } from "react"
import { useNavigate } from "react-router-dom"
type Props = {
    product: Product
}
export const ProductItem = ({ product }: Props) => {
    const theme = useTheme()
    const [checked, setCheck] = useState(false)
    const navigate = useNavigate()

    const handleClick = (id: string | number | null) => {
        if (id) navigate(`/product/${id}`)
    }

    return (
        <Paper
            square
            sx={{
                color: "text.default",
                width: "auto",
                cursor: "pointer",
                transition: "box-shadow .3s",
                "&:hover": {
                    boxShadow: theme.shadows[2],
                },
            }}
            onMouseEnter={() => setCheck((prv) => !prv)}
            onMouseLeave={() => setCheck((prv) => !prv)}
        >
            <Box
                height={{ xs: 150, sm: 240, md: 260, lg: 350 }}
                overflow="hidden"
                onClick={() => handleClick(product.id)}
                display="flex"
                flexDirection="column"
                justifyContent="center"
            >
                <Box
                    component={motion.div}
                    sx={{
                        pb: "100%",
                        backgroundPosition: "center",
                        backgroundImage: `url(${product.thumbnail})`,
                        backgroundSize: "cover",
                        backgroundRepeat: "no-repeat",
                        bgcolor: theme.palette.background.paper,
                    }}
                    animate={checked ? { scale: 1.1 } : { scale: 1 }}
                    transition={{ duration: 0.9 }}
                />
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
                        fontSize={{ xs: theme.typography.h6.fontSize, md: "20px" }}
                    >
                        {product.name}
                    </Typography>
                    <Typography
                        my={1}
                        textAlign="center"
                        fontSize={{ xs: theme.typography.h6.fontSize, md: "20px" }}
                        color={theme.palette.text.secondary}
                    >
                        {product.brand?.name}
                    </Typography>
                </Box>
                <Box textAlign="center">
                    <Typography
                        fontSize={{ xs: theme.typography.h6.fontSize, md: "20px" }}
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
                    <Typography
                        fontSize={{ xs: theme.typography.h6.fontSize, md: "20px" }}
                        sx={{ fontWeight: "900" }}
                        component="span"
                    >
                        ${product.salePrice}
                    </Typography>
                </Box>
            </CardContent>
        </Paper>
    )
}
