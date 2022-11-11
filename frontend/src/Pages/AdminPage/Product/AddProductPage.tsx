import { Box, Paper, Typography } from "@mui/material"
import { productApi } from "ApiClients/ProductApi"
import { useAppSelector } from "app/hooks"
import "draft-js/dist/Draft.css"
import { SaveProductReq } from "models"
import handleNotify from "utils/Toast-notify"
import ProductForm from "./ProductComponent/ProductForm"
type Props = {}

const AddProductPage = (props: Props) => {
    const token = useAppSelector((state) => state.auth.currentUser?.token)
    const handleFormSubmit = async (value: SaveProductReq) => {
        const formData = new FormData()
        formData.append("name", value.name)
        formData.append("descriptions", value.descriptions)
        formData.append("categories", `${value.categories}`)
        formData.append("price", `${value.price}`)
        formData.append("brand", `${value.brand}`)
        if (value.thumbnail) {
            value.thumbnail.forEach((th) => {
                formData.append("media", th)
            })
        }
        if (token) {
            try {
                const result = await productApi.create(formData, token)
                console.log(result)
                handleNotify.success("Add Product Successfully")
            } catch (error) {
                handleNotify.error(error as string)
            }
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
                        p: 3,
                    }}
                >
                    <ProductForm handleSubmitForm={handleFormSubmit} />
                </Paper>
            </Box>
        </Box>
    )
}

export default AddProductPage
