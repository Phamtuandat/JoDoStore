import { Box, Paper, Typography } from "@mui/material"
import Backdrop from "@mui/material/Backdrop"
import CircularProgress from "@mui/material/CircularProgress"
import { productApi } from "ApiClients/ProductApi"
import "draft-js/dist/Draft.css"
import { useState } from "react"
import handleNotify from "utils/Toast-notify"
import ProductForm from "./ProductComponent/ProductForm"
type Props = {}

export type SaveProductForm = {
    id?: number | string | null
    name: string | ""
    category: string | null
    brand: string | null
    description: string | null
    price: number | null
    salePrice: number | null
    smallImageLink?: string
    thumbnail?: File[] | []
    tags?: Array<string> | null
}

const AddProductPage = (props: Props) => {
    const [isLoading, setLoading] = useState(false)
    const handleFormSubmit = async (value: SaveProductForm) => {
        const formData = new FormData()
        formData.append("name", value.name)
        formData.append("description", `${value.description}`)
        formData.append("category.name", value.category || "No Category")
        formData.append("salePrice", `${value.salePrice}`)
        formData.append("price", `${value.price}`)
        formData.append("brand.name", `${value.brand}`)
        if (value.tags) {
            value.tags.forEach((tag, i) => {
                formData.append(`tags[${i}]`, `${tag}`)
            })
        }
        if (value.thumbnail) {
            value.thumbnail.forEach((th) => {
                formData.append("thumbnails", th)
            })
        }
        try {
            setLoading(true)
            await productApi.create(formData)
            handleNotify.success("Add Product Successfully")
            setLoading(false)
        } catch (error) {
            handleNotify.error(error as string)
        }
    }
    return (
        <Box display="flex" justifyContent="center" mt={2} flexDirection="column">
            <Box display="flex" flexDirection="column" textAlign="center">
                <Typography variant="h4" fontWeight={700} my={2}>
                    Add New Product
                </Typography>
                <Typography
                    fontStyle="italic"
                    sx={{
                        opacity: 0.6,
                    }}
                >
                    This information will describe more about the product.
                </Typography>
            </Box>
            <Box width="80%" mx="auto" mt={6}>
                <Paper
                    elevation={3}
                    sx={{
                        py: 3,
                        px: { sm: 2, xs: 0, md: 3 },
                    }}
                >
                    <Backdrop
                        sx={{ color: "#fff", zIndex: (theme) => theme.zIndex.drawer + 1 }}
                        open={isLoading}
                    >
                        <CircularProgress color="inherit" />
                    </Backdrop>
                    <ProductForm handleSubmitForm={handleFormSubmit} />
                </Paper>
            </Box>
        </Box>
    )
}

export default AddProductPage
