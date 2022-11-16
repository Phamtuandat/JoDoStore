import { Grid } from "@mui/material"
import { Box } from "@mui/system"
import { InputField } from "components/inputField"
import SelectTextFields from "components/inputField/SelectField"
import { convertToRaw, EditorState } from "draft-js"
import "draft-js/dist/Draft.css"
import { Brand, Category } from "models"
import { useEffect, useState } from "react"
import { Control } from "react-hook-form"
import TextEditTor from "../../components/TextEditTor"
type Props = {
    setValue: (value: string) => void
    control: Control<any>
    brands: Brand[]
    categories: Category[]
}

const ProductInfo = ({ setValue, control, categories, brands }: Props) => {
    const [editorState, setEditorState] = useState<EditorState>()
    useEffect(() => {
        if (editorState) {
            setValue(JSON.stringify(convertToRaw(editorState.getCurrentContent())))
        }
    }, [editorState, setValue])

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
                <TextEditTor handleChange={(value) => setEditorState(value)} />
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
                        name="categories"
                        label="Categories"
                        disabled={false}
                        options={categories}
                        isMutiple={false}
                    />
                    <SelectTextFields
                        control={control}
                        name="brand"
                        label="Brand"
                        disabled={false}
                        options={brands}
                        isMutiple={false}
                    />
                </Box>
            </Grid>
        </Grid>
    )
}

export default ProductInfo
