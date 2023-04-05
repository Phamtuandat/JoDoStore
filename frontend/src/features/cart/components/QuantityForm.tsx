import { Box } from "@mui/material"
import QuantityField from "components/inputField/QuantityField"
import { IFormState } from "Pages/ProductPage/components/ProductContent"
import { useForm } from "react-hook-form"

type Props = {
    quantity: number
    handleQuantityChange: (quantity: number) => void
    disabled?: boolean
}

const QuantityForm = ({ quantity, handleQuantityChange, disabled }: Props) => {
    const { control, setValue } = useForm<IFormState>({
        defaultValues: {
            quantity: quantity,
        },
    })

    return (
        <Box width={"100%"}>
            <QuantityField
                control={control}
                handleQuantityChange={(value) => {
                    handleQuantityChange(value)
                }}
                setValue={(value) => setValue("quantity", value)}
                disabled={disabled}
            />
        </Box>
    )
}

export default QuantityForm
