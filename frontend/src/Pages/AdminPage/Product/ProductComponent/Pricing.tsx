import { Box } from "@mui/material"
import { InputField } from "components/inputField"
import SelectTextFields from "components/inputField/SelectField"
import { Category } from "models"
import { Control } from "react-hook-form"

interface Props {
    control: Control<any>
}

const Pricing = ({ control }: Props) => {
    return (
        <Box px={3}>
            <Box width="100%" height="100%" display="flex" justifyContent="space-between">
                <InputField
                    control={control}
                    disabled={false}
                    name="price"
                    label="Price"
                    autoComplete="new-password"
                    variant="standard"
                    fullWidth={false}
                />
                <InputField
                    control={control}
                    disabled={false}
                    name="priceSale"
                    label="Price Sale"
                    variant="standard"
                    autoComplete="new-password"
                    fullWidth={false}
                />
                <InputField
                    control={control}
                    disabled={false}
                    name="sku"
                    label="SKU"
                    variant="standard"
                    autoComplete="new-password"
                    fullWidth={false}
                />
            </Box>
            <SelectTextFields
                control={control}
                name="tags"
                label="Tags"
                isMutiple={true}
                options={options}
                disabled={false}
            />
        </Box>
    )
}

export default Pricing

const options: Category[] = [
    {
        name: "In Stock",
        id: 1,
    },
    {
        name: "Black Friday",
        id: 2,
    },
    {
        name: "Sale",
        id: 3,
    },
    {
        name: "Expired",
        id: 4,
    },

    {
        name: "Out Of Stock",
        id: 5,
    },
]
