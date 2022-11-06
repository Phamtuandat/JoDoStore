import { Box, Paper, Step, StepLabel, Stepper, Typography } from "@mui/material"
import "draft-js/dist/Draft.css"
import { Product } from "models"
import ProductForm from "./ProductComponent/ProductForm"
const steps = ["Select master blaster campaign settings", "Create an ad group", "Create an ad"]
type Props = {}

const AddProductPage = (props: Props) => {
    const handleFormSubmit = (value: Product) => {
        console.log(value)
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
                    <Box sx={{ width: "100%" }}>
                        <Stepper activeStep={1} alternativeLabel>
                            {steps.map((label) => (
                                <Step key={label}>
                                    <StepLabel>{label}</StepLabel>
                                </Step>
                            ))}
                        </Stepper>
                    </Box>
                    <ProductForm handleSubmitForm={handleFormSubmit} />
                </Paper>
            </Box>
        </Box>
    )
}

export default AddProductPage
