import { Box } from "@mui/material"
import QuantityField from "components/inputField/QuantityField"
import { IFormState } from "Pages/ProductPage/components/ProductContent"
import { useForm } from "react-hook-form"

type Props = {
    quantity: number
    handleQuantityChange: (quantity: number) => void
}

const QuantityForm = ({ quantity, handleQuantityChange }: Props) => {
    const { control, handleSubmit, setValue } = useForm<IFormState>({
        defaultValues: {
            quantity: quantity,
        },
    })
    const onSubmit = (value: IFormState) => {
        console.log(value)
    }
    return (
        <Box component="form" onSubmit={handleSubmit(onSubmit)} width={"100%"}>
            <QuantityField
                control={control}
                handleQuantityChange={(value) => {
                    setValue("quantity", value)
                    handleQuantityChange(value)
                }}
            />
        </Box>
    )
}

export default QuantityForm
