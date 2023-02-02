/* eslint-disable react-hooks/exhaustive-deps */
import { Grid } from "@mui/material"
import { Box } from "@mui/system"
import { InputField } from "components/inputField"
import SelectTextFields from "components/inputField/SelectField"
import { convertToRaw } from "draft-js"
import "draft-js/dist/Draft.css"
import { Brand, Category } from "models"
import { Control, UseFormSetValue } from "react-hook-form"
import TextEditTor from "../../components/TextEditor"
import { SaveProductForm } from "../AddProductPage"

type Props = {
    control: Control<any>
    brands: Brand[]
    categories: Category[]
    setValue: UseFormSetValue<SaveProductForm>
}
const ProductInfo = ({ control, categories, brands, setValue }: Props) => {
    return (
        <Grid
            sx={{
                mt: 3,
            }}
            container
            spacing={3}
        >
            <Grid
                item
                sx={{
                    p: 1,
                }}
                xs={12}
                lg={6}
            >
                <InputField
                    control={control}
                    name="name"
                    label="Name"
                    disabled={false}
                    variant="standard"
                    autoComplete="new-password"
                    fullWidth={true}
                />
                <TextEditTor
                    control={control}
                    name="description"
                    handleChange={(value) => {
                        setValue(
                            "description",
                            JSON.stringify(convertToRaw(value.getCurrentContent()))
                        )
                    }}
                />
            </Grid>
            <Grid
                item
                sx={{
                    p: 2,
                }}
                xs={12}
                lg={6}
            >
                <Box
                    sx={{
                        "&>div": {
                            width: "100%",
                        },
                    }}
                >
                    <SelectTextFields
                        control={control}
                        name="category"
                        label="category"
                        disabled={false}
                        options={categories}
                        isMutiple={false}
                        setValue={(value) => setValue("category", value)}
                    />
                    <SelectTextFields
                        control={control}
                        name="brand"
                        label="Brand"
                        disabled={false}
                        options={brands}
                        isMutiple={false}
                        setValue={(value) => setValue("brand", value)}
                    />
                </Box>
            </Grid>
        </Grid>
    )
}

export default ProductInfo
