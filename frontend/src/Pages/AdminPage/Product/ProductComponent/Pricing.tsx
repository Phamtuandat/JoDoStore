import { Box, Stack } from "@mui/material"
import { InputField } from "components/inputField"
import SelectTextFields from "components/inputField/SelectField"
import { Tag } from "models"
import { Control, UseFormSetValue } from "react-hook-form"
import { SaveProductForm } from "../AddProductPage"

interface Props {
    control: Control<any>
    setValue: UseFormSetValue<SaveProductForm>
}

const Pricing = ({ control, setValue }: Props) => {
    return (
        <Box px={3}>
            <Stack
                width="100%"
                height="100%"
                display="flex"
                spacing={3}
                direction={{ md: "row", sm: "column" }}
                justifyContent="space-between"
            >
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
                    name="salePrice"
                    label="Sale Price "
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
            </Stack>
            <SelectTextFields
                control={control}
                name="tags"
                label="Tags"
                isMutiple={true}
                options={options}
                disabled={false}
                setValue={(value) => setValue("tags", [value])}
            />
        </Box>
    )
}

export default Pricing

const options: Tag[] = [
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
